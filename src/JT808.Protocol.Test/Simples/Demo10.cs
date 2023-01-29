using JT808.Protocol.Enums;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System.Text.Json;
using JT808.Protocol.MessageBody.CarDVR;
using JT808.Protocol.Test.JT808LocationAttach;

namespace JT808.Protocol.Test.Simples
{
    /// <summary>
    /// 主要测试
    /// 1:1 正常自定义附加
    /// 2:1 非正常自定义附加
    /// 2:2 非正常自定义附加
    /// 1:4 粤标自定义附加
    /// 感谢大兄弟提供的真实设备数据
    /// </summary>
    public class Demo10
    {
        JT808Serializer JT808Serializer;

        public class Demo10ConfigBase : GlobalConfigBase
        {
            public override string ConfigId { get; protected set; } = "demo10Config";
        }

        public Demo10()
        {
            IJT808Config jT808Config = new Demo10ConfigBase();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap<JT808LocationAttachImpl0xDF>();
            jT808Config.FormatterFactory.SetMap<JT808LocationAttachImpl0xDF>();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap2<JT808LocationAttachImpl0xDE>();
            jT808Config.FormatterFactory.SetMap<JT808LocationAttachImpl0xDE>();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap3<JT808LocationAttachImpl0xDD>();
            jT808Config.FormatterFactory.SetMap<JT808LocationAttachImpl0xDD>();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap4<JT808LocationAttachImpl0x62>();
            jT808Config.FormatterFactory.SetMap<JT808LocationAttachImpl0x62>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1_1_1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId._0x0200.ToUInt16Value(),
                ManualMsgNum = 1,
                TerminalPhoneNo = "1122334455",
            };
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2021-08-30 18:17:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>(),
                CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>()
            };
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.CustomLocationAttachData.Add(0xDF, new JT808LocationAttachImpl0xDF
            {
                TestValue = new byte[66]
            });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0200006A0011223344550001000000010000000200BA7F0E07E4F11C0028003C000021083018171001040000006402020037DF42000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000677E", hex);
        }

        [Fact]
        public void Test2_1_1()
        {
            byte[] bytes = "7E0200006A0011223344550001000000010000000200BA7F0E07E4F11C0028003C000021083018171001040000006402020037DF42000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000677E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0200.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(1u, jT808Package.Header.MsgNum);
            Assert.Equal("1122334455", jT808Package.Header.TerminalPhoneNo);
            JT808_0x0200 jT808UploadLocationRequest = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(40u, jT808UploadLocationRequest.Altitude);
            Assert.Equal(DateTime.Parse("2021-08-30 18:17:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2u, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
            var jT808LocationAttachImpl0XDF = (JT808LocationAttachImpl0xDF)jT808UploadLocationRequest.CustomLocationAttachData[0xDF];
            Assert.Equal(0xDF, jT808LocationAttachImpl0XDF.AttachInfoId);
            Assert.Equal(new byte[66], jT808LocationAttachImpl0XDF.TestValue);
            Assert.Equal(66u, jT808LocationAttachImpl0XDF.AttachInfoLength);
        }

        [Fact]
        public void Test1_2_2()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId._0x0200.ToUInt16Value(),
                ManualMsgNum = 1,
                TerminalPhoneNo = "1122334455",
            };
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2021-08-30 18:17:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>(),
                CustomLocationAttachData3 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase3>()
            };
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.CustomLocationAttachData3.Add(0xDD, new JT808LocationAttachImpl0xDD
            {
                 TestValue=new byte[66]
            });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0200006C0011223344550001000000010000000200BA7F0E07E4F11C0028003C00002108301817100104000000640202003700DD0042000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000637E", hex);
        }

        [Fact]
        public void Test2_2_2()
        {
            byte[] bytes = "7E0200006C0011223344550001000000010000000200BA7F0E07E4F11C0028003C00002108301817100104000000640202003700DD0042000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000637E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0200.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(1u, jT808Package.Header.MsgNum);
            Assert.Equal("1122334455", jT808Package.Header.TerminalPhoneNo);
            JT808_0x0200 jT808UploadLocationRequest = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(40u, jT808UploadLocationRequest.Altitude);
            Assert.Equal(DateTime.Parse("2021-08-30 18:17:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2u, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
            var jT808LocationAttachImpl0XDD = (JT808LocationAttachImpl0xDD)jT808UploadLocationRequest.CustomLocationAttachData3[0xDD];
            Assert.Equal(0xDD, jT808LocationAttachImpl0XDD.AttachInfoId);
            Assert.Equal(new byte[66], jT808LocationAttachImpl0XDD.TestValue);
            Assert.Equal(66u, jT808LocationAttachImpl0XDD.AttachInfoLength);
        }

        [Fact]
        public void Test1_2_1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId._0x0200.ToUInt16Value(),
                ManualMsgNum = 1,
                TerminalPhoneNo = "1122334455",
            };
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2021-08-30 18:17:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>(),
                CustomLocationAttachData2 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase2>()
            };
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.CustomLocationAttachData2.Add(0xDE, new JT808LocationAttachImpl0xDE
            {
                TestValue = new byte[66]
            });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0200006B0011223344550001000000010000000200BA7F0E07E4F11C0028003C00002108301817100104000000640202003700DE42000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000677E", hex);
        }

        [Fact]
        public void Test2_2_1()
        {
            byte[] bytes = "7E0200006B0011223344550001000000010000000200BA7F0E07E4F11C0028003C00002108301817100104000000640202003700DE42000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000677E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0200.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(1u, jT808Package.Header.MsgNum);
            Assert.Equal("1122334455", jT808Package.Header.TerminalPhoneNo);
            JT808_0x0200 jT808UploadLocationRequest = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(40u, jT808UploadLocationRequest.Altitude);
            Assert.Equal(DateTime.Parse("2021-08-30 18:17:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2u, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
            var jT808LocationAttachImpl0XDE = (JT808LocationAttachImpl0xDE)jT808UploadLocationRequest.CustomLocationAttachData2[0xDE];
            Assert.Equal(0xDE, jT808LocationAttachImpl0XDE.AttachInfoId);
            Assert.Equal(new byte[66], jT808LocationAttachImpl0XDE.TestValue);
            Assert.Equal(66u, jT808LocationAttachImpl0XDE.AttachInfoLength);
        }

        [Fact]
        public void Test1_1_4()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId._0x0200.ToUInt16Value(),
                ManualMsgNum = 1,
                TerminalPhoneNo = "1122334455",
            };
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2021-05-31 18:17:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>(),
                CustomLocationAttachData4 = new Dictionary<byte, JT808_0x0200_CustomBodyBase4>()
            };
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.CustomLocationAttachData4.Add(0x62, new JT808LocationAttachImpl0x62
            {
                Data = new byte[256]
            });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0200012B0011223344550001000000010000000200BA7F0E07E4F11C0028003C000021053118171001040000006402020037620000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000D57E", hex);
        }

        [Fact]
        public void Test2_1_4()
        {
            byte[] bytes = "7E0200012B0011223344550001000000010000000200BA7F0E07E4F11C0028003C000021053118171001040000006402020037620000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000D57E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0200.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(1u, jT808Package.Header.MsgNum);
            Assert.Equal("1122334455", jT808Package.Header.TerminalPhoneNo);
            JT808_0x0200 jT808UploadLocationRequest = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(40u, jT808UploadLocationRequest.Altitude);
            Assert.Equal(DateTime.Parse("2021-05-31 18:17:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2u, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
            var jT808LocationAttachImpl0X62 = (JT808LocationAttachImpl0x62)jT808UploadLocationRequest.CustomLocationAttachData4[0x62];
            Assert.Equal(0x62, jT808LocationAttachImpl0X62.AttachInfoId);
            Assert.Equal(new byte[256], jT808LocationAttachImpl0X62.Data);
            Assert.Equal(256u, jT808LocationAttachImpl0X62.AttachInfoLength);
        }

        [Fact]
        public void Test_Mix_1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId._0x0200.ToUInt16Value(),
                ManualMsgNum = 1,
                TerminalPhoneNo = "1122334455",
            };
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2021-08-30 18:17:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>(),
                CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>(),
                CustomLocationAttachData2 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase2>(),
                CustomLocationAttachData3 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase3>(),
                CustomLocationAttachData4 = new Dictionary<byte, JT808_0x0200_CustomBodyBase4>(),
                UnknownLocationAttachData = new Dictionary<ushort, byte[]>(),
                ExceptionLocationAttachOriginalData = new List<byte[]>()
            };
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.CustomLocationAttachData.Add(0xDF, new JT808LocationAttachImpl0xDF
            {
                TestValue = new byte[66]
            });
            jT808UploadLocationRequest.CustomLocationAttachData3.Add(0xDD, new JT808LocationAttachImpl0xDD
            {
                TestValue = new byte[66]
            });
            jT808UploadLocationRequest.CustomLocationAttachData2.Add(0xDE, new JT808LocationAttachImpl0xDE
            {
                TestValue = new byte[66]
            });
            jT808UploadLocationRequest.CustomLocationAttachData4.Add(0x62, new JT808LocationAttachImpl0x62
            {
                Data = new byte[256]
            });
            jT808UploadLocationRequest.UnknownLocationAttachData.Add(0xCA, new byte[3] { 0xCA, 0x1, 0xFF });
            jT808UploadLocationRequest.UnknownLocationAttachData.Add(0xCB, new byte[4] { 0xCB, 0x2, 0xFF, 0xFE });
            //理论上组包的时候不会添加异常的原始数据下去
            jT808UploadLocationRequest.ExceptionLocationAttachOriginalData.Add(new byte[4] { 0xCD, 0x2, 0xFF, 0xFE });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E020002050011223344550001000000010000000200BA7F0E07E4F11C0028003C000021083018171001040000006402020037DF4200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000DE4200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000DD0042000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000620000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000CA01FFCB02FFFECD02FFFE587E", hex);
        }

        [Fact]
        public void Test_Mix_2()
        {
            byte[] bytes = "7E020002050011223344550001000000010000000200BA7F0E07E4F11C0028003C000021083018171001040000006402020037DF4200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000DE4200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000DD0042000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000620000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000CA01FFCB02FFFECD02FFFE587E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0200.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(1u, jT808Package.Header.MsgNum);
            Assert.Equal("1122334455", jT808Package.Header.TerminalPhoneNo);
            JT808_0x0200 jT808UploadLocationRequest = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(40u, jT808UploadLocationRequest.Altitude);
            Assert.Equal(DateTime.Parse("2021-08-30 18:17:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2u, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);

            var jT808LocationAttachImpl0X62 = (JT808LocationAttachImpl0x62)jT808UploadLocationRequest.CustomLocationAttachData4[0x62];
            Assert.Equal(0x62, jT808LocationAttachImpl0X62.AttachInfoId);
            Assert.Equal(new byte[256], jT808LocationAttachImpl0X62.Data);
            Assert.Equal(256u, jT808LocationAttachImpl0X62.AttachInfoLength);

            var jT808LocationAttachImpl0XDE = (JT808LocationAttachImpl0xDE)jT808UploadLocationRequest.CustomLocationAttachData2[0xDE];
            Assert.Equal(0xDE, jT808LocationAttachImpl0XDE.AttachInfoId);
            Assert.Equal(new byte[66], jT808LocationAttachImpl0XDE.TestValue);
            Assert.Equal(66u, jT808LocationAttachImpl0XDE.AttachInfoLength);

            var jT808LocationAttachImpl0XDD = (JT808LocationAttachImpl0xDD)jT808UploadLocationRequest.CustomLocationAttachData3[0xDD];
            Assert.Equal(0xDD, jT808LocationAttachImpl0XDD.AttachInfoId);
            Assert.Equal(new byte[66], jT808LocationAttachImpl0XDD.TestValue);
            Assert.Equal(66u, jT808LocationAttachImpl0XDD.AttachInfoLength);

            var jT808LocationAttachImpl0XDF = (JT808LocationAttachImpl0xDF)jT808UploadLocationRequest.CustomLocationAttachData[0xDF];
            Assert.Equal(0xDF, jT808LocationAttachImpl0XDF.AttachInfoId);
            Assert.Equal(new byte[66], jT808LocationAttachImpl0XDF.TestValue);
            Assert.Equal(66u, jT808LocationAttachImpl0XDF.AttachInfoLength);

            Assert.True(jT808UploadLocationRequest.UnknownLocationAttachData.ContainsKey(0xCA));
            Assert.True(jT808UploadLocationRequest.UnknownLocationAttachData.ContainsKey(0xCB));
            Assert.Equal(new byte[3] { 0xCA, 0x1, 0xFF },jT808UploadLocationRequest.UnknownLocationAttachData[0xCA]);
            Assert.Equal(new byte[4] { 0xCB, 0x2, 0xFF, 0xFE },jT808UploadLocationRequest.UnknownLocationAttachData[0xCB]);
            //理论上组包的时候不会添加异常的原始数据下去
            Assert.Equal(new byte[4] { 0xCD, 0x2, 0xFF, 0xFE },jT808UploadLocationRequest.UnknownLocationAttachData[0xCD]);
            Assert.Empty(jT808UploadLocationRequest.ExceptionLocationAttachOriginalData);
        }

        [Fact]
        public void Test_Repeat_1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId._0x0200.ToUInt16Value(),
                ManualMsgNum = 1,
                TerminalPhoneNo = "1122334455",
            };
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2021-08-30 18:17:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>(),
                CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>(),
                CustomLocationAttachData2 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase2>(),
                CustomLocationAttachData3 = new Dictionary<ushort, JT808_0x0200_CustomBodyBase3>(),
                CustomLocationAttachData4 = new Dictionary<byte, JT808_0x0200_CustomBodyBase4>(),
                UnknownLocationAttachData = new Dictionary<ushort, byte[]>(),
                ExceptionLocationAttachOriginalData = new List<byte[]>()
            };
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.CustomLocationAttachData.Add(0xDF, new JT808LocationAttachImpl0xDF
            {
                TestValue = new byte[66]
            });
            jT808UploadLocationRequest.UnknownLocationAttachData.Add(0xDF, new byte[3] { 0xDF, 0x1, 0xFF });
            jT808UploadLocationRequest.UnknownLocationAttachData.Add(0xCB, new byte[4] { 0xCB, 0x2, 0xFF, 0xFE });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E020000710011223344550001000000010000000200BA7F0E07E4F11C0028003C000021083018171001040000006402020037DF42000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000DF01FFCB02FFFE957E", hex);
        }

        [Fact]
        public void Test_Repeat_2()
        {
            byte[] bytes = "7E020000710011223344550001000000010000000200BA7F0E07E4F11C0028003C000021083018171001040000006402020037DF42000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000DF01FFCB02FFFE957E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0200.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(1u, jT808Package.Header.MsgNum);
            Assert.Equal("1122334455", jT808Package.Header.TerminalPhoneNo);
            JT808_0x0200 jT808UploadLocationRequest = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(40u, jT808UploadLocationRequest.Altitude);
            Assert.Equal(DateTime.Parse("2021-08-30 18:17:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2u, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);

            var jT808LocationAttachImpl0XDF = (JT808LocationAttachImpl0xDF)jT808UploadLocationRequest.CustomLocationAttachData[0xDF];
            Assert.Equal(0xDF, jT808LocationAttachImpl0XDF.AttachInfoId);
            Assert.Equal(new byte[66], jT808LocationAttachImpl0XDF.TestValue);
            Assert.Equal(66u, jT808LocationAttachImpl0XDF.AttachInfoLength);

            Assert.Single(jT808UploadLocationRequest.ExceptionLocationAttachOriginalData);
            Assert.Equal(new byte[3] { 0xDF, 0x1, 0xFF }, jT808UploadLocationRequest.ExceptionLocationAttachOriginalData[0]);
        }
    }

    /// <summary>
    /// 1:1
    /// </summary>
    public class JT808LocationAttachImpl0xDF : JT808MessagePackFormatter<JT808LocationAttachImpl0xDF>,JT808_0x0200_CustomBodyBase
    {
        /// <summary>
        /// 附加Id 0xDF
        /// </summary>
        public byte AttachInfoId { get; set; } = 0xDF;
        /// <summary>
        /// 
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte[] TestValue { get; set; }
        public override JT808LocationAttachImpl0xDF Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808LocationAttachImpl0xDF jT808LocationAttachImpl0XDF = new JT808LocationAttachImpl0xDF();
            jT808LocationAttachImpl0XDF.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0XDF.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0XDF.TestValue = reader.ReadArray(jT808LocationAttachImpl0XDF.AttachInfoLength).ToArray();
            return jT808LocationAttachImpl0XDF;
        }
        public override void Serialize(ref JT808MessagePackWriter writer, JT808LocationAttachImpl0xDF value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            if (value.TestValue != null)
            {
                writer.WriteByte((byte)value.TestValue.Length);
                writer.WriteArray(value.TestValue);
            }
            else
            {
                writer.WriteByte(0);
            }
        }
    }
    /// <summary>
    /// 2:1
    /// </summary>
    public class JT808LocationAttachImpl0xDE : JT808MessagePackFormatter<JT808LocationAttachImpl0xDE>, JT808_0x0200_CustomBodyBase2
    {
        /// <summary>
        /// 附加Id 0xDE
        /// </summary>
        public ushort AttachInfoId { get; set; } = 0xDE;
        /// <summary>
        /// 不固定
        /// </summary>
        public byte AttachInfoLength { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte[] TestValue { get; set; }

        public override JT808LocationAttachImpl0xDE Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808LocationAttachImpl0xDE jT808LocationAttachImpl0XDE = new JT808LocationAttachImpl0xDE();
            jT808LocationAttachImpl0XDE.AttachInfoId = reader.ReadUInt16();
            jT808LocationAttachImpl0XDE.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0XDE.TestValue = reader.ReadArray(jT808LocationAttachImpl0XDE.AttachInfoLength).ToArray();
            return jT808LocationAttachImpl0XDE;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808LocationAttachImpl0xDE value, IJT808Config config)
        {
            writer.WriteUInt16(value.AttachInfoId);
            if (value.TestValue != null)
            {
                writer.WriteByte((byte)value.TestValue.Length);
                writer.WriteArray(value.TestValue);
            }
            else
            {
                writer.WriteByte(0);
            }
        }
    }
    /// <summary>
    /// 2:2
    /// </summary>
    public class JT808LocationAttachImpl0xDD : JT808MessagePackFormatter<JT808LocationAttachImpl0xDD>,JT808_0x0200_CustomBodyBase3
    {
        /// <summary>
        /// 附加Id 0xDD
        /// </summary>
        public ushort AttachInfoId { get; set; } = 0xDD;
        /// <summary>
        /// 
        /// </summary>
        public ushort AttachInfoLength { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte[] TestValue { get; set; }

        public override JT808LocationAttachImpl0xDD Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808LocationAttachImpl0xDD jT808LocationAttachImpl0XDD = new JT808LocationAttachImpl0xDD();
            jT808LocationAttachImpl0XDD.AttachInfoId = reader.ReadUInt16();
            jT808LocationAttachImpl0XDD.AttachInfoLength = reader.ReadUInt16();
            jT808LocationAttachImpl0XDD.TestValue = reader.ReadArray(jT808LocationAttachImpl0XDD.AttachInfoLength).ToArray();
            return jT808LocationAttachImpl0XDD;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808LocationAttachImpl0xDD value, IJT808Config config)
        {
            writer.WriteUInt16(value.AttachInfoId);
            if (value.TestValue != null)
            {
                writer.WriteUInt16((ushort)value.TestValue.Length);
                writer.WriteArray(value.TestValue);
            }
            else
            {
                writer.WriteUInt16(0);
            }
        }
    }
    /// <summary>
    /// 1:4
    /// </summary>
    public class JT808LocationAttachImpl0x62 : JT808MessagePackFormatter<JT808LocationAttachImpl0x62>, JT808_0x0200_CustomBodyBase4
    {
        public byte AttachInfoId { get; set; } = 0x62;
        public uint AttachInfoLength { get; set; } = 256;
        public byte[] Data { get; set; }

        public override JT808LocationAttachImpl0x62 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808LocationAttachImpl0x62 jT808LocationAttachImpl0x62 = new JT808LocationAttachImpl0x62();
            jT808LocationAttachImpl0x62.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x62.AttachInfoLength = reader.ReadUInt32();
            jT808LocationAttachImpl0x62.Data = reader.ReadArray((int)jT808LocationAttachImpl0x62.AttachInfoLength).ToArray();
            return jT808LocationAttachImpl0x62;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808LocationAttachImpl0x62 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteUInt32((uint)value.Data.Length);
            writer.WriteArray(value.Data);
        }
    }
}
