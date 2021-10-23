using JT808.Protocol.Buffers;
using JT808.Protocol.Enums;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessagePack
{
    /// <summary>
    /// JT808消息写入器
    /// </summary>
    public ref struct JT808MessagePackWriter
    {
        private JT808BufferWriter writer;
        /// <summary>
        /// JT808版本号
        /// </summary>
        public JT808Version Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">内存块</param>
        /// <param name="version">版本号:默认2013</param>
        public JT808MessagePackWriter(Span<byte> buffer, JT808Version version= JT808Version.JTT2013)
        {
            this.writer = new JT808BufferWriter(buffer);
            Version = version;
        }
        /// <summary>
        /// 编码后的数组
        /// </summary>
        /// <returns></returns>
        public byte[] FlushAndGetEncodingArray()
        {
            return writer.Written.Slice(writer.BeforeCodingWrittenPosition).ToArray();
        }
        /// <summary>
        /// 编码后的内存块
        /// </summary>
        /// <returns></returns>
        public ReadOnlySpan<byte> FlushAndGetEncodingReadOnlySpan()
        {
            return writer.Written.Slice(writer.BeforeCodingWrittenPosition);
        }
        /// <summary>
        /// 获取实际写入的内存块
        /// </summary>
        /// <returns></returns>
        public ReadOnlySpan<byte> FlushAndGetRealReadOnlySpan()
        {
            return writer.Written;
        }
        /// <summary>
        /// 获取实际写入的数组
        /// </summary>
        /// <returns></returns>
        public byte[] FlushAndGetRealArray()
        {
            return writer.Written.ToArray();
        }
        /// <summary>
        /// 写入头标识
        /// </summary>
        public void WriteStart()=> WriteByte(JT808Package.BeginFlag);
        /// <summary>
        /// 写入尾标识
        /// </summary>
        public void WriteEnd() => WriteByte(JT808Package.EndFlag);
        /// <summary>
        /// 写入空标识,0x00
        /// </summary>
        /// <param name="position"></param>
        public void Nil(out int position)
        {
            position = writer.WrittenCount;
            var span = writer.Free;
            span[0] = 0x00;
            writer.Advance(1);
        }
        /// <summary>
        /// 跳过多少字节数
        /// </summary>
        /// <param name="count"></param>
        /// <param name="position">跳过前的内存位置</param>
        public void Skip(in int count, out int position)
        {
            position = writer.WrittenCount;
            var span = writer.Free;
            for (var i = 0; i < count; i++)
            {
                span[i] = 0x00;
            }
            writer.Advance(count);
        }
        /// <summary>
        /// 跳过多少字节数
        /// </summary>
        /// <param name="count"></param>
        /// <param name="position">跳过前的内存位置</param>
        /// <param name="fullValue">用什么数值填充跳过的内存块</param>
        public void Skip(in int count,out int position, in byte fullValue = 0x00)
        {
            position = writer.WrittenCount;
            var span = writer.Free;
            for (var i = 0; i < count; i++)
            {
                span[i] = fullValue;
            }
            writer.Advance(count);
        }
        /// <summary>
        /// 写入一个字符
        /// </summary>
        /// <param name="value"></param>
        public void WriteChar(in char value)
        {
            var span = writer.Free;
            span[0] = (byte)value;
            writer.Advance(1);
        }
        /// <summary>
        /// 写入一个字节
        /// </summary>
        /// <param name="value"></param>
        public void WriteByte(in byte value)
        {
            var span = writer.Free;
            span[0] = value;
            writer.Advance(1);
        }
        /// <summary>
        /// 写入两个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt16(in short value)
        {
            BinaryPrimitives.WriteInt16BigEndian(writer.Free, value);
            writer.Advance(2);
        }
        /// <summary>
        /// 写入两个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInt16(in ushort value)
        {
            BinaryPrimitives.WriteUInt16BigEndian(writer.Free, value);
            writer.Advance(2);
        }
        /// <summary>
        /// 写入四个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt32(in int value)
        {
            BinaryPrimitives.WriteInt32BigEndian(writer.Free, value);
            writer.Advance(4);
        }
        /// <summary>
        /// 写入四个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInt32(in uint value)
        {
            BinaryPrimitives.WriteUInt32BigEndian(writer.Free, value);
            writer.Advance(4);
        }
        /// <summary>
        /// 写入八个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInt64(in ulong value)
        {
            BinaryPrimitives.WriteUInt64BigEndian(writer.Free, value);
            writer.Advance(8);
        }
        /// <summary>
        /// 写入八个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt64(in long value)
        {
            BinaryPrimitives.WriteInt64BigEndian(writer.Free, value);
            writer.Advance(8);
        }
        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value"></param>
        public void WriteString(in string value)
        {
            byte[] codeBytes = JT808Constants.Encoding.GetBytes(value);
            codeBytes.CopyTo(writer.Free);
            writer.Advance(codeBytes.Length);
        }
        /// <summary>
        /// 写入数组
        /// </summary>
        /// <param name="src"></param>
        public void WriteArray(in ReadOnlySpan<byte> src)
        {
            src.CopyTo(writer.Free);
            writer.Advance(src.Length);
        }
        /// <summary>
        /// 根据内存定位,反写两个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteUInt16Return(in ushort value, in int position)
        {
            BinaryPrimitives.WriteUInt16BigEndian(writer.Written.Slice(position, 2), value);
        }
        /// <summary>
        /// 根据内存定位,反写两个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteInt16Return(in short value, in int position)
        {
            BinaryPrimitives.WriteInt16BigEndian(writer.Written.Slice(position, 2), value);
        }
        /// <summary>
        /// 根据内存定位,反写四个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteInt32Return(in int value, in int position)
        {
            BinaryPrimitives.WriteInt32BigEndian(writer.Written.Slice(position, 4), value);
        }
        /// <summary>
        /// 根据内存定位,反写四个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteUInt32Return(in uint value, in int position)
        {
            BinaryPrimitives.WriteUInt32BigEndian(writer.Written.Slice(position, 4), value);
        }

        /// <summary>
        /// 根据内存定位,反写八个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteInt64Return(in long value, in int position)
        {
            BinaryPrimitives.WriteInt64BigEndian(writer.Written.Slice(position, 8), value);
        }
        /// <summary>
        /// 根据内存定位,反写八个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteUInt64Return(in ulong value, in int position)
        {
            BinaryPrimitives.WriteUInt64BigEndian(writer.Written.Slice(position, 8), value);
        }
        /// <summary>
        /// 根据内存定位,反写1个字节的数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteByteReturn(in byte value, in int position)
        {
            writer.Written[position] = value;
        }
        /// <summary>
        /// 根据内存定位,反写BCD编码数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        /// <param name="position"></param>
        public void WriteBCDReturn(in string value, in int len, in int position)
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
        /// <summary>
        /// 根据内存定位,反写一串字符串数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteStringReturn(in string value, in int position)
        {
            Span<byte> codeBytes = JT808Constants.Encoding.GetBytes(value);
            codeBytes.CopyTo(writer.Written.Slice(position));
        }
        /// <summary>
        /// 根据内存定位,反写一组数组数据
        /// </summary>
        /// <param name="src"></param>
        /// <param name="position"></param>
        public void WriteArrayReturn(in ReadOnlySpan<byte> src, in int position)
        {
            src.CopyTo(writer.Written.Slice(position));
        }

        /// <summary>
        /// 整型数据转为BCD BYTE
        /// 为了兼容int类型，不使用byte做参数
        /// 支持0xFF一个字节的转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte IntToBcd(int value)
        {
            byte result = 0;
            if (value <= 0xFF)
            {
                //19 00010011
                //0001 1001
                int high = value / 10;
                int low = value % 10;
                result = (byte)(high << 4 | low); 
            }
            return result;
        }

        /// <summary>
        /// 整型数据转为BCD BYTE[]
        /// </summary>
        /// <param name="value">整数值</param>
        /// <param name="list">bytes</param>
        /// <param name="count">字节数>=整数值</param>
        public void IntToBcd(int value, Span<byte> list, int count)
        {
            int level = count - 1;
            var high = value / 100;
            var low = value % 100;
            if (high > 0)
            {
                IntToBcd(high, list, --count);
            }
            byte res = (byte)(((low / 10) << 4) + (low % 10));
            list[level] = res;
        }

        /// <summary>
        /// 整型数据转为BCD BYTE[]
        /// </summary>
        /// <param name="value">整数值</param>
        /// <param name="list">bytes</param>
        /// <param name="byteCount">字节数>=整数值</param>
        public void IntToBcd(long value, Span<byte> list, int byteCount)
        {
            int level = byteCount - 1;
            if (level < 0) return;
            var high = value / 100;
            var low = value % 100;
            if (high > 0)
            {
                IntToBcd(high, list, --byteCount);
            }
            byte res = (byte)(((low / 10) << 4) + (low % 10));
            list[level] = res;
        }

        /// <summary>
        /// 写入BCD编码数据
        /// </summary>
        /// <param name="value">整数值</param>
        /// <param name="byteCount">字节数>=整数值</param>
        public void WriteBCD(in long value, in int byteCount)
        {
            var span = writer.Free;
            IntToBcd(value, writer.Free, byteCount);
            writer.Advance(byteCount);
        }

        /// <summary>
        /// 写入BCD编码数据
        /// </summary>
        /// <param name="value">整数值</param>
        /// <param name="count">字节数>=整数值</param>
        public void WriteBCD(in int value, in int count)
        {
            var span = writer.Free;
            IntToBcd(value, writer.Free, count);
            writer.Advance(count);
        }

        /// <summary>
        /// 写入六个字节的日期类型,yyMMddHHmmss
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        [Obsolete("use WriteDateTime_yyMMddHHmmss")]
        public void WriteDateTime6(in DateTime value, in int fromBase = 16)
        {
            var span = writer.Free;
            int yy = value.Year - JT808Constants.DateLimitYear;
            span[0] = IntToBcd(yy);
            span[1] = IntToBcd(value.Month);
            span[2] = IntToBcd(value.Day);
            span[3] = IntToBcd(value.Hour);
            span[4] = IntToBcd(value.Minute);
            span[5] = IntToBcd(value.Second);
            writer.Advance(6);
        }

        /// <summary>
        /// 写入六个字节的日期类型,yyMMddHHmmss
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime_yyMMddHHmmss(in DateTime value)
        {
            var span = writer.Free;
            int yy = value.Year - JT808Constants.DateLimitYear;
            span[0] = IntToBcd(yy);
            span[1] = IntToBcd(value.Month);
            span[2] = IntToBcd(value.Day);
            span[3] = IntToBcd(value.Hour);
            span[4] = IntToBcd(value.Minute);
            span[5] = IntToBcd(value.Second);
            writer.Advance(6);
        }

        /// <summary>
        /// 写入六个字节的可空日期类型,yyMMddHHmmss
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        [Obsolete("use WriteDateTime_yyMMddHHmmss")]
        public void WriteDateTime6(in DateTime? value, in int fromBase = 16)
        {
            var span = writer.Free;
            if (value == null || value.HasValue)
            {
                span[0] = 0;
                span[1] = 0;
                span[2] = 0;
                span[3] = 0;
                span[4] = 0;
                span[5] = 0;
            }
            else
            {
                int yy = value.Value.Year - JT808Constants.DateLimitYear;
                span[0] = IntToBcd(yy);
                span[1] = IntToBcd(value.Value.Month);
                span[2] = IntToBcd(value.Value.Day);
                span[3] = IntToBcd(value.Value.Hour);
                span[4] = IntToBcd(value.Value.Minute);
                span[5] = IntToBcd(value.Value.Second);
            }
            writer.Advance(6);
        }

        /// <summary>
        /// 写入六个字节的可空日期类型,yyMMddHHmmss
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime_yyMMddHHmmss(in DateTime? value)
        {
            var span = writer.Free;
            if (value == null || value.HasValue)
            {
                span[0] = 0;
                span[1] = 0;
                span[2] = 0;
                span[3] = 0;
                span[4] = 0;
                span[5] = 0;
            }
            else
            {
                int yy = value.Value.Year - JT808Constants.DateLimitYear;
                span[0] = IntToBcd(yy);
                span[1] = IntToBcd(value.Value.Month);
                span[2] = IntToBcd(value.Value.Day);
                span[3] = IntToBcd(value.Value.Hour);
                span[4] = IntToBcd(value.Value.Minute);
                span[5] = IntToBcd(value.Value.Second);
            }
            writer.Advance(6);
        }

        /// <summary>
        /// 写入五个字节的日期类型,HH-mm-ss-msms或HH-mm-ss-fff
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        [Obsolete("use WriteDateTime_HHmmssfff")]
        public void WriteDateTime5(in DateTime value, in int fromBase = 16)
        {
            var span = writer.Free;
            span[0] = IntToBcd(value.Hour);
            span[1] = IntToBcd(value.Minute);
            span[2] = IntToBcd(value.Second);
            writer.Advance(3);
            IntToBcd(value.Millisecond, writer.Free, 2);
            writer.Advance(2);
        }

        /// <summary>
        /// 写入五个字节的日期类型,HH-mm-ss-msms或HH-mm-ss-fff
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime_HHmmssfff(in DateTime value)
        {
            var span = writer.Free;
            span[0] = IntToBcd(value.Hour);
            span[1] = IntToBcd(value.Minute);
            span[2] = IntToBcd(value.Second);
            writer.Advance(3);
            IntToBcd(value.Millisecond, writer.Free, 2);
            writer.Advance(2);
        }

        /// <summary>
        /// 写入五个字节的可空日期类型,HH-mm-ss-msms或HH-mm-ss-fff
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        [Obsolete("use WriteDateTime_HHmmssfff")]
        public void WriteDateTime5(in DateTime? value, in int fromBase = 16)
        {
            if (value == null || value.HasValue)
            {
                var span = writer.Free;
                span[0] = 0;
                span[1] = 0;
                span[2] = 0;
                span[3] = 0;
                span[4] = 0;
                writer.Advance(5);
            }
            else
            {
                var span = writer.Free;
                span[0] = IntToBcd(value.Value.Hour);
                span[1] = IntToBcd(value.Value.Minute);
                span[2] = IntToBcd(value.Value.Second);
                writer.Advance(3);
                IntToBcd(value.Value.Millisecond, writer.Free, 2);
                writer.Advance(2);
            }
        }

        /// <summary>
        /// 写入五个字节的可空日期类型,HH-mm-ss-msms或HH-mm-ss-fff
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime_HHmmssfff(in DateTime? value)
        {
            if (value == null || value.HasValue)
            {
                var span = writer.Free;
                span[0] = 0;
                span[1] = 0;
                span[2] = 0;
                span[3] = 0;
                span[4] = 0;
                writer.Advance(5);
            }
            else
            {
                var span = writer.Free;
                span[0] = IntToBcd(value.Value.Hour);
                span[1] = IntToBcd(value.Value.Minute);
                span[2] = IntToBcd(value.Value.Second);
                writer.Advance(3);
                IntToBcd(value.Value.Millisecond, writer.Free, 2);
                writer.Advance(2);
            }
        }

        /// <summary>
        /// 写入UTC日期类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteUTCDateTime(in DateTime value)
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
        /// 写入四个字节的日期类型,YYYYMMDD BCD[4] 数据形如：20200101
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        [Obsolete("use WriteDateTime_YYYYMMDD")]
        public void WriteDateTime4(in DateTime value, in int fromBase = 16)
        {
            IntToBcd(value.Year, writer.Free, 2);
            writer.Advance(2);
            var span = writer.Free;
            span[0] = IntToBcd(value.Month);
            span[1] = IntToBcd(value.Day);
            writer.Advance(2);
        }

        /// <summary>
        /// 写入四个字节的日期类型,YYYYMMDD BCD[4] 数据形如：20200101
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime_YYYYMMDD(in DateTime value)
        {
            IntToBcd(value.Year, writer.Free, 2);
            writer.Advance(2);
            var span = writer.Free;
            span[0] = IntToBcd(value.Month);
            span[1] = IntToBcd(value.Day);
            writer.Advance(2);
        }

        /// <summary>
        /// 写入四个字节的可空日期类型,YYYYMMDD BCD[4]数据形如:20200101
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        [Obsolete("use WriteDateTime_YYYYMMDD")]
        public void WriteDateTime4(in DateTime? value, in int fromBase = 16)
        {
            if (value==null || value.HasValue)
            {
                var span = writer.Free;
                span[0] = 0;
                span[1] = 0;
                span[2] = 0;
                span[3] = 0;
                writer.Advance(4);
            }
            else
            {
                IntToBcd(value.Value.Year, writer.Free, 2);
                writer.Advance(2);
                var span = writer.Free;
                span[0] = IntToBcd(value.Value.Month);
                span[1] = IntToBcd(value.Value.Day);
                writer.Advance(2);
            }    
        }

        /// <summary>
        /// 写入四个字节的可空日期类型,YYYYMMDD BCD[4]数据形如:20200101
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime_YYYYMMDD(in DateTime? value)
        {
            if (value == null || value.HasValue)
            {
                var span = writer.Free;
                span[0] = 0;
                span[1] = 0;
                span[2] = 0;
                span[3] = 0;
                writer.Advance(4);
            }
            else
            {
                IntToBcd(value.Value.Year, writer.Free, 2);
                writer.Advance(2);
                var span = writer.Free;
                span[0] = IntToBcd(value.Value.Month);
                span[1] = IntToBcd(value.Value.Day);
                writer.Advance(2);
            }
        }

        /// <summary>
        /// 写入三个字节的日期类型,YYMMDD 数据形如：20200101
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        [Obsolete("use WriteDateTime_YYMMDD")]
        public void WriteDateTime3(in DateTime value, in int fromBase = 16)
        {
            var span = writer.Free;
            int yy = value.Year - JT808Constants.DateLimitYear;
            span[0] = IntToBcd(yy);
            span[1] = IntToBcd(value.Month);
            span[2] = IntToBcd(value.Day);
            writer.Advance(3);
        }
        /// <summary>
        /// 写入三个字节的日期类型,YYMMDD 数据形如：200101
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime_YYMMDD(in DateTime value)
        {
            var span = writer.Free;
            int yy = value.Year - JT808Constants.DateLimitYear;
            span[0] = IntToBcd(yy);
            span[1] = IntToBcd(value.Month);
            span[2] = IntToBcd(value.Day);
            writer.Advance(3);
        }
        /// <summary>
        /// 写入三个字节的可空日期类型,YYMMDD 数据形如：200101
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromBase"></param>
        [Obsolete("use WriteDateTime_YYMMDD")]
        public void WriteDateTime3(in DateTime? value, in int fromBase = 16)
        {
            var span = writer.Free;
            if (value == null || value.HasValue)
            {
                span[0] = 0;
                span[1] = 0;
                span[2] = 0;
            }
            else
            {
                int yy = value.Value.Year - JT808Constants.DateLimitYear;
                span[0] = IntToBcd(yy);
                span[1] = IntToBcd(value.Value.Month);
                span[2] = IntToBcd(value.Value.Day);
            }
            writer.Advance(3);
        }
        /// <summary>
        /// 写入三个字节的可空日期类型,YYMMDD 数据形如：20200101
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime_YYMMDD(in DateTime? value)
        {
            var span = writer.Free;
            if (value == null || value.HasValue)
            {
                span[0] = 0;
                span[1] = 0;
                span[2] = 0;
            }
            else
            {
                int yy = value.Value.Year - JT808Constants.DateLimitYear;
                span[0] = IntToBcd(yy);
                span[1] = IntToBcd(value.Value.Month);
                span[2] = IntToBcd(value.Value.Day);
            }
            writer.Advance(3);
        }
        /// <summary>
        /// 将指定内存块进行或运算并写入一个字节
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void WriteXor(in int start, in int end)
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
        /// <summary>
        /// 将指定内存块进行或运算并写入一个字节
        /// </summary>
        /// <param name="start"></param>
        public void WriteXor(in int start)
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
        /// <summary>
        /// 将内存块进行或运算并写入一个字节
        /// </summary>
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
        /// <summary>
        /// 写入BCD编码数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        public void WriteBCD(in string value, in int len)
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
        /// <summary>
        /// 写入Hex编码数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        public void WriteHex(string value, in int len)
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
        /// <summary>
        /// 写入ASCII编码数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteASCII(in string value)
        {
            var spanFree = writer.Free;
            var bytes = Encoding.ASCII.GetBytes(value).AsSpan();
            bytes.CopyTo(spanFree);
            writer.Advance(bytes.Length);
        }
        /// <summary>
        /// 将内存块进行808转义处理
        /// </summary>
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
        /// <summary>
        /// 将内存块进行808转义处理
        /// </summary>
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
        /// 写入数值类型，数字编码 大端模式、高位在前
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        public void WriteBigNumber(in string value, int len)
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
        /// <summary>
        /// 将字符串写入并写入一个\0作为结尾
        /// </summary>
        /// <param name="value"></param>
        public void WriteStringEndChar0(string value)
        {
            WriteString(value);
            WriteChar('\0');
        }
        /// <summary>
        /// 获取当前内存块写入的位置
        /// </summary>
        /// <returns></returns>
        public int GetCurrentPosition()
        {
            return writer.WrittenCount;
        }
        /// <summary>
        /// 写入JT19056校验码
        /// </summary>
        /// <param name="currentPosition"></param>
        public void WriteCarDVRCheckCode(in int currentPosition)
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
