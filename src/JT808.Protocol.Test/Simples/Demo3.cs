using JT808.Protocol.Enums;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using JT808.Protocol.Internal;

namespace JT808.Protocol.Test.Simples
{
    public  class Demo3
    {
        public JT808Serializer JT808Serializer;
        public Demo3()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = JT808MsgId._0x0200.Create("123456789012",
                new JT808_0x0200
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-10-15 10:10:10"),
                    Lat = 12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    StatusFlag = 2,
                    BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>
                    {
                        { JT808Constants.JT808_0x0200_0x01,new JT808_0x0200_0x01{Mileage = 100}},
                        { JT808Constants.JT808_0x0200_0x02,new JT808_0x0200_0x02{Oil = 125}}
                    }
                });
            jT808Package.Header.ManualMsgNum = 1;
            byte[] data = JT808Serializer.Serialize(jT808Package);
            var hex = data.ToHexString();
            //输出结果Hex：
            //7E 02 00 00 26 12 34 56 78 90 12 00 01 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 6C 7E
            //"7E020000261234567890120001000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D016C7E"
            Assert.Equal("7E020000261234567890120001000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D016C7E", hex);
        }
    }
}
