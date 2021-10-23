using JT808.Protocol.MessagePack;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Buffers;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test.MessagePack
{
    public class JT808MessagePackReaderTest
    {
        [Fact]
        public void JT808MessagePackReaderConstructorTest()
        {
            byte[] bytes = "7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            jT808MessagePackReader.Decode(new byte[4096]);
            Assert.Equal(0x13, jT808MessagePackReader.CalculateCheckXorCode);
            Assert.Equal(jT808MessagePackReader.SrcBuffer.Length, bytes.Length);
            byte[] bytes1 = "7E 02 00 00 26 12 34 56 78 90 12 00 7E 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 13 7E".ToHexBytes();
            //7E02000026123456789012007E000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D137D
            //7E02000026123456789012007E000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D137E
            var a = jT808MessagePackReader.Reader.ToArray().ToHexString();
            Assert.Equal(jT808MessagePackReader.Reader.ToArray(), bytes1);
        }

        [Fact]
        public void ReadEncodeTest()
        {
            byte[] bytes = "7E 7D 02 7D 01 7D 02 7D 01 7E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            jT808MessagePackReader.Decode(new byte[4096]);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            Assert.Equal(0x7E, jT808MessagePackReader.ReadByte());
            Assert.Equal(0x7D, jT808MessagePackReader.ReadByte());
            Assert.Equal(0x7E, jT808MessagePackReader.ReadByte());
            Assert.Equal(0x7D, jT808MessagePackReader.ReadByte());
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
            Assert.Equal(6, jT808MessagePackReader.ReaderCount);
        }

        [Fact]
        public void ReadEncodeTest1()
        {
            byte[] bytes = "7E 00 02 00 00 04 00 21 67 92 87 00 2B 7D 02 7E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
        }

        [Fact]
        public void ReadEncodeTest2()
        {
            byte[] bytes = "7E 00 02 00 00 04 00 21 67 92 87 00 2B 7D 02 7E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            jT808MessagePackReader.Decode();
            Assert.Equal(0x7E, jT808MessagePackReader.CalculateCheckXorCode);
        }


        [Fact]
        public void ReadDateTimeTest()
        {
            byte[] bytes = "7E2019061923232301231906192323237E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            Assert.Equal(DateTime.Parse("2019-06-19"), jT808MessagePackReader.ReadDateTime_YYYYMMDD());
            Assert.Equal(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,23,23,23,123), jT808MessagePackReader.ReadDateTime_HHmmssfff());
            Assert.Equal(DateTime.Parse("2019-06-19 23:23:23"), jT808MessagePackReader.ReadDateTime_yyMMddHHmmss());
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
        }

        [Fact]
        public void ReadDateTimeNullTest()
        {
            byte[] bytes = "7E0000000000000000000000000000007E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            Assert.Null(jT808MessagePackReader.ReadDateTimeNull_YYYYMMDD());
            Assert.Null(jT808MessagePackReader.ReadDateTimeNull_HHmmssfff());
            Assert.Null(jT808MessagePackReader.ReadDateTimeNull_yyMMddHHmmss());
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
        }


        [Theory]
        [InlineData("smallchi(Koike)")]
        public void ReadStringTest(string str)
        {
            byte[] bytes = "7E736D616C6C636869284B6F696B65297E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            Assert.Equal(str, jT808MessagePackReader.ReadString(str.Length));
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
        }

        [Fact]
        public void ReadASCIITest()
        {
            byte[] array1 = new byte[] { 0x53, 0x56, 0x31, 0x2E, 0x31, 0x2E, 0x30 };
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(array1);
            Assert.Equal("SV1.1.0", jT808MessagePackReader.ReadASCII(7));
        }

        [Theory]
        [InlineData("0000000000000000000000ABCDEF1234")]
        public void ReadHexTest(string hexStr)
        {
            byte[] bytes = "7E0000000000000000000000ABCDEF12347E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            //0000000000000000000000ABCDEF1234
            Assert.Equal(hexStr, jT808MessagePackReader.ReadHex(16));
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
        }

        [Fact]
        public void ReadUTCDateTimeTest()
        {
            byte[] bytes = "000000005D0CF66B".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(DateTime.Parse("2019-06-21 23:23:23"), jT808MessagePackReader.ReadUTCDateTime());
        }

        [Fact]
        public void ReadBCDTest1()
        {
            byte[] bytes = "7E 12 34 56 78 90 7E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            Assert.Equal("1234567890", jT808MessagePackReader.ReadBCD(10));
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
        }

        [Fact]
        public void ReadBCDTest2()
        {
            byte[] bytes = "7E 12 34 7E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            Assert.Equal("1234", jT808MessagePackReader.ReadBCD(5));
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
        }

        [Fact]
        public void ReadBCDTest3()
        {
            byte[] bytes = "7E 00 12 7E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            Assert.Equal("12", jT808MessagePackReader.ReadBCD(5));
            //Assert.Equal("0012", jT808MessagePackReader.ReadBCD(5));
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
        }

        [Fact]
        public void ReadArrayTest()
        {
            byte[] bytes = "7E 01 02 03 7E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            Assert.Equal(new byte[] { 0x01, 0x02, 0x03 }, jT808MessagePackReader.ReadArray(3).ToArray());
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
        }

        [Fact]
        public void ReadNumericalTest()
        {
            byte[] bytes = "7E 01 00 10 00 00 00 20 00 00 00 40 7E".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
            Assert.Equal(0x01, jT808MessagePackReader.ReadByte());
            Assert.Equal(16, jT808MessagePackReader.ReadUInt16());
            Assert.Equal(32, jT808MessagePackReader.ReadInt32());
            Assert.Equal((uint)64, jT808MessagePackReader.ReadUInt32());
            Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
        }

        [Fact]
        public void ReadUInt64Test()
        {
            byte[] bytes = "00 00 00 00 00 0F 63 E3".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal((ulong)1008611, jT808MessagePackReader.ReadUInt64());
        }

        [Theory]
        [InlineData("123456789")]
        public void ReadBigNumberTest(string numStr)
        {
            byte[] bytes = "0000000000075BCD15".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            Assert.Equal(numStr, jT808MessagePackReader.ReadBigNumber(numStr.Length));
        }

        [Fact]
        public void ReadContentTest()
        {
            var bytes = "7e080123000138123456782032000a000205060708090a0bffc400b5100002010303020403050504040000017d0101020300041105122131410613516107227114328191a1082342b1c11552d1f02433627282090a161718191a25262728292a3435363738393a434445464748494a535455565758595a636465666768696a737475767778797a838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae1e2e3e4e5e6e7e8e9eaf1f2f3f4f5f6f7f8f9faffc4001f0100030101010101010101010000000000000102030405060708090a0bffc400b51100020102040403040705040400010277000102031104052131061241510761711322328108144291a1b1c109233352f0156272d10a162434e125f11718191a262728292a35363738393a434445464748494a535455565758595a636465666768696a737475767778797a82838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae2e3e4e5e6e7e8e9eaf2f3f4f5f6f7f8f9faffdd00040000ffda000c03010002110311003f006c6a2a755ce299a5c942e0f35281c5004aa72314a54e38a07b8841ef4840a0673de21b4ff498ee402038dade991fe7f4acc110f4a0cd8ef2f1405cd01d45f2e9360a062edc5745616a6dad511861cfccff0053499512e056cf1460e3348a0ed4b8e338fc2819cb5edbfd9ee648b18556f97fdd3d3f4aafb4d332ea433a6573e9550d3131d18c9c558031c0a4083a503039a60c42c2984e4f4a06260d370690098ef4751400c615132d021868a621431a33480ef235e05595403eb54cbb0b8e7069dc0e3a9a41b12a024f4a9d40f4a18c5651e951c88179268194ee614b989a2719461ffea35cfdcda4b6b2ed71c1e55874345c96ba91819a704c50217613db349b39031c9e945c66a69ba794713cebf30fb8be9ee6b540c1e948a48760e3a526d2dc77a0a144471d297cb623a71484646bb685234b81d01d8e7d018f43f9ff003ac16386c552225b8300c2a84c8c8c4ed247b502616cc0517e".ToHexBytes();
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes);
            JT808Package jT808Package = new JT808Package();
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.SkipCRCCode = true;
            var package= jT808Package.Deserialize(ref jT808MessagePackReader, jT808Config);
        }

        [Theory]
        [InlineData(100000)]
        //[InlineData(1000000)]
        //[InlineData(10000000)]
        //[InlineData(100000000)]
        public void ArrayPoolTest1(int count)
        {
            var arrayPool = ArrayPool<byte>.Create();
            while (count >= 0)
            {
                var buffer = arrayPool.Rent(4096);
                byte[] bytes = "7E 7D 02 7D 01 7D 02 7D 01 7E".ToHexBytes();
                var jT808MessagePackReader = new JT808MessagePackReader(bytes);
                jT808MessagePackReader.Decode(buffer);
                try
                {
                    Assert.Equal(JT808Package.BeginFlag, jT808MessagePackReader.ReadStart());
                    Assert.Equal(0x7E, jT808MessagePackReader.ReadByte());
                    Assert.Equal(0x7D, jT808MessagePackReader.ReadByte());
                    Assert.Equal(0x7E, jT808MessagePackReader.ReadByte());
                    Assert.Equal(0x7D, jT808MessagePackReader.ReadByte());
                    Assert.Equal(JT808Package.EndFlag, jT808MessagePackReader.ReadEnd());
                    Assert.Equal(6, jT808MessagePackReader.ReaderCount);
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
        public void VersionTest1()
        {
            byte[] array = new byte[4096];
            var reader = new JT808MessagePackReader(array);
            Assert.Equal(JT808Version.JTT2013, reader.Version);
            reader.Version = JT808Version.JTT2019;
            Assert.Equal(JT808Version.JTT2019, reader.Version);
        }       

        [Fact]
        public void ReadStringEndChar0Test1()
        {
            byte[] array = "736D616C6C636869286B6F696B652900".ToHexBytes();
            var reader = new JT808MessagePackReader(array);
            var str = reader.ReadStringEndChar0();
            Assert.Equal("smallchi(koike)", str);
            Assert.Equal(16, reader.ReaderCount);
        }        

        [Fact]
        public void ReadVirtualArrayEndChar0Test1()
        {
            byte[] array = "736D616C6C636869286B6F696B652900".ToHexBytes();
            var reader = new JT808MessagePackReader(array);
            var str = reader.ReadVirtualStringEndChar0();
            Assert.Equal("smallchi(koike)", str);
            Assert.Equal(0, reader.ReaderCount);
        }
    }
}
