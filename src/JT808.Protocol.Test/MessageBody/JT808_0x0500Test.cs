using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0500Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0500.ToUInt16Value(),
                    ManualMsgNum = 8888,
                    TerminalPhoneNo = "112233445566",
                }
            };
            JT808_0x0500 jT808_0X0500 = new JT808_0x0500();
            JT808_0x0200 JT808_0x0200_1 = new JT808_0x0200
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
            JT808_0x0200_1.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });
            JT808_0x0200_1.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 55
            });
            jT808_0X0500.JT808_0x0200 = JT808_0x0200_1;
            jT808_0X0500.MsgNum = 1000;
            jT808Package.Bodies = jT808_0X0500;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0500002811223344556622B803E8000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037B57E".Length, hex.Length);
            Assert.Equal("7E0500002811223344556622B803E8000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037B57E", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E0500002811223344556622B803E8000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037B57E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0500.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(8888, jT808Package.Header.MsgNum);
            Assert.Equal("112233445566", jT808Package.Header.TerminalPhoneNo);
            JT808_0x0500 JT808Bodies = (JT808_0x0500)jT808Package.Bodies;
            Assert.Equal(1000, JT808Bodies.MsgNum);
            Assert.Equal((uint)1, JT808Bodies.JT808_0x0200.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), JT808Bodies.JT808_0x0200.GPSTime);
            Assert.Equal(12222222, JT808Bodies.JT808_0x0200.Lat);
            Assert.Equal(132444444, JT808Bodies.JT808_0x0200.Lng);
            Assert.Equal(0, JT808Bodies.JT808_0x0200.Direction);
            Assert.Equal(60, JT808Bodies.JT808_0x0200.Speed);
            Assert.Equal((uint)2, JT808Bodies.JT808_0x0200.StatusFlag);
            Assert.Equal(100, ((JT808_0x0200_0x01)JT808Bodies.JT808_0x0200.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x01]).Mileage);
            Assert.Equal(55, ((JT808_0x0200_0x02)JT808Bodies.JT808_0x0200.BasicLocationAttachData[JT808Constants.JT808_0x0200_0x02]).Oil);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "7E0500002811223344556622B803E8000000010000000200BA7F0E07E4F11C0028003C000018071510101001040000006402020037B57E".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808Package>(bytes);
        }
    }
}
