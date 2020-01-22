using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Test.JT808LocationAttach;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0200Test
    {
        JT808Serializer JT808Serializer;

        public JT808_0x0200Test()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap<JT808LocationAttachImpl0x06>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Test1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037", hex);
        }

        [Fact]
        public void Parallel_Test1_1()
        {
            ConcurrentBag<string> hexs = new ConcurrentBag<string>();
            Parallel.For(1, 20, new ParallelOptions
            {
                MaxDegreeOfParallelism=5
            }, (i) => 
            {
                JT808Package jT808Package = new JT808Package();
                jT808Package.Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.位置信息汇报.ToUInt16Value(),
                    ManualMsgNum = (ushort)i,
                    TerminalPhoneNo = "1122334455"+i.ToString(),
                };
                JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-07-15 10:10:10").AddSeconds(i),
                    Lat = 12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    StatusFlag = 2,
                    JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
                };
                jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
                {
                    Mileage = 100
                });
                jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
                {
                    Oil = 55
                });
                jT808Package.Bodies = jT808UploadLocationRequest;
                var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
                hexs.Add(hex);
            });
            //7E02000026 112233445519 0013 000000010000000200BA7F0E07E4F11C0028003C0000 180715101029 01040000006402020037987E,
            //7E02000026 112233445518 0012 000000010000000200BA7F0E07E4F11C0028003C0000 180715101028 01040000006402020037997E,
            //7E02000026 112233445517 0011 000000010000000200BA7F0E07E4F11C0028003C0000 180715101027 010400000064020200379A7E,
            //7E02000026 112233445516 0010 000000010000000200BA7F0E07E4F11C0028003C0000 180715101026 010400000064020200379B7E,
            //7E02000026 112233445515 000F 000000010000000200BA7F0E07E4F11C0028003C0000 180715101025 01040000006402020037847E,
            //7E02000026 112233445514 000E 000000010000000200BA7F0E07E4F11C0028003C0000 180715101024 01040000006402020037857E,
            //7E02000026 112233445513 000D 000000010000000200BA7F0E07E4F11C0028003C0000 180715101023 01040000006402020037867E,
            //7E02000026 112233445512 000C 000000010000000200BA7F0E07E4F11C0028003C0000 180715101022 01040000006402020037877E,
            //7E02000026 112233445511 000B 000000010000000200BA7F0E07E4F11C0028003C0000 180715101021 01040000006402020037807E,
            //7E02000026 112233445510 000A 000000010000000200BA7F0E07E4F11C0028003C0000 180715101020 01040000006402020037817E,
            //7E02000026 011223344559 0009 000000010000000200BA7F0E07E4F11C0028003C0000 180715101019 01040000006402020037A27E,
            //7E02000026 011223344558 0008 000000010000000200BA7F0E07E4F11C0028003C0000 180715101018 01040000006402020037A37E,
            //7E02000026 011223344557 0007 000000010000000200BA7F0E07E4F11C0028003C0000 180715101017 01040000006402020037AC7E,
            //7E02000026 011223344556 0006 000000010000000200BA7F0E07E4F11C0028003C0000 180715101016 01040000006402020037AD7E,
            //7E02000026 011223344555 0005 000000010000000200BA7F0E07E4F11C0028003C0000 180715101015 01040000006402020037AE7E,
            //7E02000026 011223344554 0004 000000010000000200BA7F0E07E4F11C0028003C0000 180715101014 01040000006402020037AF7E,
            //7E02000026 011223344553 0003 000000010000000200BA7F0E07E4F11C0028003C0000 180715101013 01040000006402020037A87E,
            //7E02000026 011223344552 0002 000000010000000200BA7F0E07E4F11C0028003C0000 180715101012 01040000006402020037A97E,
            //7E02000026 011223344551 0001 000000010000000200BA7F0E07E4F11C0028003C0000 180715101011 01040000006402020037AA7E,
            //7E02000026 011223344550 0001 000000010000000200BA7F0E07E4F11C0028003C0000 180715101010 01040000006402020037AA7E
            //7E02000026 011223344550 0000 000000010000000200BA7F0E07E4F11C0028003C0000 180715101010 01040000006402020037AB7E,
            string result = "7E020000260112233445500000000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037AB7E,7E020000260112233445570007000000010000000200BA7F0E07E4F11C0028003C000018071510101701040000006402020037AC7E,7E020000260112233445560006000000010000000200BA7F0E07E4F11C0028003C000018071510101601040000006402020037AD7E,7E020000260112233445550005000000010000000200BA7F0E07E4F11C0028003C000018071510101501040000006402020037AE7E,7E020000260112233445540004000000010000000200BA7F0E07E4F11C0028003C000018071510101401040000006402020037AF7E,7E020000261122334455190013000000010000000200BA7F0E07E4F11C0028003C000018071510102901040000006402020037987E,7E020000261122334455180012000000010000000200BA7F0E07E4F11C0028003C000018071510102801040000006402020037997E,7E020000261122334455170011000000010000000200BA7F0E07E4F11C0028003C0000180715101027010400000064020200379A7E,7E020000261122334455160010000000010000000200BA7F0E07E4F11C0028003C0000180715101026010400000064020200379B7E,7E02000026112233445515000F000000010000000200BA7F0E07E4F11C0028003C000018071510102501040000006402020037847E,7E02000026112233445514000E000000010000000200BA7F0E07E4F11C0028003C000018071510102401040000006402020037857E,7E02000026112233445513000D000000010000000200BA7F0E07E4F11C0028003C000018071510102301040000006402020037867E,7E02000026112233445512000C000000010000000200BA7F0E07E4F11C0028003C000018071510102201040000006402020037877E,7E020000260112233445530003000000010000000200BA7F0E07E4F11C0028003C000018071510101301040000006402020037A87E,7E020000260112233445520002000000010000000200BA7F0E07E4F11C0028003C000018071510101201040000006402020037A97E,7E020000260112233445510001000000010000000200BA7F0E07E4F11C0028003C000018071510101101040000006402020037AA7E,7E02000026112233445511000B000000010000000200BA7F0E07E4F11C0028003C000018071510102101040000006402020037807E,7E02000026112233445510000A000000010000000200BA7F0E07E4F11C0028003C000018071510102001040000006402020037817E,7E020000260112233445590009000000010000000200BA7F0E07E4F11C0028003C000018071510101901040000006402020037A27E,7E020000260112233445580008000000010000000200BA7F0E07E4F11C0028003C000018071510101801040000006402020037A37E";
            //7E020000260112233445500001000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037AA7E
            List<string> resultHexs = result.Split(',').ToList();
            string hexStr = string.Join(',', hexs);
            foreach (var item in hexs)
            {
                Assert.Contains(item, resultHexs);
            }
        }

        [Fact]
        public void Test2()
        {
            byte[] bodys = "00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal((uint)1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal((uint)2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
        }

        [Fact]
        public void Test2_1()
        {
            byte[] bodys = "00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0200>(bodys);
        }

        [Fact]
        public void Test3()
        {

            //00 00 00 01 
            //00 00 00 02 
            //00 BA 7F 0E 
            //07 E4 F1 1C 
            //00 28 
            //00 3C 
            //00 00 
            //18 07 15 10 10 10 
            //01 
            //  04 
            //      00 00 00 64 
            //02 
            //  02 
            //      00 37 
            //06
            //  0D 
            //      00 00 00 12 01 73 6D 61 6C 6C 63 68 69"
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>(),
                JT808CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>()
            };
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.JT808CustomLocationAttachData.Add(0x06, new JT808LocationAttachImpl0x06
            {
                Age = 18,
                Gender = 1,
                UserName = "smallchi"
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037060D0000001201736D616C6C636869", hex);
        }

        [Fact]
        public void Test4()
        {
            // 1.添加自定义附加信息扩展 AddJT808LocationAttachMethod 
            byte[] bodys = "00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 06 0D 00 00 00 12 01 73 6D 61 6C 6C 63 68 69".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal((uint)1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal((uint)2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
            var jT808LocationAttachImpl0x06 = (JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808CustomLocationAttachData[0x06];
            Assert.Equal(18, jT808LocationAttachImpl0x06.Age);
            Assert.Equal(1, jT808LocationAttachImpl0x06.Gender);
            Assert.Equal("smallchi", jT808LocationAttachImpl0x06.UserName);
        }

        [Fact]
        public void Test5_1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.位置信息汇报.ToUInt16Value(),
                MsgNum = 8888,
                TerminalPhoneNo = "112233445566",
                //MessageBodyProperty=new JT808MessageBodyProperty(38),
            };
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
        }
        [Fact]
        public void Test5_2()
        {
            byte[] bytes = "7E 02 00 00 26 11 22 33 44 55 66 22 B8 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 57 7E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
        }
        [Fact]
        public void Test5()
        {
            //7E 
            //02 00 
            //00 33 
            //11 22 33 44 55 66 
            //22 B8 
            //00 00 00 01 
            //00 00 00 02 
            //00 BA 7F 0E 
            //07 E4 F1 1C 
            //00 28 
            //00 3C 
            //00 00 
            //18 07 15 10 10 10 
            //01 
            //  04 
            //     00 00 00 64 
            //02 
            //   02 
            //     00 37 
            //42 7E


            byte[] bytes = "7E 02 00 00 26 11 22 33 44 55 66 22 B8 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 57 7E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.位置信息汇报.ToValue(), jT808Package.Header.MsgId);

            Assert.Equal(38, jT808Package.Header.MessageBodyProperty.DataLength);
            Assert.Equal(8888, jT808Package.Header.MsgNum);
            Assert.Equal("112233445566", jT808Package.Header.TerminalPhoneNo);
            Assert.False(jT808Package.Header.MessageBodyProperty.IsPackage);
            Assert.Equal(0, jT808Package.Header.PackageIndex);
            Assert.Equal(0, jT808Package.Header.PackgeCount);
            Assert.Equal(JT808EncryptMethod.None, jT808Package.Header.MessageBodyProperty.Encrypt);

            JT808_0x0200 jT808UploadLocationRequest = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal((uint)1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal((uint)2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
        }
        [Fact]
        public void Test6()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 300000,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 1002222,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000493E0000F4AEE00BA7F0E07E4F11C0028003C000018071510101001040000006402020037", hex);
        }
        [Fact]
        public void Test6_1()
        {
            byte[] bodys = "000493E0000F4AEE00BA7F0E07E4F11C0028003C000018071510101001040000006402020037".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0200>(bodys);
        }

        [Fact]
        public void Test_all_attcahids()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.位置信息汇报.ToUInt16Value(),
                ManualMsgNum = 8888,
                TerminalPhoneNo = "112233445566",
            };
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200();
            jT808UploadLocationRequest.AlarmFlag = 1;
            jT808UploadLocationRequest.Altitude = 40;
            jT808UploadLocationRequest.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
            jT808UploadLocationRequest.Lat = 12222222;
            jT808UploadLocationRequest.Lng = 132444444;
            jT808UploadLocationRequest.Speed = 60;
            jT808UploadLocationRequest.Direction = 0;
            jT808UploadLocationRequest.StatusFlag = 2;
            jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>();
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x03, new JT808_0x0200_0x03
            {
                Speed = 56
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x04, new JT808_0x0200_0x04
            {
                EventId = 1
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x11, new JT808_0x0200_0x11
            {
                AreaId = 1,
                JT808PositionType = Enums.JT808PositionType.圆形区域
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x12, new JT808_0x0200_0x12
            {
                AreaId = 1,
                JT808PositionType = Enums.JT808PositionType.圆形区域,
                Direction = Enums.JT808DirectionType.出
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x13, new JT808_0x0200_0x13
            {
                DrivenRoute = Enums.JT808DrivenRouteType.过长,
                DrivenRouteId = 2,
                Time = 34
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x25, new JT808_0x0200_0x25
            {
                CarSignalStatus = 23
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x2A, new JT808_0x0200_0x2A
            {
                IOStatus = 244
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x2B, new JT808_0x0200_0x2B
            {
                Analog = 242
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x30, new JT808_0x0200_0x30
            {
                WiFiSignalStrength = 0x02
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808Constants.JT808_0x0200_0x31, new JT808_0x0200_0x31
            {
                GNSSCount = 0x05
            });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0200005C11223344556622B8000000010000000200BA7F0E07E4F11C0028003C00001807151010100104000000640202003703020038040200011105010000000112060100000001011307000000020022012504000000172A0200F42B04000000F2300102310105167E", hex);
            //7E0200005C11223344556622B8000000010000000200BA7F0E07E4F11C0028003C00001807151010100104000000640202003703020038040200011105010000000112060100000001011307000000020022012504000000172A0200F42B04000000F2300102310105167E

            //7E
            //02 00
            //00 5C
            //11 22 33 44 55 66
            //22 B8
            //00 00 00 01
            //00 00 00 02
            //00 BA 7F 0E
            //07 E4 F1 1C
            //00 28 
            //00 3C
            //00 00
            //18 07 15 10 10 10
            //01
            //  04
            //      00 00 00 64
            //02
            //  02
            //      00 37
            //03
            //  02
            //      00 38
            //04
            //  02
            //      00 01
            //11
            //  05
            //      01 00 00 00 01 
            //12
            //  06
            //      01 00 00 00 01 01 
            //13
            //  07
            //      00 00 00 02 00 22 01
            //25
            //  04
            //      00 00 00 17
            //2A
            //  02
            //      00 F4 
            //2B
            //  04
            //      00 00 00 F2 
            //30
            //  01
            //      02
            //31
            //  01
            //      05
            //16
            //7E
        }
        [Fact]
        public void LatLngTest1_1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = -12222222,
                Lng = -132444444,
                Speed = 60,
                Direction = 0,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.StatusFlag = 0x18000000;
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("0000000118000000FF4580F2F81B0EE40028003C0000180715101010", hex);
        }
        [Fact]
        public void LatLngTest1_2()
        {
            byte[] bodys = "0000000118000000FF4580F2F81B0EE40028003C0000180715101010".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(402653184u, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(-12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(-132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed); //‭402653184‬
        }
        [Fact]
        public void LatLngTest2_1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = -12222222,
                Lng = -132444444,
                Speed = 60,
                Direction = 0,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.StatusFlag = 0x18000000 | 0x302;
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("0000000118000302FF4580F2F81B0EE40028003C0000180715101010", hex);
        }
        [Fact]
        public void LatLngTest2_2()
        {
            byte[] bodys = "0000000118000302FF4580F2F81B0EE40028003C0000180715101010".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal((uint)(0x18000000 | 0x302), jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(-12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(-132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed); //‭402653184‬
        }
        [Fact]
        public void LatTest1_1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = -12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.StatusFlag = 0x10000000;
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("0000000110000000FF4580F207E4F11C0028003C0000180715101010", hex);
        }
        [Fact]
        public void LatTest1_2()
        {
            byte[] bodys = "0000000110000000FF4580F207E4F11C0028003C0000180715101010".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal((uint)0x10000000, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(-12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed); //‭402653184‬
        }
        [Fact]
        public void LatTest2()
        {
            JT808Exception exception= Assert.Throws<JT808Exception>(() => {
                JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                    Lat = -12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
                };
                jT808UploadLocationRequest.StatusFlag = 1111;
                var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            });
            Assert.Equal(JT808ErrorCode.LatOrLngError, exception.ErrorCode);
        }
        [Fact]
        public void LatTest3_1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = -12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.StatusFlag = 0x10000000 | 0x000300;
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("0000000110000300FF4580F207E4F11C0028003C0000180715101010", hex);
        }
        [Fact]
        public void LatTest3_2()
        {
            byte[] bodys = "0000000110000300FF4580F207E4F11C0028003C0000180715101010".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal((uint)(0x10000000 | 0x000300), jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(-12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed); //‭402653184‬
        }
        [Fact]
        public void LngTest1_1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = 12222222,
                Lng = -132444444,
                Speed = 60,
                Direction = 0,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.StatusFlag = 0x8000000;
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010800000000BA7F0EF81B0EE40028003C0000180715101010", hex);
        }
        [Fact]
        public void LngTest1_2()
        {
            byte[] bodys = "000000010800000000BA7F0EF81B0EE40028003C0000180715101010".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal((uint)0x8000000, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(-132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed); //‭402653184‬
        }
        [Fact]
        public void LngTest2()
        {
            JT808Exception exception = Assert.Throws<JT808Exception>(() => {
                JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                    Lat = 12222222,
                    Lng = -132444444,
                    Speed = 60,
                    Direction = 0,
                    JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
                };
                jT808UploadLocationRequest.StatusFlag = 1111;
                var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            });
            Assert.Equal(JT808ErrorCode.LatOrLngError, exception.ErrorCode);
        }
        [Fact]
        public void LngTest3_1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = 12222222,
                Lng = -132444444,
                Speed = 60,
                Direction = 0,
                JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.StatusFlag = 0x8000000|0x6601;
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010800660100BA7F0EF81B0EE40028003C0000180715101010", hex);
        }
        [Fact]
        public void LngTest3_2()
        {
            byte[] bodys = "000000010800660100BA7F0EF81B0EE40028003C0000180715101010".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal((uint)(0x8000000 | 0x6601), jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(-132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed); //‭402653184‬
        }
    }
}
