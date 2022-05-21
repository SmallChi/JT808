using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0201Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0201.ToUInt16Value(),
                    ManualMsgNum = 8888,
                    TerminalPhoneNo = "112233445566",
                }
            };
            JT808_0x0201 jT808_0X0201 = new JT808_0x0201
            {
                ReplyMsgNum = 12345
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
                BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808_0X0201.Position = jT808UploadLocationRequest;
            jT808Package.Bodies = jT808_0X0201;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0201002811223344556622B83039000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037517E".Length, hex.Length);
            Assert.Equal("7E0201002811223344556622B83039000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037517E", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E0201002811223344556622B83039000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037517E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize(bytes);
            JT808_0x0201 jT808_0X0201 = (JT808_0x0201) jT808Package.Bodies;
            Assert.Equal(12345, jT808_0X0201.ReplyMsgNum);
            Assert.Equal((uint)1, jT808_0X0201.Position.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808_0X0201.Position.GPSTime);
            Assert.Equal(12222222, jT808_0X0201.Position.Lat);
            Assert.Equal(132444444, jT808_0X0201.Position.Lng);
            Assert.Equal(60, jT808_0X0201.Position.Speed);
            Assert.Equal((uint)2, jT808_0X0201.Position.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)jT808_0X0201.Position.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)jT808_0X0201.Position.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "7E0201002811223344556622B83039000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037517E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
        }
    }
}
