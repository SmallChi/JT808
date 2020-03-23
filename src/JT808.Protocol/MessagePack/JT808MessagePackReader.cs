using JT808.Protocol.Buffers;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace JT808.Protocol.MessagePack
{
    public ref struct JT808MessagePackReader
    {
        public ReadOnlySpan<byte> Reader { get; private set; }
        public ReadOnlySpan<byte> SrcBuffer { get; }
        public int ReaderCount { get; private set; }
        public JT808Version Version { get; set; }
        private byte _calculateCheckXorCode;
        private byte _realCheckXorCode;
        private bool _checkXorCodeVali;
        /// <summary>
        /// 是否进行解码操作
        /// 若进行解码操作，则对应的是一个正常的包
        /// 若不进行解码操作，则对应的是一个非正常的包（头部包，数据体包等等）
        /// 主要用来一次性读取所有数据体内容操作
        /// </summary>
        private bool _decoded;
        private static readonly byte[] decode7d01 = new byte[] { 0x7d, 0x01 };
        private static readonly byte[] decode7d02 = new byte[] { 0x7d, 0x02 };
        /// <summary>
        /// 解码（转义还原）,计算校验和
        /// </summary>
        /// <param name="buffer"></param>
        public JT808MessagePackReader(ReadOnlySpan<byte> srcBuffer, JT808Version version = JT808Version.JTT2013)
        {
            SrcBuffer = srcBuffer;
            ReaderCount = 0;
            _realCheckXorCode = 0x00;
            _calculateCheckXorCode = 0x00;
            _checkXorCodeVali = false;
            _decoded = false;
            Version = version;
            Reader = srcBuffer;
        }
        /// <summary>
        /// 在解码的时候把校验和也计算出来，避免在循环一次进行校验
        /// </summary>
        /// <returns></returns>
        public void Decode()
        {
            Span<byte> span = new byte[SrcBuffer.Length];
            Decode(span);
            _decoded = true;
        }
        /// <summary>
        /// 在解码的时候把校验和也计算出来，避免在循环一次进行校验
        /// </summary>
        /// <returns></returns>
        public void Decode(Span<byte> allocateBuffer)
        {
            int i = 0;
            int offset = 0;
            int len = SrcBuffer.Length;
            _realCheckXorCode = 0;
            allocateBuffer[offset++] = SrcBuffer[0];
            // 取出校验码看是否需要转义
            ReadOnlySpan<byte> checkCodeBufferSpan = SrcBuffer.Slice(len - 3,2);
            int checkCodeLen = 1;
            if (checkCodeBufferSpan.SequenceEqual(decode7d01))
            {
                _realCheckXorCode = 0x7d;
                checkCodeLen += 1;
            }
            else if (checkCodeBufferSpan.SequenceEqual(decode7d02))
            {
                _realCheckXorCode = 0x7e;
                checkCodeLen += 1;
            }
            else
            {
                _realCheckXorCode = checkCodeBufferSpan[1];
            }
            len = len - checkCodeLen - 1 - 1;
            ReadOnlySpan<byte> tmpBufferSpan = SrcBuffer.Slice(1, len);
            while (i < len)
            {
                if (tmpBufferSpan[i] == 0x7d)
                {
                    if (len > i + 1)
                    {
                        if (tmpBufferSpan[i + 1] == 0x01)
                        {
                            allocateBuffer[offset++] = 0x7d;
                            _calculateCheckXorCode = (byte)(_calculateCheckXorCode ^ 0x7d);
                            i++;
                        }
                        else if (tmpBufferSpan[i + 1] == 0x02)
                        {
                            allocateBuffer[offset++] = 0x7e;
                            _calculateCheckXorCode = (byte)(_calculateCheckXorCode ^ 0x7e);
                            i++;
                        }
                        else
                        {
                            allocateBuffer[offset++] = tmpBufferSpan[i];
                            _calculateCheckXorCode = (byte)(_calculateCheckXorCode ^ tmpBufferSpan[i]);
                        }
                    }
                }
                else
                {
                    allocateBuffer[offset++] = tmpBufferSpan[i];
                    _calculateCheckXorCode = (byte)(_calculateCheckXorCode ^ tmpBufferSpan[i]);
                }
                i++;
            }
            allocateBuffer[offset++] = _realCheckXorCode;
            allocateBuffer[offset++] = SrcBuffer[SrcBuffer.Length- 1];
            _checkXorCodeVali = (_calculateCheckXorCode == _realCheckXorCode);
            Reader = allocateBuffer.Slice(0, offset);
            _decoded = true;
        }
        public byte CalculateCheckXorCode => _calculateCheckXorCode;
        public byte RealCheckXorCode => _realCheckXorCode;
        public bool CheckXorCodeVali => _checkXorCodeVali;
        public byte ReadStart()=> ReadByte();
        public byte ReadEnd()=> ReadByte();
        public short ReadInt16()
        {
            return BinaryPrimitives.ReadInt16BigEndian(GetReadOnlySpan(2));
        }
        public ushort ReadUInt16()
        {
            return BinaryPrimitives.ReadUInt16BigEndian(GetReadOnlySpan(2)); 
        }
        public uint ReadUInt32()
        {
            return BinaryPrimitives.ReadUInt32BigEndian(GetReadOnlySpan(4));
        }
        public int ReadInt32()
        {
            return BinaryPrimitives.ReadInt32BigEndian(GetReadOnlySpan(4));
        }
        public ulong ReadUInt64()
        {
            return BinaryPrimitives.ReadUInt64BigEndian(GetReadOnlySpan(8));
        }
        public long ReadInt64()
        {
            return BinaryPrimitives.ReadInt64BigEndian(GetReadOnlySpan(8));
        }
        public byte ReadByte()
        {
            return GetReadOnlySpan(1)[0];
        }
        public byte ReadVirtualByte()
        {
            return GetVirtualReadOnlySpan(1)[0];
        }
        public ReadOnlySpan<byte> ReadVirtualArray(int count)
        {
            return GetVirtualReadOnlySpan(count);
        }
        public ushort ReadVirtualUInt16()
        {
            return BinaryPrimitives.ReadUInt16BigEndian(GetVirtualReadOnlySpan(2));
        }
        public short ReadVirtualInt16()
        {
            return BinaryPrimitives.ReadInt16BigEndian(GetVirtualReadOnlySpan(2));
        }
        public uint ReadVirtualUInt32()
        {
            return BinaryPrimitives.ReadUInt32BigEndian(GetVirtualReadOnlySpan(4));
        }
        public int ReadVirtualInt32()
        {
            return BinaryPrimitives.ReadInt32BigEndian(GetVirtualReadOnlySpan(4));
        }
        public ulong ReadVirtualUInt64()
        {
            return BinaryPrimitives.ReadUInt64BigEndian(GetVirtualReadOnlySpan(8));
        }
        public long ReadVirtualInt64()
        {
            return BinaryPrimitives.ReadInt64BigEndian(GetVirtualReadOnlySpan(8));
        }
        /// <summary>
        /// 数字编码 大端模式、高位在前
        /// </summary>
        /// <param name="len"></param>
        public string ReadBigNumber(int len)
        {
            ulong result = 0;
            var readOnlySpan = GetReadOnlySpan(len);
            for (int i = 0; i < len; i++)
            {
                ulong currentData = (ulong)readOnlySpan[i] << (8 * (len - i - 1));
                result += currentData;
            }
            return result.ToString();
        }
        public ReadOnlySpan<byte> ReadArray(int len)
        {
            return GetReadOnlySpan(len).Slice(0, len);
        }
        public ReadOnlySpan<byte> ReadArray(int start,int end)
        {
            return Reader.Slice(start,end);
        }
        public string ReadString(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string value = JT808Constants.Encoding.GetString(readOnlySpan.Slice(0, len).ToArray());
            return value.Trim('\0');
        }
        public string ReadASCII(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string value = Encoding.ASCII.GetString(readOnlySpan.Slice(0, len).ToArray());
            return value;
        }
        public string ReadRemainStringContent()
        {
            return ReadString(ReadCurrentRemainContentLength());
        }
        public string ReadHex(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string hex = HexUtil.DoHexDump(readOnlySpan, 0, len);
            return hex;
        }
        /// <summary>
        /// yyMMddHHmmss
        /// </summary>
        /// <param name="fromBase">>D2： 10  X2：16</param>
        public DateTime ReadDateTime6(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(6);
                int year = Convert.ToInt32(readOnlySpan[0].ToString(format)) + JT808Constants.DateLimitYear;
                int month = Convert.ToInt32(readOnlySpan[1].ToString(format));
                int day = Convert.ToInt32(readOnlySpan[2].ToString(format));
                int hour = Convert.ToInt32(readOnlySpan[3].ToString(format));
                int minute = Convert.ToInt32(readOnlySpan[4].ToString(format));
                int second = Convert.ToInt32(readOnlySpan[5].ToString(format));
                d = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// HH-mm-ss-msms
        /// HH-mm-ss-fff
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        public DateTime ReadDateTime5(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(5);
                StringBuilder sb = new StringBuilder(4);
                sb.Append(readOnlySpan[3].ToString("X2"));
                sb.Append(readOnlySpan[4].ToString("X2"));
                d = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                Convert.ToInt32(readOnlySpan[0].ToString(format)),
                Convert.ToInt32(readOnlySpan[1].ToString(format)),
                Convert.ToInt32(readOnlySpan[2].ToString(format)),
                Convert.ToInt32(sb.ToString().TrimStart()));
            }
            catch
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// YYYYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        public DateTime ReadDateTime4(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(4);
                StringBuilder sb = new StringBuilder(4);
                sb.Append(readOnlySpan[0].ToString("X2"));
                sb.Append(readOnlySpan[1].ToString("X2"));
                d = new DateTime(
                Convert.ToInt32(sb.ToString()),
                Convert.ToInt32(readOnlySpan[2].ToString(format)),
                Convert.ToInt32(readOnlySpan[3].ToString(format)));
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;   
        }
        /// <summary>
        /// YYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        public DateTime ReadDateTime3(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(3);
                d = new DateTime(
                Convert.ToInt32(readOnlySpan[0].ToString(format)) + JT808Constants.DateLimitYear,
                Convert.ToInt32(readOnlySpan[1].ToString(format)),
                Convert.ToInt32(readOnlySpan[2].ToString(format)));
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        public DateTime ReadUTCDateTime()
        {
            DateTime d;
            try
            {
                ulong result = 0;
                var readOnlySpan = GetReadOnlySpan(8);
                for (int i = 0; i < 8; i++)
                {
                    ulong currentData = (ulong)readOnlySpan[i] << (8 * (8 - i - 1));
                    result += currentData;
                }
                d = JT808Constants.UTCBaseTime.AddSeconds(result).AddHours(8);
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        public string ReadBCD(int len , bool trim = true)
        {
            int count = len / 2;
            var readOnlySpan = GetReadOnlySpan(count);
            StringBuilder bcdSb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                bcdSb.Append(readOnlySpan[i].ToString("X2"));
            }
            if (trim)
            {
                return bcdSb.ToString().TrimStart('0');
            }
            else
            {
                return bcdSb.ToString();
            }  
        }
        private ReadOnlySpan<byte> GetReadOnlySpan(int count)
        {
            ReaderCount += count;
            return Reader.Slice(ReaderCount - count);
        }
        public ReadOnlySpan<byte> GetVirtualReadOnlySpan(int count)
        {
            return Reader.Slice(ReaderCount, count);
        }
        public ReadOnlySpan<byte> ReadContent(int count=0)
        {
            if (_decoded)
            {
                //内容长度=总长度-读取的长度-2（校验码1位+终止符1位）
                int totalContent = Reader.Length - ReaderCount - 2;
                //实际读取内容长度
                int realContent = totalContent - count;
                int tempReaderCount = ReaderCount;
                ReaderCount += realContent;
                return Reader.Slice(tempReaderCount, realContent);
            }
            else
            {
                return Reader.Slice(ReaderCount);
            }
        }
        public int ReadCurrentRemainContentLength()
        {
            if (_decoded)
            {
                //内容长度=总长度-读取的长度-2（校验码1位+终止符1位）
                return Reader.Length - ReaderCount - 2; 
            }
            else
            {
                return Reader.Length - ReaderCount;
            }
        }
        public void Skip(int count=1)
        {
            ReaderCount += count;
        }
        public (byte CalculateXorCheckCode, byte RealXorCheckCode) ReadCarDVRCheckCode(int currentPosition)
        {
            var reader = Reader.Slice(currentPosition, ReaderCount - currentPosition);
            byte calculateXorCheckCode = 0;
            foreach (var item in reader)
            {
                calculateXorCheckCode = (byte)(calculateXorCheckCode ^ item);
            }
            var realXorCheckCode = Reader.Slice(ReaderCount)[0];
            return (calculateXorCheckCode, realXorCheckCode);
        }
    }
}
