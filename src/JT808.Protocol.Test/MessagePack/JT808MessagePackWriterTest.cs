using JT808.Protocol.MessagePack;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Buffers;
using JT808.Protocol.Enums;
using System.Buffers.Binary;

namespace JT808.Protocol.Test.MessagePack
{
    public class JT808MessagePackWriterTest
    {
        [Fact]
        public void WriteEncodeTest()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteByte(0x7E);
            msgpackWriter.WriteByte(0x7D);
            msgpackWriter.WriteByte(0x7E);
            msgpackWriter.WriteByte(0x7D);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //===========output=========
            //Unencode:
            //7E 7E 7D 7E 7D 7E
            //Encode
            //7E 7D 02 7D 01 7D 02 7D 01 7E
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 7E 7D 7E 7D 7E 7E 7D 02 7D 01 7D 02 7D 01 7E".Replace(" ", ""), realBytes);
        }

        [Fact]
        public void WriteEncodeTest1()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteByte(0x7E);
            msgpackWriter.WriteByte(0x7D);
            msgpackWriter.WriteByte(0x7E);
            msgpackWriter.WriteByte(0x7D);
            msgpackWriter.WriteFullEncode();
            //===========output=========
            //Unencode:
            //7E 7D 7E 7D 
            //Encode
            //7D 02 7D 01 7D 02 7D 01
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 7D 7E 7D 7D 02 7D 01 7D 02 7D 01".Replace(" ", ""), realBytes);
        }

        [Fact]
        public void WriteDateTimeTest()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteDateTime_YYYYMMDD(DateTime.Parse("2019-06-19 23:23:23"));
            msgpackWriter.WriteDateTime_HHmmssfff(DateTime.Parse("2019-06-19 23:23:23.123"));
            msgpackWriter.WriteDateTime_yyMMddHHmmss(DateTime.Parse("2019-06-19 23:23:23"));
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //===========output=========
            //WriteDateTime4=>YYYYMMDD=>20 19 06 19
            //WriteDateTime5=>HH-mm-ss-fff|HH-mm-ss-msms=>23 23 23 01 23
            //WriteDateTime6=>yyMMddHHmmss=>19 23 23 23
            //Unencode:
            //7E2019061923232312301906192323237E
            //Encode
            //7E 20 19 06 19 23 23 23 01 23 19 06 19 23 23 23 7E
            var encodeBytes = msgpackWriter.FlushAndGetEncodingArray().ToHexString();
            Assert.Equal("7E2019061923232301231906192323237E".Replace(" ", ""), encodeBytes);
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E2019061923232301231906192323237E7E2019061923232301231906192323237E", realBytes);
        }

        [Fact]
        public void WriteDateTimeNullTest()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteDateTime_YYYYMMDD(null);
            msgpackWriter.WriteDateTime_HHmmssfff(null);
            msgpackWriter.WriteDateTime_yyMMddHHmmss(null);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //===========output=========
            //WriteDateTime4=>YYYYMMDD=>00 00 00 00
            //WriteDateTime5=>HH-mm-ss-fff|HH-mm-ss-msms=>00 00 00 00 00
            //WriteDateTime6=>yyMMddHHmmss=>00 00 00 00
            var encodeBytes = msgpackWriter.FlushAndGetEncodingArray().ToHexString();
            Assert.Equal("7E0000000000000000000000000000007E".Replace(" ", ""), encodeBytes);
        }

        [Fact]
        public void WriteUTCDateTimeTest()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteUTCDateTime(DateTime.Parse("2019-06-21 23:23:23"));
            var encodeBytes = msgpackWriter.FlushAndGetEncodingArray().ToHexString();
            Assert.Equal("000000005D0CF66B", encodeBytes);
        }

        [Fact]
        public void WriteBCDTest1()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteBCD("1234567890", 10);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //7E 12 34 56 78 90 7E 7E 12 34 56 78 90 7E
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 12 34 56 78 90 7E 7E 12 34 56 78 90 7E".Replace(" ", ""), realBytes);
        }

        [Fact]
        public void WriteBCDTest2()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteBCD("1234567890", 5);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //7E 12 34 7E 7E 12 34 7E
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 12 34 7E 7E 12 34 7E".Replace(" ", ""), realBytes);
        }

        [Fact]
        public void WriteBCDTest3()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteBCD("123", 5);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //7E 00 12 7E 7E 00 12 7E
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 00 12 7E 7E 00 12 7E".Replace(" ", ""), realBytes);
        }

        [Theory]
        [InlineData("smallchi(Koike)")]
        public void WriteStringTest(string str)
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteString(str);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            byte[] strBytes = JT808Constants.Encoding.GetBytes(str);
            var strHex = strBytes.ToHexString();
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            //7E736D616C6C636869284B6F696B65297E7E736D616C6C636869284B6F696B65297E
            Assert.StartsWith(strHex, realBytes.Substring(2));
            Assert.Equal("7E736D616C6C636869284B6F696B65297E7E736D616C6C636869284B6F696B65297E", realBytes);
        }

        [Theory]
        [InlineData("ABCDEF1234")]
        public void WriteHexTest(string hexStr)
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteHex(hexStr, 16);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            //7E0000000000000000000000ABCDEF12347E7E0000000000000000000000ABCDEF12347E
            Assert.StartsWith("0000000000000000000000ABCDEF1234", realBytes.Substring(2));
            Assert.Equal("7E0000000000000000000000ABCDEF12347E7E0000000000000000000000ABCDEF12347E", realBytes);
        }

        [Theory]
        [InlineData(new byte[] { 0x01, 0x02, 0x03 })]
        public void WriteArrayTest(byte[] dara)
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteArray(dara);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //===========output=========
            //Unencode:
            //7E 01 02 03 7E
            //Encode
            //7E 01 02 03 7E
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 01 02 03 7E 7E 01 02 03 7E".Replace(" ",""), realBytes);
        }

        [Fact]
        public void WriteNumericalTest()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteByte(0x01);
            msgpackWriter.WriteUInt16(16);
            msgpackWriter.WriteInt32(32);
            msgpackWriter.WriteUInt32(64);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //===========output=========
            //Unencode:
            //7E 01 00 10 00 00 00 20 00 00 00 40 7E
            //Encode
            //7E 01 00 10 00 00 00 20 00 00 00 40 7E
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 01 00 10 00 00 00 20 00 00 00 40 7E 7E 01 00 10 00 00 00 20 00 00 00 40 7E".Replace(" ", ""), realBytes);
        }

        [Theory]
        [InlineData(5)]
        public void SkipTest(int count)
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.Skip(count, out int position);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //===========output=========
            //Unencode:
            //7E 00 00 00 00 00 7E
            //Encode
            //7E 00 00 00 00 00 7E
            Assert.Equal(1, position);
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 00 00 00 00 00 7E 7E 00 00 00 00 00 7E".Replace(" ",""), realBytes);
        }

        [Theory]
        [InlineData(5,0xFF)]
        public void CustomSkipTest(int count,byte fullValue)
        {
            
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.Skip(count, out int position, fullValue);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //===========output=========
            //Unencode:
            //7E FF FF FF FF FF 7E
            //Encode
            //7E FF FF FF FF FF 7E
            Assert.Equal(1, position);
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E FF FF FF FF FF 7E 7E FF FF FF FF FF 7E".Replace(" ", ""), realBytes);
        }

        [Fact]
        public void NilTest()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.Nil(out int position);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            Assert.Equal(1, position);
            //===========output=========
            //Unencode:
            //7E 00 7E
            //Encode
            //7E 00 7E
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 00 7E 7E 00 7E".Replace(" ", ""), realBytes);
        }

        [Theory]
        [InlineData(1, 12)]
        public void WriteXorTest1(int start,int end)
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteArray("02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01".ToHexBytes());
            msgpackWriter.WriteXor(start, end);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.NotEqual("13", realBytes.Substring(realBytes.Length-4, 2));
        }

        [Theory]
        [InlineData(12, 1)]
        public void WriteXorTest2(int start, int end)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                byte[] array = new byte[4096];
                var msgpackWriter = new JT808MessagePackWriter(array);
                msgpackWriter.WriteStart();
                msgpackWriter.WriteXor(start, end);
                msgpackWriter.WriteEnd();
                msgpackWriter.WriteEncode();
                var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            });
        }

        [Fact]
        public void WriteXorTest3()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteArray("02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01".ToHexBytes());
            msgpackWriter.WriteXor();
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("13", realBytes.Substring(realBytes.Length - 4, 2));
        }

        [Theory]
        [InlineData(1,0x02,
                    2,8,
                    4,9,
                    6,"654321",
                    3,new byte[] { 0x01,0x02,0x03})]
        public void WriteReturnTest(
            int skipbyte, byte writeNewByte,
            int skipInt16, ushort writeNewInt16,
            int skipInt32, int writeNewInt32,
            int skipString, string writeNewString,
            int skipArray3, byte[] writeNewArray)
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.Skip(skipbyte, out var position1);
            msgpackWriter.Skip(skipInt16, out var position2);
            msgpackWriter.Skip(skipInt32, out var position3);
            msgpackWriter.Skip(skipString, out var position4);
            msgpackWriter.Skip(skipArray3, out var position5);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //===========skip output=========
            //Unencode:
            //7E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 7E
            //Encode
            //7E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 7E
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 7E 7E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 7E".Replace(" ", ""), realBytes);
            msgpackWriter.WriteByteReturn(writeNewByte, position1);
            msgpackWriter.WriteUInt16Return(writeNewInt16, position2);
            msgpackWriter.WriteInt32Return(writeNewInt32, position3);
            msgpackWriter.WriteBCDReturn(writeNewString, skipString, position4);
            msgpackWriter.WriteArrayReturn(writeNewArray, position5);
            //===========write return output=========
            //7E 02 00 08 00 00 00 09 65 43 21 00 00 00 01 02 03 7E
            var writeRealBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 02 00 08 00 00 00 09 65 43 21 00 00 00 01 02 03 7E 7E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 7E".Replace(" ", ""), writeRealBytes);
        }

        [Fact]
        public void WriteUInt64Test()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteUInt64(1008611);
            var hex = msgpackWriter.FlushAndGetEncodingArray().ToHexString();
            //00 00 00 00 00 0F 63 E3
            Assert.Equal("00 00 00 00 00 0F 63 E3".Replace(" ", ""), hex);
        }

        [Theory]
        [InlineData("123456789")]
        public void WriteBigNumberTest(string numStr)
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteBigNumber(numStr, numStr.Length);
            var hex = msgpackWriter.FlushAndGetEncodingArray().ToHexString();
            Assert.Equal("0000000000075BCD15", hex);
        }

        [Theory]
        [InlineData(100000)]
        //[InlineData(1000000)]
        //[InlineData(10000000)]
        //[InlineData(100000000)]
        public void ArrayPoolTest1(int count)
        {
           var arrayPool = ArrayPool<byte>.Create();
            while (count>=0)
            {
                var buffer = arrayPool.Rent(65535);
                var msgpackWriter = new JT808MessagePackWriter(buffer);
                try
                {
                    msgpackWriter.WriteStart();
                    msgpackWriter.WriteInt32(16);
                    msgpackWriter.WriteEnd();
                    msgpackWriter.WriteEncode();
                    var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
                    Assert.Equal("7E 00 00 00 10 7E 7E 00 00 00 10 7E".Replace(" ", ""), realBytes);
                }
                catch (Exception)
                {

                }
                finally
                {
                    arrayPool.Return(buffer);
                    count--;
                }
            }
        }

        [Fact]
        public void WriteASCII()
        {
            byte[] array = new byte[4096];
            byte[] array1 = new byte[] { 0x53,0x56,0x31,0x2E,0x31,0x2E,0x30 };
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteASCII("SV1.1.0");
            var writeRealBytes = msgpackWriter.FlushAndGetRealArray();
            Assert.Equal(array1, writeRealBytes);
        }

        [Fact]
        public void CompositeTest1()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteByte(0x01);
            msgpackWriter.WriteByte(0x7E);
            msgpackWriter.WriteByte(0x7d);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            var encodeBytes = msgpackWriter.FlushAndGetEncodingArray().ToHexString();
            Assert.Equal("7E017D027D017E", encodeBytes);
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E017E7D7E7E017D027D017E", realBytes);
        }

        [Fact]
        public void CompositeTest2()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStart();
            msgpackWriter.WriteByte(0x01);
            msgpackWriter.WriteByte(0x7E);
            msgpackWriter.Nil(out int nilPosition);
            Assert.Equal(3, nilPosition);
            msgpackWriter.WriteByte(0x7d);
            msgpackWriter.WriteBCD("123456789", 10);
            msgpackWriter.Skip(5, out int skipPostion);
            Assert.Equal(10, skipPostion);
            msgpackWriter.WriteEnd();
            msgpackWriter.WriteEncode();
            //===========output=========
            //Unencode:
            //7E 01 7E 00 7D 01 23 45 67 89 00 00 00 00 00 7E
            //Encode
            //7E 01 7D 02 00 7D 01 01 23 45 67 89 00 00 00 00 00 7E
            //7E 01 7D 02 00 7D 01 01 00 23 00 45 00 00 00 00 00 7E
            var encodeBytes = msgpackWriter.FlushAndGetEncodingArray().ToHexString();
            Assert.Equal("7E 01 7D 02 00 7D 01 01 23 45 67 89 00 00 00 00 00 7E".Replace(" ", ""), encodeBytes);
            var realBytes = msgpackWriter.FlushAndGetRealArray().ToHexString();
            Assert.Equal("7E 01 7E 00 7D 01 23 45 67 89 00 00 00 00 00 7E 7E 01 7D 02 00 7D 01 01 23 45 67 89 00 00 00 00 00 7E".Replace(" ", ""), realBytes);
        }

        [Fact]
        public void VersionTest1()
        {
            byte[] array = new byte[4096];
            var msgpackWriter = new JT808MessagePackWriter(array);
            Assert.Equal(JT808Version.JTT2013, msgpackWriter.Version);
            msgpackWriter.Version = JT808Version.JTT2019;
            Assert.Equal(JT808Version.JTT2019, msgpackWriter.Version);
        }

        [Fact]
        public void WriteInt16Test1()
        {
            byte[] array1 = new byte[2];
            byte[] array2= new byte[2];
            BinaryPrimitives.WriteInt16BigEndian(array1, -1233);
            short a = -1233;
            BinaryPrimitives.WriteUInt16BigEndian(array2, (ushort)a);
            Assert.Equal(array1, array2);
        }

        [Fact]
        public void WriteStringEndChar0Test()
        {
            byte[] array = new byte[22];
            var msgpackWriter = new JT808MessagePackWriter(array);
            msgpackWriter.WriteStringEndChar0("smallchi(koike)");
            var hex = msgpackWriter.FlushAndGetEncodingArray().ToHexString();
            Assert.Equal("736D616C6C636869286B6F696B652900", hex);
        }

        [Fact]
        public void IntToBcdTest()
        {
            var bytes1 = new byte[5];
            Span<byte> buffer1 = new Span<byte>(bytes1);
            IntToBcd(123456700, buffer1, buffer1.Length);
            Assert.NotEqual(new byte[] { 0x12, 0x34, 0x56, 0x70,0x00 }, buffer1.ToArray());
            Assert.Equal(new byte[] { 0x01, 0x23, 0x45, 0x67,0x00 }, buffer1.ToArray());

            var bytes = new byte[6];
            Span<byte> buffer = new Span<byte>(bytes);
            IntToBcd(123456700, buffer, buffer.Length);
            Assert.Equal(new byte[] { 0x00, 0x01, 0x23, 0x45, 0x67, 0x00 }, buffer.ToArray());
        }

        [Fact]
        public void WriteBcdTest()
        {
            byte[] array = new byte[100];
            var msgpackWriter = new JT808MessagePackWriter(array);
            int val1 = 1234567890;
            long val2 = 123456789011;
            msgpackWriter.WriteBCD(val1, 5);
            msgpackWriter.WriteBCD(val2, 10);
            var result = msgpackWriter.FlushAndGetRealArray();
            Assert.Equal(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0x00, 0x00, 0x00, 0x00, 0x12, 0x34, 0x56, 0x78, 0x90, 0x11 }, result);
        }

        private void IntToBcd(int num, Span<byte> list, int count)
        {
            int level = count - 1;
            var high = num / 100;
            var low = num % 100;
            if (high > 0)
            {
                IntToBcd(high, list, --count);
            }
            byte res = (byte)(((low / 10) << 4) + (low % 10));
            list[level] = res;
        }
    }
}
