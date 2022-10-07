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
    /// <summary>
    /// JT808消息读取器
    /// </summary>
    public ref struct JT808MessagePackReader
    {
        /// <summary>
        /// 读取buffer
        /// </summary>
        public ReadOnlySpan<byte> Reader { get; private set; }
        /// <summary>
        /// 原数据
        /// </summary>
        public ReadOnlySpan<byte> SrcBuffer { get; }
        /// <summary>
        /// 读取到的数量
        /// </summary>
        public int ReaderCount { get; private set; }
        /// <summary>
        /// JT808版本号
        /// </summary>
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
        /// <param name="srcBuffer"></param>
        /// <param name="version">默认JT808Version.JTT2013</param>
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
        /// <summary>
        /// 计算的校验码
        /// </summary>
        public byte CalculateCheckXorCode => _calculateCheckXorCode;
        /// <summary>
        /// 实际获取的校验码
        /// </summary>
        public byte RealCheckXorCode => _realCheckXorCode;
        /// <summary>
        /// 验证码是否正确
        /// </summary>
        public bool CheckXorCodeVali => _checkXorCodeVali;
        /// <summary>
        /// 读取标识头
        /// </summary>
        /// <returns></returns>
        public byte ReadStart()=> ReadByte();
        /// <summary>
        /// 读取尾标识
        /// </summary>
        /// <returns></returns>
        public byte ReadEnd()=> ReadByte();
        /// <summary>
        /// 读取有符号位的两字节数值类型
        /// </summary>
        /// <returns></returns>
        public short ReadInt16()
        {
            return BinaryPrimitives.ReadInt16BigEndian(GetReadOnlySpan(2));
        }
        /// <summary>
        /// 读取无符号位的两字节数值类型
        /// </summary>
        /// <returns></returns>
        public ushort ReadUInt16()
        {
            return BinaryPrimitives.ReadUInt16BigEndian(GetReadOnlySpan(2)); 
        }
        /// <summary>
        /// 读取无符号位的四字节数值类型
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt32()
        {
            return BinaryPrimitives.ReadUInt32BigEndian(GetReadOnlySpan(4));
        }
        /// <summary>
        /// 读取有符号位的四字节数值类型
        /// </summary>
        /// <returns></returns>
        public int ReadInt32()
        {
            return BinaryPrimitives.ReadInt32BigEndian(GetReadOnlySpan(4));
        }
        /// <summary>
        /// 读取无符号位的八字节数值类型
        /// </summary>
        /// <returns></returns>
        public ulong ReadUInt64()
        {
            return BinaryPrimitives.ReadUInt64BigEndian(GetReadOnlySpan(8));
        }
        /// <summary>
        /// 读取有符号位的八字节数值类型
        /// </summary>
        /// <returns></returns>
        public long ReadInt64()
        {
            return BinaryPrimitives.ReadInt64BigEndian(GetReadOnlySpan(8));
        }
        /// <summary>
        /// 读取一个字节
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return GetReadOnlySpan(1)[0];
        }
        /// <summary>
        /// 读取一个字符
        /// </summary>
        /// <returns></returns>
        public char ReadChar()
        {
            return (char)GetReadOnlySpan(1)[0];
        }
        /// <summary>
        /// 虚拟读取一个字节，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public byte ReadVirtualByte()
        {
            return GetVirtualReadOnlySpan(1)[0];
        }
        /// <summary>
        /// 虚拟读取一个数组，不计入内存偏移量
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> ReadVirtualArray(int count)
        {
            return GetVirtualReadOnlySpan(count);
        }
        /// <summary>
        /// 虚拟读取无符号位的两字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public ushort ReadVirtualUInt16()
        {
            return BinaryPrimitives.ReadUInt16BigEndian(GetVirtualReadOnlySpan(2));
        }
        /// <summary>
        /// 虚拟读取有符号位的两字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public short ReadVirtualInt16()
        {
            return BinaryPrimitives.ReadInt16BigEndian(GetVirtualReadOnlySpan(2));
        }
        /// <summary>
        /// 虚拟读取无符号位的四字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public uint ReadVirtualUInt32()
        {
            return BinaryPrimitives.ReadUInt32BigEndian(GetVirtualReadOnlySpan(4));
        }
        /// <summary>
        /// 虚拟读取有符号位的四字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public int ReadVirtualInt32()
        {
            return BinaryPrimitives.ReadInt32BigEndian(GetVirtualReadOnlySpan(4));
        }
        /// <summary>
        /// 虚拟读取无符号位的八字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public ulong ReadVirtualUInt64()
        {
            return BinaryPrimitives.ReadUInt64BigEndian(GetVirtualReadOnlySpan(8));
        }
        /// <summary>
        /// 虚拟读取有符号位的八字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public long ReadVirtualInt64()
        {
            return BinaryPrimitives.ReadInt64BigEndian(GetVirtualReadOnlySpan(8));
        }
        /// <summary>
        /// 读取数字编码 
        /// 大端模式、高位在前
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
        /// <summary>
        /// 读取固定大小的内存块
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> ReadArray(int len)
        {
            return GetReadOnlySpan(len).Slice(0, len);
        }
        /// <summary>
        /// 读取固定大小的内存块
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> ReadArray(int start,int end)
        {
            return Reader.Slice(start,end);
        }
        /// <summary>
        /// 读取GBK字符串编码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public string ReadString(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string value = JT808Constants.Encoding.GetString(readOnlySpan.Slice(0, len).ToArray());
            return value.Trim('\0');
        }
        /// <summary>
        /// 读取ASCII编码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public string ReadASCII(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string value = Encoding.ASCII.GetString(readOnlySpan.Slice(0, len).ToArray());
            return value;
        }
        /// <summary>
        /// 读取剩余数据体内容为字符串模式
        /// </summary>
        /// <returns></returns>
        public string ReadRemainStringContent()
        {
            return ReadString(ReadCurrentRemainContentLength());
        }
        /// <summary>
        /// 读取16进制编码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public string ReadHex(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string hex = Convert.ToHexString(readOnlySpan.Slice(0, len));
            return hex;
        }
        /// <summary>
        /// 读取六字节日期,yyMMddHHmmss
        /// </summary>
        /// <param name="format">>D2： 10  X2：16</param>
        [Obsolete("use ReadDateTime_yyMMddHHmmss")]
        public DateTime ReadDateTime6(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(6);
                int year = BcdToInt(readOnlySpan[0])  + JT808Constants.DateLimitYear;
                int month = BcdToInt(readOnlySpan[1]);
                int day = BcdToInt(readOnlySpan[2]);
                int hour = BcdToInt(readOnlySpan[3]);
                int minute = BcdToInt(readOnlySpan[4]);
                int second = BcdToInt(readOnlySpan[5]);
                d = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取六字节日期,yyMMddHHmmss
        /// </summary>
        public DateTime ReadDateTime_yyMMddHHmmss()
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(6);
                int year = BcdToInt(readOnlySpan[0]) + JT808Constants.DateLimitYear;
                int month = BcdToInt(readOnlySpan[1]);
                int day = BcdToInt(readOnlySpan[2]);
                int hour = BcdToInt(readOnlySpan[3]);
                int minute = BcdToInt(readOnlySpan[4]);
                int second = BcdToInt(readOnlySpan[5]);
                d = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 16进制的BCD BYTE转成整型
        /// </summary>
        /// <param name="value">16进制的BCD BYTE转成整型</param>
        /// <returns></returns>
        public int BcdToInt(byte value)
        {
            int high = value >> 4;
            int low = value & 0xF;
            int number = 10 * high + low;
            return number;
        }
        /// <summary>
        /// 读取可空类型的六字节日期,yyMMddHHmmss
        /// </summary>
        /// <param name="format">>D2： 10  X2：16</param>
        [Obsolete("use ReadDateTimeNull_yyMMddHHmmss")]
        public DateTime? ReadDateTimeNull6(string format = "X2")
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(6);
                int year = BcdToInt(readOnlySpan[0]);
                int month = BcdToInt(readOnlySpan[1]);
                int day = BcdToInt(readOnlySpan[2]);
                int hour = BcdToInt(readOnlySpan[3]);
                int minute = BcdToInt(readOnlySpan[4]);
                int second = BcdToInt(readOnlySpan[5]);
                if (year == 0 && month == 0 && day == 0 && hour == 0 && minute == 0 && second == 0) return null;
                d = new DateTime(year + JT808Constants.DateLimitYear, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                return null;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的六字节日期,yyMMddHHmmss
        /// </summary>
        public DateTime? ReadDateTimeNull_yyMMddHHmmss()
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(6);
                int year = BcdToInt(readOnlySpan[0]);
                int month = BcdToInt(readOnlySpan[1]);
                int day = BcdToInt(readOnlySpan[2]);
                int hour = BcdToInt(readOnlySpan[3]);
                int minute = BcdToInt(readOnlySpan[4]);
                int second = BcdToInt(readOnlySpan[5]);
                if (year == 0 && month == 0 && day == 0 && hour == 0 && minute == 0 && second == 0) return null;
                d = new DateTime(year + JT808Constants.DateLimitYear, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                return null;
            }
            return d;
        }
        /// <summary>
        /// 读取五字节日期,HH-mm-ss-msms|HH-mm-ss-fff
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        [Obsolete("use ReadDateTime_HHmmssfff")]
        public DateTime ReadDateTime5(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(5);
                var fff = BcdToInt(readOnlySpan[3]) * 100 + BcdToInt(readOnlySpan[4]);
                d = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                BcdToInt(readOnlySpan[0]),
                BcdToInt(readOnlySpan[1]),
                BcdToInt(readOnlySpan[2]), fff);
            }
            catch
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取五字节日期,HH-mm-ss-msms|HH-mm-ss-fff
        /// </summary>
        public DateTime ReadDateTime_HHmmssfff()
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(5);
                var fff = BcdToInt(readOnlySpan[3]) * 100 + BcdToInt(readOnlySpan[4]);
                d = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                BcdToInt(readOnlySpan[0]),
                BcdToInt(readOnlySpan[1]),
                BcdToInt(readOnlySpan[2]), fff);
            }
            catch
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的五字节日期,HH-mm-ss-msms|HH-mm-ss-fff
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        [Obsolete("use ReadDateTimeNull_HHmmssfff")]
        public DateTime? ReadDateTimeNull5(string format = "X2")
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(5);
                int hour = BcdToInt(readOnlySpan[0]);
                int minute = BcdToInt(readOnlySpan[1]);
                int second = BcdToInt(readOnlySpan[2]);
                var fff = BcdToInt(readOnlySpan[3]) * 100 + BcdToInt(readOnlySpan[4]);
                if (hour == 0 && minute == 0 && second == 0 && fff == 0) return null;
                d = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                hour,
                minute,
                second,
                fff);
            }
            catch
            {
                return null;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的五字节日期,HH-mm-ss-msms|HH-mm-ss-fff
        /// </summary>
        public DateTime? ReadDateTimeNull_HHmmssfff()
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(5);
                int hour = BcdToInt(readOnlySpan[0]);
                int minute = BcdToInt(readOnlySpan[1]);
                int second = BcdToInt(readOnlySpan[2]);
                var fff = BcdToInt(readOnlySpan[3]) * 100 + BcdToInt(readOnlySpan[4]);
                if (hour == 0 && minute == 0 && second == 0 && fff == 0) return null;
                d = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                hour,
                minute,
                second,
                fff);
            }
            catch
            {
                return null;
            }
            return d;
        }
        /// <summary>
        /// 读取四字节日期，YYYYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        [Obsolete("use ReadDateTime_YYYYMMDD")]
        public DateTime ReadDateTime4(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(4);
                var year = BcdToInt(readOnlySpan[0]) * 100 + BcdToInt(readOnlySpan[1]);
                d = new DateTime(
                    year, 
                    BcdToInt(readOnlySpan[2]),
                    BcdToInt(readOnlySpan[3]));
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;   
        }
        /// <summary>
        /// 读取四字节日期，YYYYMMDD
        /// </summary>
        public DateTime ReadDateTime_YYYYMMDD()
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(4);
                var year = BcdToInt(readOnlySpan[0]) * 100 + BcdToInt(readOnlySpan[1]);
                d = new DateTime(
                    year,
                    BcdToInt(readOnlySpan[2]),
                    BcdToInt(readOnlySpan[3]));
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的四字节日期，YYYYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        [Obsolete("use ReadDateTimeNull_YYYYMMDD")]
        public DateTime? ReadDateTimeNull4(string format = "X2")
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(4);
                var year = BcdToInt(readOnlySpan[0]) * 100 + BcdToInt(readOnlySpan[1]);
                int month = BcdToInt(readOnlySpan[2]);
                int day = BcdToInt(readOnlySpan[3]);
                if (year == 0 && month == 0 && day == 0) return null;
                d = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                return null;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的四字节日期，YYYYMMDD
        /// </summary>
        public DateTime? ReadDateTimeNull_YYYYMMDD()
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(4);
                var year = BcdToInt(readOnlySpan[0]) * 100 + BcdToInt(readOnlySpan[1]);
                int month = BcdToInt(readOnlySpan[2]);
                int day = BcdToInt(readOnlySpan[3]);
                if (year == 0 && month == 0 && day == 0) return null;
                d = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                return null;
            }
            return d;
        }
        /// <summary>
        /// 读取三字节日期，YYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        [Obsolete("use ReadDateTime_YYMMDD")]
        public DateTime ReadDateTime3(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(3);
                d = new DateTime(
                BcdToInt(readOnlySpan[0]) + JT808Constants.DateLimitYear,
                BcdToInt(readOnlySpan[1]),
                BcdToInt(readOnlySpan[2]));
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取三字节日期，YYMMDD
        /// </summary>
        public DateTime ReadDateTime_YYMMDD()
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(3);
                d = new DateTime(
                BcdToInt(readOnlySpan[0]) + JT808Constants.DateLimitYear,
                BcdToInt(readOnlySpan[1]),
                BcdToInt(readOnlySpan[2]));
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的三字节日期，YYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        [Obsolete("use ReadDateTimeNull_YYMMDD")]
        public DateTime? ReadDateTimeNull3(string format = "X2")
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(3);
                int year = BcdToInt(readOnlySpan[0]);
                int month= BcdToInt(readOnlySpan[1]);
                int day = BcdToInt(readOnlySpan[2]);
                if (year == 0 && month == 0 && day == 0) return null;
                d = new DateTime(
                 year + JT808Constants.DateLimitYear, month,day
                );
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的三字节日期，YYMMDD
        /// </summary>
        public DateTime? ReadDateTimeNull_YYMMDD()
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(3);
                int year = BcdToInt(readOnlySpan[0]);
                int month = BcdToInt(readOnlySpan[1]);
                int day = BcdToInt(readOnlySpan[2]);
                if (year == 0 && month == 0 && day == 0) return null;
                d = new DateTime(
                 year + JT808Constants.DateLimitYear, month, day
                );
            }
            catch (Exception)
            {
                d = JT808Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取UTC时间类型
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 读取BCD编码
        /// </summary>
        /// <param name="len"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取数量大小的内存块
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private ReadOnlySpan<byte> GetReadOnlySpan(int count)
        {
            ReaderCount += count;
            return Reader.Slice(ReaderCount - count);
        }
        /// <summary>
        /// 虚拟读取数量大小的内存块，不计入内存偏移量
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> GetVirtualReadOnlySpan(int count)
        {
            return Reader.Slice(ReaderCount, count);
        }
        /// <summary>
        /// 读取数据体内存块
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取一整串字符串到\0结束
        /// </summary>
        /// <returns></returns>
        public string ReadStringEndChar0()
        {
            var remainSpans = Reader.Slice(ReaderCount, ReadCurrentRemainContentLength());
            int length = remainSpans.IndexOf((byte)'\0') + 1;
            string value = JT808Constants.Encoding.GetString(ReadArray(length).ToArray());
            return value.Trim('\0');
        }
        /// <summary>
        /// 虚拟读取一整串字符串到\0结束，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public string ReadVirtualStringEndChar0()
        {
            var remainSpans = Reader.Slice(ReaderCount);
            string value = JT808Constants.Encoding.GetString(GetVirtualReadOnlySpan(remainSpans.IndexOf((byte)'\0') + 1).ToArray());
            return value.Trim('\0');
        }
        /// <summary>
        /// 读取剩余数据体内容长度
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 跳过多少字节
        /// </summary>
        /// <param name="count"></param>
        public void Skip(int count=1)
        {
            ReaderCount += count;
        }
        /// <summary>
        /// 读取JT19056校验码
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
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
