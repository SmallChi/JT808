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
using System.Linq;
using JT808.Protocol.Test.JT808LocationAttach;

namespace JT808.Protocol.Test.Simples
{
    public class Demo12
    {
        JT808Serializer JT808Serializer;

        public Demo12()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.JT808_0X0200_Custom_Factory.SetMap<JT808LocationAttachImpl0x62>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.位置信息汇报.ToUInt16Value(),
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
            jT808UploadLocationRequest.JT808CustomLocationAttachData.Add(0x62, new JT808LocationAttachImpl0x62
            {
                Data=new byte[256]
            });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0200012B0011223344550001000000010000000200BA7F0E07E4F11C0028003C000021053118171001040000006402020037620000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000D57E", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E0200012B0011223344550001000000010000000200BA7F0E07E4F11C0028003C000021053118171001040000006402020037620000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000D57E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.位置信息汇报.ToValue(), jT808Package.Header.MsgId);
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
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
            var jT808LocationAttachImpl0X62 = (JT808LocationAttachImpl0x62)jT808UploadLocationRequest.JT808CustomLocationAttachData[0x62];
            Assert.Equal(0x62, jT808LocationAttachImpl0X62.AttachInfoId);
            Assert.Equal(new byte[256], jT808LocationAttachImpl0X62.Data);
            Assert.Equal(256u, jT808LocationAttachImpl0X62.AttachInfoLengthExtend);
            Assert.Equal(0, jT808LocationAttachImpl0X62.AttachInfoLength);
        }
    }
}
