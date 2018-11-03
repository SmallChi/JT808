using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.Test.JT808LocationAttach;
using System.IO;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0200Test : JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200();
            jT808UploadLocationRequest.AlarmFlag = 1;
            jT808UploadLocationRequest.Altitude = 40;
            jT808UploadLocationRequest.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
            jT808UploadLocationRequest.Lat = 12222222;
            jT808UploadLocationRequest.Lng = 132444444;
            jT808UploadLocationRequest.Speed = 60;
            jT808UploadLocationRequest.Direction = 0;
            jT808UploadLocationRequest.StatusFlag = 2;
            jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 55
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037", hex);
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
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
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
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200();
            jT808UploadLocationRequest.AlarmFlag = 1;
            jT808UploadLocationRequest.Altitude = 40;
            jT808UploadLocationRequest.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
            jT808UploadLocationRequest.Lat = 12222222;
            jT808UploadLocationRequest.Lng = 132444444;
            jT808UploadLocationRequest.Speed = 60;
            jT808UploadLocationRequest.Direction = 0;
            jT808UploadLocationRequest.StatusFlag = 2;
            jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(0x06, new JT808LocationAttachImpl0x06
            {
                Age = 18,
                Gender = 1,
                UserName = "smallchi"
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037060D0000001201736D616C6C636869", hex);
        }

        static JT808_0x0200Test()
        {
            JT808GlobalConfig.Instance.Register_0x0200_Attach<JT808LocationAttachImpl0x06>(0x06);
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
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
            Assert.Equal(18, ((JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808LocationAttachData[0x06]).Age);
            Assert.Equal(1, ((JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808LocationAttachData[0x06]).Gender);
            Assert.Equal("smallchi", ((JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808LocationAttachData[0x06]).UserName);
        }

        [Fact]
        public void Test5_1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                 MsgId= Enums.JT808MsgId.位置信息汇报.ToUInt16Value(),
                 MsgNum=8888,
                 TerminalPhoneNo="112233445566",
                 //MessageBodyProperty=new JT808MessageBodyProperty(38),
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
            jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 55
            });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
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
            //Assert.Equal(1, jT808Package.Header.MessageBodyProperty.DataLength);
            Assert.False(jT808Package.Header.MessageBodyProperty.IsPackge);
            Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackageIndex);
            Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackgeCount);
            Assert.Equal(JT808EncryptMethod.None, jT808Package.Header.MessageBodyProperty.Encrypt);

            JT808_0x0200 jT808UploadLocationRequest = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal((uint)1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal((uint)2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
        }


        [Fact]
        public void Test6()
        {
            //7E 02 00 00 3C 04 00 21 68 20 87 1D 0C 00 00 00 00 00 0C 00 C2 01 5B 6C 6E 06 C8 56 98 00 1E 00 00 00 E0 18 09 16 10 35 37 01 04 00 05 4C 7D 01 02 02 00 00 03 02 00 00 25 04 00 00 00 00 2B 04 00 00 00 00 30 01 1D 31 01 0D 97 7E

            byte[] bytes = "7E 02 00 00 3C 04 00 21 68 20 87 1D 0C 00 00 00 00 00 0C 00 C2 01 5B 6C 6E 06 C8 56 98 00 1E 00 00 00 E0 18 09 16 10 35 37 01 04 00 05 4C 7D 01 02 02 00 00 03 02 00 00 25 04 00 00 00 00 2B 04 00 00 00 00 30 01 1D 31 01 0D 97 7E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            
        }

        [Fact]
        public void Demo1()
        {
            JT808Package jT808Package = new JT808Package();

            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.位置信息汇报.ToUInt16Value(),
                MsgNum = 126,
                TerminalPhoneNo = "123456789012"
            };

            JT808_0x0200 jT808_0x0200 = new JT808_0x0200();
            jT808_0x0200.AlarmFlag = 1;
            jT808_0x0200.Altitude = 40;
            jT808_0x0200.GPSTime = DateTime.Parse("2018-10-15 10:10:10");
            jT808_0x0200.Lat = 12222222;
            jT808_0x0200.Lng = 132444444;
            jT808_0x0200.Speed = 60;
            jT808_0x0200.Direction = 0;
            jT808_0x0200.StatusFlag = 2;
            jT808_0x0200.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();

            jT808_0x0200.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 100
            });

            jT808_0x0200.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 125
            });

            jT808Package.Bodies = jT808_0x0200;

            byte[] data = JT808Serializer.Serialize(jT808Package);
            
            var hex = data.ToHexString();
            Assert.Equal("7E02000026123456789012007D02000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D01137E", hex);
            // 输出结果Hex：
            // 7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E
        }

        [Fact]
        public void Demo2()
        {
            //1.转成byte数组
            byte[] bytes = "7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E".ToHexBytes();

            //2.将数组反序列化
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);

            //3.数据包头
            Assert.Equal(Enums.JT808MsgId.位置信息汇报.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(38, jT808Package.Header.MessageBodyProperty.DataLength);
            Assert.Equal(126, jT808Package.Header.MsgNum);
            Assert.Equal("123456789012", jT808Package.Header.TerminalPhoneNo);
            Assert.False(jT808Package.Header.MessageBodyProperty.IsPackge);
            Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackageIndex);
            Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackgeCount);
            Assert.Equal(JT808EncryptMethod.None, jT808Package.Header.MessageBodyProperty.Encrypt);

            //4.数据包体
            JT808_0x0200 jT808_0x0200 = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal((uint)1, jT808_0x0200.AlarmFlag);
            Assert.Equal((uint)40, jT808_0x0200.Altitude);
            Assert.Equal(DateTime.Parse("2018-10-15 10:10:10"), jT808_0x0200.GPSTime);
            Assert.Equal(12222222, jT808_0x0200.Lat);
            Assert.Equal(132444444, jT808_0x0200.Lng);
            Assert.Equal(60, jT808_0x0200.Speed);
            Assert.Equal(0, jT808_0x0200.Direction);
            Assert.Equal((uint)2, jT808_0x0200.StatusFlag);
            //4.1.附加信息1
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808_0x0200.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            //4.2.附加信息2
            Assert.Equal(125, ((JT808LocationAttachImpl0x02)jT808_0x0200.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
        }

        [Fact]
        public void Demo3()
        {
            JT808Package jT808Package = Enums.JT808MsgId.位置信息汇报.Create("123456789012", 
                new JT808_0x0200 { 
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-10-15 10:10:10"),
                    Lat = 12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    StatusFlag = 2,
                    JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>
                    {
                        { JT808LocationAttachBase.AttachId0x01,new JT808LocationAttachImpl0x01{Mileage = 100}},
                        { JT808LocationAttachBase.AttachId0x02,new JT808LocationAttachImpl0x02{Oil = 125}}
                    }
            });

            byte[] data = JT808Serializer.Serialize(jT808Package);

            var hex = data.ToHexString();
            //输出结果Hex：
            //7E 02 00 00 26 12 34 56 78 90 12 00 01 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 6C 7E
            //7E 02 00 00 26 12 34 56 78 90 12 00 01 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 6C 7E
            Assert.Equal("7E020000261234567890120001000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D016C7E", hex);
        }

        [Fact]
        public void Demo4()
        {
            JT808GlobalConfig.Instance
                // 注册自定义位置附加信息
                .Register_0x0200_Attach<JT808LocationAttachImpl0x06>(0x06)
                //.SetMsgSNDistributed(//todo 实现IMsgSNDistributed消息流水号)
                // 注册自定义数据上行透传信息
                //.Register_0x0900_Ext<>(//todo 继承自JT808_0x0900_BodyBase类)
                // 注册自定义数据下行透传信息
                //.Register_0x8900_Ext<>(//todo 继承自JT808_0x8900_BodyBase类)
                // 跳过校验码验证
                .SetSkipCRCCode(true);
        }
    }
}
