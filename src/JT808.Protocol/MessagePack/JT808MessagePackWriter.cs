using JT808.Protocol.Buffers;
using JT808.Protocol.Enums;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Text;

namespace JT808.Protocol.MessagePack
{
    public ref struct JT808MessagePackWriter
    {
        private JT808BufferWriter writer;
        public JT808Version Version { get; set; }
        public JT808MessagePackWriter(Span<byte> buffer, JT808Version version= JT808Version.JTT2013)
        {
            this.writer = new JT808BufferWriter(buffer);
            Version = version;
        }
        public byte[] FlushAndGetEncodingArray()
        {
            return writer.Written.Slice(writer.BeforeCodingWrittenPosition).ToArray();
        }

        public ReadOnlySpan<byte> FlushAndGetEncodingReadOnlySpan()
        {
            return writer.Written.Slice(writer.BeforeCodingWrittenPosition);
        }

        public ReadOnlySpan<byte> FlushAndGetRealReadOnlySpan()
        {
            return writer.Written;
        }

        public byte[] FlushAndGetRealArray()
        {
            return writer.Written.ToArray();
        }
        public void WriteStart()=> WriteByte(JT808Package.BeginFlag);
        public void WriteEnd() => WriteByte(JT808Package.EndFlag);
        public void Nil(out int position)
        {
            position = writer.WrittenCount;
            var span = writer.Free;
            span[0] = 0x00;
            writer.Advance(1);
        }
        public void Skip(int count, out int position)
        {
            position = writer.WrittenCount;
            var span = writer.Free;
            for (var i = 0; i < count; i++)
            {
                span[i] = 0x00;
            }
            writer.Advance(count);
        }
        public void Skip(int count,out int position, byte fullValue = 0x00)
        {
            position = writer.WrittenCount;
            var span = writer.Free;
            for (var i = 0; i < count; i++)
            {
                span[i] = fullValue;
            }
            writer.Advance(count);
        }
        public void WriteByte(byte value)
        {
            var span = writer.Free;
            span[0] = value;
            writer.Advance(1);
        }
        public void WriteInt16(short value)
        {
            BinaryPrimitives.WriteInt16BigEndian(writer.Free, value);
            writer.Advance(2);
        }
        public void WriteUInt16(ushort value)
        {
            BinaryPrimitives.WriteUInt16BigEndian(writer.Free, value);
            writer.Advance(2);
        }
        public void WriteInt32(int value)
        {
            BinaryPrimitives.WriteInt32BigEndian(writer.Free, value);
            writer.Advance(4);
        }
        public void WriteUInt64(ulong value)
        {
            BinaryPrimitives.WriteUInt64BigEndian(writer.Free, value);
            writer.Advance(8);
        }
        public void WriteUInt32(uint value)
        {
            BinaryPrimitives.WriteUInt32BigEndian(writer.Free, value);
            writer.Advance(4);
        }
        public void WriteString(string value)
        {
            byte[] codeBytes = JT808Constants.Encoding.GetBytes(value);
            codeBytes.CopyTo(writer.Free);
            writer.Advance(codeBytes.Length);
        }
        public void WriteArray(ReadOnlySpan<byte> src)
        {
            src.CopyTo(writer.Free);
            writer.Advance(src.Length);
        }
        public void WriteUInt16Return(ushort value, int position)
        {
            BinaryPrimitives.WriteUInt16BigEndian(writer.Written.Slice(position, 2), value);
        }
        public void WriteInt32Return(int value, int position)
        {
            BinaryPrimitives.WriteInt32BigEndian(writer.Written.Slice(position, 4), value);
        }
        public void WriteUInt32Return(uint value, int position)
        {
            BinaryPrimitives.WriteUInt32BigEndian(writer.Written.Slice(position, 4), value);
        }
        public void WriteByteReturn(byte value, int position)
        {
            writer.Written[position] = value;
        }
        public void WriteBCDReturn(string value,int len, int position)
        {
            string bcdText = value ?? "";
            int startIndex = 0;
            int noOfZero = len - bcdText.Length;
            if (noOfZero > 0)
            {
                bcdText = bcdText.Insert(startIndex, new string('0', noOfZero));
            }
            int byteIndex = 0;
            int count = len / 2;
            var bcdSpan = bcdText.AsSpan();
            while (startIndex < bcdText.Length && byteIndex < count)
            {
                writer.Written[position+(byteIndex++)] = Convert.ToByte(bcdSpan.Slice(startIndex, 2).ToString(), 16);
                startIndex += 2;
            }
        }
        public void WriteStringReturn(string value, int position)
        {
            Span<byte> codeBytes = JT808Constants.Encoding.GetBytes(value);
            codeBytes.CopyTo(writer.Written.Slice(position));
        }
        public void WriteArrayReturn(ReadOnlySpan<byte> src, int position)
        {
            src.CopyTo(writer.Written.Slice(position));
        }
        /// <summary>
        /// yyMMddHHmmss
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        public void WriteDateTime6(DateTime value, int fromBase = 16)
        {
            var span = writer.Free;
            span[0] = Convert.ToByte(value.ToString("yy"), fromBase);
            span[1] = Convert.ToByte(value.ToString("MM"), fromBase);
            span[2] = Convert.ToByte(value.ToString("dd"), fromBase);
            span[3] = Convert.ToByte(value.ToString("HH"), fromBase);
            span[4] = Convert.ToByte(value.ToString("mm"), fromBase);
            span[5] = Convert.ToByte(value.ToString("ss"), fromBase);
            writer.Advance(6);
        }
        /// <summary>
        /// HH-mm-ss-msms
        /// HH-mm-ss-fff
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        public void WriteDateTime5(DateTime value, int fromBase = 16)
        {
            var span = writer.Free;
            span[0] = Convert.ToByte(value.ToString("HH"), fromBase);
            span[1] = Convert.ToByte(value.ToString("mm"), fromBase);
            span[2] = Convert.ToByte(value.ToString("ss"), fromBase);
            var msSpan = value.Millisecond.ToString().PadLeft(4,'0').AsSpan();
            span[3] = Convert.ToByte(msSpan.Slice(0, 2).ToString(), fromBase);
            span[4] = Convert.ToByte(msSpan.Slice(2, 2).ToString(), fromBase);
            writer.Advance(5);
        }
        public void WriteUTCDateTime(DateTime value)
        {
            ulong totalSecends = (ulong)(value.AddHours(-8) - JT808Constants.UTCBaseTime).TotalSeconds;
            var span = writer.Free;
            //高位在前
            for (int i = 7; i >= 0; i--)
            {
                span[i] = (byte)(totalSecends & 0xFF);  //取低8位
                totalSecends >>= 8;
            }
            writer.Advance(8);
        }
        /// <summary>
        /// YYYYMMDD
        /// BCD[4]
        /// 数据形如：20200101
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        public void WriteDateTime4(DateTime value, int fromBase = 16)
        {
            var span = writer.Free;
            var yearSpan=value.ToString("yyyy").AsSpan();
            span[0] = Convert.ToByte(yearSpan.Slice(0, 2).ToString(), fromBase);
            span[1] = Convert.ToByte(yearSpan.Slice(2, 2).ToString(), fromBase);
            span[2] = Convert.ToByte(value.ToString("MM"), fromBase);
            span[3] = Convert.ToByte(value.ToString("dd"), fromBase);
            writer.Advance(4);
        }
        /// <summary>
        /// YYMMDD
        /// BCD[4]
        /// 数据形如：20200101
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        public void WriteDateTime3(DateTime value, int fromBase = 16)
        {
            var span = writer.Free;
            span[0] = Convert.ToByte(value.ToString("yy"), fromBase);
            span[1] = Convert.ToByte(value.ToString("MM"), fromBase);
            span[2] = Convert.ToByte(value.ToString("dd"), fromBase);
            writer.Advance(3);
        }
        public void WriteXor(int start, int end)
        {
            if (start > end)
            {
                throw new ArgumentOutOfRangeException($"start>end:{start}>{end}");
            }
            var xorSpan = writer.Written.Slice(start, end);
            byte result = xorSpan[0];
            for (int i = start + 1; i < end; i++)
            {
                result = (byte)(result ^ xorSpan[i]);
            }
            var span = writer.Free;
            span[0] = result;
            writer.Advance(1);
        }
        public void WriteXor(int start)
        {
            if(writer.WrittenCount< start)
            {
                throw new ArgumentOutOfRangeException($"Written<start:{writer.WrittenCount}>{start}");
            }
            var xorSpan = writer.Written.Slice(start);
            byte result = xorSpan[0];
            for (int i = start + 1; i < xorSpan.Length; i++)
            {
                result = (byte)(result ^ xorSpan[i]);
            }
            var span = writer.Free;
            span[0] = result;
            writer.Advance(1);
        }
        public void WriteXor()
        {
            if (writer.WrittenCount < 1)
            {
                throw new ArgumentOutOfRangeException($"Written<start:{writer.WrittenCount}>{1}");
            }
            //从第1位开始
            var xorSpan = writer.Written.Slice(1);
            byte result = xorSpan[0];
            for (int i = 1; i < xorSpan.Length; i++)
            {
                result = (byte)(result ^ xorSpan[i]);
            }
            var span = writer.Free;
            span[0] = result;
            writer.Advance(1);
        }
        public void WriteBCD(string value, int len)
        {
            string bcdText = value ?? "";
            int startIndex = 0;
            int noOfZero = len - bcdText.Length;
            if (noOfZero > 0)
            {
                bcdText = bcdText.Insert(startIndex, new string('0', noOfZero));
            }
            int byteIndex = 0;
            int count = len / 2;
            var bcdSpan = bcdText.AsSpan();
            var spanFree = writer.Free;
            while (startIndex < bcdText.Length && byteIndex < count)
            {
                spanFree[byteIndex++] = Convert.ToByte(bcdSpan.Slice(startIndex, 2).ToString(), 16);
                startIndex += 2;
            }
            writer.Advance(byteIndex);
        }
        public void WriteHex(string value, int len)
        {
            value = value ?? "";
            value = value.Replace(" ", "");
            int startIndex = 0;
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                startIndex = 2;
            }
            int length = len;
            if (length == -1)
            {
                length = (value.Length - startIndex) / 2;
            }
            int noOfZero = length * 2 + startIndex - value.Length;
            if (noOfZero > 0)
            {
                value = value.Insert(startIndex, new string('0', noOfZero));
            }
            int byteIndex = 0;
            var hexSpan = value.AsSpan();
            var spanFree = writer.Free;
            while (startIndex < value.Length && byteIndex < length)
            {
                spanFree[byteIndex++] = Convert.ToByte(hexSpan.Slice(startIndex, 2).ToString(), 16);
                startIndex += 2;
            }
            writer.Advance(byteIndex);
        }
        public void WriteASCII(string value)
        {
            var spanFree = writer.Free;
            var bytes = Encoding.ASCII.GetBytes(value).AsSpan();
            bytes.CopyTo(spanFree);
            writer.Advance(bytes.Length);
        }
        public void WriteFullEncode()
        {
            var tmpSpan = writer.Written;
            writer.BeforeCodingWrittenPosition = writer.WrittenCount;
            var spanFree = writer.Free;
            int tempOffset = 0;
            for (int i = 0; i < tmpSpan.Length; i++)
            {
                if (tmpSpan[i] == 0x7e)
                {
                    spanFree[tempOffset++] = 0x7d;
                    spanFree[tempOffset++] = 0x02;
                }
                else if (tmpSpan[i] == 0x7d)
                {
                    spanFree[tempOffset++] = 0x7d;
                    spanFree[tempOffset++] = 0x01;
                }
                else
                {
                    spanFree[tempOffset++] = tmpSpan[i];
                }
            }
            writer.Advance(tempOffset);
        }
        internal void WriteEncode()
        {
            var tmpSpan = writer.Written;
            writer.BeforeCodingWrittenPosition = writer.WrittenCount;
            var spanFree = writer.Free;
            int tempOffset = 0;
            spanFree[tempOffset++] = tmpSpan[0];
            for (int i = 1; i < tmpSpan.Length - 1; i++)
            {
                if (tmpSpan[i] == 0x7e)
                {
                    spanFree[tempOffset++] = 0x7d;
                    spanFree[tempOffset++] = 0x02;
                }
                else if (tmpSpan[i] == 0x7d)
                {
                    spanFree[tempOffset++] = 0x7d;
                    spanFree[tempOffset++] = 0x01;
                }
                else
                {
                    spanFree[tempOffset++] = tmpSpan[i];
                }
            }
            spanFree[tempOffset++] = tmpSpan[tmpSpan.Length - 1];
            writer.Advance(tempOffset);
        }
        /// <summary>
        /// 数字编码 大端模式、高位在前
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        public void WriteBigNumber(string value, int len)
        {
            var spanFree = writer.Free;
            ulong number = string.IsNullOrEmpty(value) ? 0 : (ulong)double.Parse(value);
            for (int i = len - 1; i >= 0; i--)
            {
                spanFree[i] = (byte)(number & 0xFF);  //取低8位
                number = number >> 8;
            }
            writer.Advance(len);
        }
        public int GetCurrentPosition()
        {
            return writer.WrittenCount;
        }
        public void  WriteCarDVRCheckCode(int currentPosition)
        {
            var carDVRPackage = writer.Written.Slice(currentPosition, writer.WrittenCount- currentPosition);
            byte calculateXorCheckCode = 0;
            foreach (var item in carDVRPackage)
            {
                calculateXorCheckCode = (byte)(calculateXorCheckCode ^ item);
            }
            WriteByte(calculateXorCheckCode);
        }
    }
}
