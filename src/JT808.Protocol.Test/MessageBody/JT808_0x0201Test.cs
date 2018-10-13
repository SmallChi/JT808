using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808LocationAttach;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.MessageBodyReply
{
    public class JT808_0x0201Test
    {
        [Fact]
        public void Test1()
        {
            //"7E 02 01 00 2A 11 22 33 44 55 66 22 B8 30 39 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 00 00 53 7E"

            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.位置信息查询应答,
                MsgNum = 8888,
                TerminalPhoneNo = "112233445566",
            };
            JT808_0x0201 jT808_0X0201 = new JT808_0x0201();
            jT808_0X0201.MsgNum = 12345;
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
            jT808_0X0201.Position = jT808UploadLocationRequest;
            jT808Package.Bodies = jT808_0X0201;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E 02 01 00 2A 11 22 33 44 55 66 22 B8 30 39 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 00 00 53 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize(bytes);
            JT808_0x0201 jT808_0X0201 = (JT808_0x0201) jT808Package.Bodies;
            Assert.Equal(12345, jT808_0X0201.MsgNum);
            Assert.Equal((uint)1, jT808_0X0201.Position.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808_0X0201.Position.GPSTime);
            Assert.Equal(12222222, jT808_0X0201.Position.Lat);
            Assert.Equal(132444444, jT808_0X0201.Position.Lng);
            Assert.Equal(60, jT808_0X0201.Position.Speed);
            Assert.Equal((uint)2, jT808_0X0201.Position.StatusFlag);
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808_0X0201.Position.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808_0X0201.Position.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
        }
    }
}
