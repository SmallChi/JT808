using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using System;
using System.Reflection;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0801Test
    {
        JT808Serializer JT808Serializer;

        public JT808_0x0801Test()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.SkipCRCCode = true;
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            JT808_0x0801 jT808_0X0801 = new JT808_0x0801
            {
                ChannelId = 123,
                EventItemCoding = JT808EventItemCoding.regular_action.ToByteValue(),
                MultimediaCodingFormat = JT808MultimediaCodingFormat.JPEG.ToByteValue(),
                MultimediaId = 2567,
                MultimediaType = JT808MultimediaType.image.ToByteValue(),
                MultimediaDataPackage = new byte[] { 0x01, 0x02, 0x03, 0x04 },
                Position = new JT808_0x0200
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-11-15 23:26:10"),
                    Lat = 12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    StatusFlag = 2
                }
            };
            string hex = JT808Serializer.Serialize(jT808_0X0801).ToHexString();
            Assert.Equal("00000A070000017B000000010000000200BA7F0E07E4F11C0028003C000018111523261001020304", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "00000A070000017B000000010000000200BA7F0E07E4F11C0028003C000018111523261001020304".ToHexBytes();
            JT808_0x0801 jT808_0X0801 = JT808Serializer.Deserialize<JT808_0x0801>(bytes);
            Assert.Equal(123, jT808_0X0801.ChannelId);
            Assert.Equal(JT808EventItemCoding.regular_action.ToByteValue(), jT808_0X0801.EventItemCoding);
            Assert.Equal(JT808MultimediaCodingFormat.JPEG.ToByteValue(), jT808_0X0801.MultimediaCodingFormat);
            Assert.Equal((uint)2567, jT808_0X0801.MultimediaId);
            Assert.Equal(JT808MultimediaType.image.ToByteValue(), jT808_0X0801.MultimediaType);
            Assert.Equal(new byte[] { 0x01, 0x02, 0x03, 0x04 }, jT808_0X0801.MultimediaDataPackage);
            Assert.Equal((uint)1, jT808_0X0801.Position.AlarmFlag);
            Assert.Equal(40, jT808_0X0801.Position.Altitude);
            Assert.Equal(DateTime.Parse("2018-11-15 23:26:10"), jT808_0X0801.Position.GPSTime);
            Assert.Equal(40, jT808_0X0801.Position.Altitude);
            Assert.Equal(12222222, jT808_0X0801.Position.Lat);
            Assert.Equal(132444444, jT808_0X0801.Position.Lng);
            Assert.Equal(60, jT808_0X0801.Position.Speed);
            Assert.Equal(0, jT808_0X0801.Position.Direction);
            Assert.Equal((uint)2, jT808_0X0801.Position.StatusFlag);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "00000A070000017B000000010000000200BA7F0E07E4F11C0028003C000018111523261001020304".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0801>(bytes);
        }

        [Fact]
        public void Package1()
        {
            byte[] bytes = "7e080123240138123456782031000a00010000271d0000010100000000000c200301d2400a063296a501e100000000190104092952ffd8ffc4001f0000010501010101010100000000000000000102030405060708090a0bffc400b5100002010303020403050504040000017d0101020300041105122131410613516107227114328191a1082342b1c11552d1f02433627282090a161718191a25262728292a3435363738393a434445464748494a535455565758595a636465666768696a737475767778797a838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae1e2e3e4e5e6e7e8e9eaf1f2f3f4f5f6f7f8f9faffc4001f0100030101010101010101010000000000000102030405060708090a0bffc400b51100020102040403040705040400010277000102031104052131061241510761711322328108144291a1b1c109233352f0156272d10a162434e125f11718191a262728292a35363738393a434445464748494a535455565758595a636465666768696a737475767778797a82838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae2e3e4e5e6e7e8e9eaf2f3f4f5f6f7f8f9faffdb004300080606070605080707070909080a0c140d0c0b0b0c1912130f141d1a1f1e1d1a1c1c20242e2720222c231c1c2837292c30313434341f27393d38323c2e333432ffdb0043010909090c0b0c180d0d1832211c213232323232323232323232323232323232323232323232323232323232323232323232323232323232323232323232323232fffe000b4750456e636f646572ffdb0043000d090a0b0a080d0b0a0b0e0e0d0f13201513121213271c1e17202e2931302e292d2c333a4a3e333646372c2d405741464c4e525352323e5a615a50604a51524fffdb0043010e0e0e131113261515264f352d354f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4f4fffc000110800f0014003012100021101031101ffc4001f000001050101010101010000000000000000010203041c7e".ToHexBytes();
            JT808Package jT808_0X0801 = JT808Serializer.Deserialize<JT808Package>(bytes);
        }

        [Fact]
        public void Package2()
        {
            byte[] bytes = "7e080123000138123456782032000a000205060708090a0bffc400b5100002010303020403050504040000017d0101020300041105122131410613516107227114328191a1082342b1c11552d1f02433627282090a161718191a25262728292a3435363738393a434445464748494a535455565758595a636465666768696a737475767778797a838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae1e2e3e4e5e6e7e8e9eaf1f2f3f4f5f6f7f8f9faffc4001f0100030101010101010101010000000000000102030405060708090a0bffc400b51100020102040403040705040400010277000102031104052131061241510761711322328108144291a1b1c109233352f0156272d10a162434e125f11718191a262728292a35363738393a434445464748494a535455565758595a636465666768696a737475767778797a82838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae2e3e4e5e6e7e8e9eaf2f3f4f5f6f7f8f9faffdd00040000ffda000c03010002110311003f006c6a2a755ce299a5c942e0f35281c5004aa72314a54e38a07b8841ef4840a0673de21b4ff498ee402038dade991fe7f4acc110f4a0cd8ef2f1405cd01d45f2e9360a062edc5745616a6dad511861cfccff0053499512e056cf1460e3348a0ed4b8e338fc2819cb5edbfd9ee648b18556f97fdd3d3f4aafb4d332ea433a6573e9550d3131d18c9c558031c0a4083a503039a60c42c2984e4f4a06260d370690098ef4751400c615132d021868a621431a33480ef235e05595403eb54cbb0b8e7069dc0e3a9a41b12a024f4a9d40f4a18c5651e951c88179268194ee614b989a2719461ffea35cfdcda4b6b2ed71c1e55874345c96ba91819a704c50217613db349b39031c9e945c66a69ba794713cebf30fb8be9ee6b540c1e948a48760e3a526d2dc77a0a144471d297cb623a71484646bb685234b81d01d8e7d018f43f9ff003ac16386c552225b8300c2a84c8c8c4ed247b502616cc0517e".ToHexBytes();
            JT808Package jT808_0X0801 = JT808Serializer.Deserialize<JT808Package>(bytes);
        }

        [Fact]
        public void Package3()
        {
            byte[] bytes = "7e080123000138123456782032000a000205060708090a0bffc400b5100002010303020403050504040000017d0101020300041105122131410613516107227114328191a1082342b1c11552d1f02433627282090a161718191a25262728292a3435363738393a434445464748494a535455565758595a636465666768696a737475767778797a838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae1e2e3e4e5e6e7e8e9eaf1f2f3f4f5f6f7f8f9faffc4001f0100030101010101010101010000000000000102030405060708090a0bffc400b51100020102040403040705040400010277000102031104052131061241510761711322328108144291a1b1c109233352f0156272d10a162434e125f11718191a262728292a35363738393a434445464748494a535455565758595a636465666768696a737475767778797a82838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae2e3e4e5e6e7e8e9eaf2f3f4f5f6f7f8f9faffdd00040000ffda000c03010002110311003f006c6a2a755ce299a5c942e0f35281c5004aa72314a54e38a07b8841ef4840a0673de21b4ff498ee402038dade991fe7f4acc110f4a0cd8ef2f1405cd01d45f2e9360a062edc5745616a6dad511861cfccff0053499512e056cf1460e3348a0ed4b8e338fc2819cb5edbfd9ee648b18556f97fdd3d3f4aafb4d332ea433a6573e9550d3131d18c9c558031c0a4083a503039a60c42c2984e4f4a06260d370690098ef4751400c615132d021868a621431a33480ef235e05595403eb54cbb0b8e7069dc0e3a9a41b12a024f4a9d40f4a18c5651e951c88179268194ee614b989a2719461ffea35cfdcda4b6b2ed71c1e55874345c96ba91819a704c50217613db349b39031c9e945c66a69ba794713cebf30fb8be9ee6b540c1e948a48760e3a526d2dc77a0a144471d297cb623a71484646bb685234b81d01d8e7d018f43f9ff003ac16386c552225b8300c2a84c8c8c4ed247b502616cc0517e".ToHexBytes();
            JT808HeaderPackage jT808_0X0801 = JT808Serializer.HeaderDeserialize(bytes);
        }

        [Fact]
        public void Test2011_1()
        {
            JT808_0x0801 jT808_0X0801 = new JT808_0x0801
            {
                ChannelId = 123,
                EventItemCoding = JT808EventItemCoding.regular_action.ToByteValue(),
                MultimediaCodingFormat = JT808MultimediaCodingFormat.JPEG.ToByteValue(),
                MultimediaId = 2567,
                MultimediaType = JT808MultimediaType.image.ToByteValue(),
                MultimediaDataPackage = new byte[] { 0x01, 0x02, 0x03, 0x04 }
            };
            string hex = JT808Serializer.Serialize(jT808_0X0801,JT808Version.JTT2011).ToHexString();
            Assert.Equal("00000A070000017B01020304", hex);
        }

        [Fact]
        public void Test2011_2()
        {
            byte[] bytes = "00000A070000017B01020304".ToHexBytes();
            JT808_0x0801 jT808_0X0801 = JT808Serializer.Deserialize<JT808_0x0801>(bytes);
            Assert.Equal(123, jT808_0X0801.ChannelId);
            Assert.Equal(JT808EventItemCoding.regular_action.ToByteValue(), jT808_0X0801.EventItemCoding);
            Assert.Equal(JT808MultimediaCodingFormat.JPEG.ToByteValue(), jT808_0X0801.MultimediaCodingFormat);
            Assert.Equal((uint)2567, jT808_0X0801.MultimediaId);
            Assert.Equal(JT808MultimediaType.image.ToByteValue(), jT808_0X0801.MultimediaType);
            Assert.Equal(new byte[] { 0x01, 0x02, 0x03, 0x04 }, jT808_0X0801.MultimediaDataPackage);
        }

        [Fact]
        public void Test2011_3()
        {
            byte[] bytes = "00000A070000017B01020304".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0801>(bytes);
        }
    }
}
