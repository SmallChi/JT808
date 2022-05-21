using JT808.Protocol.Interfaces;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Internal;

namespace JT808.Protocol.Test.Simples
{
    public class Demo1
    {
        public JT808Serializer JT808Serializer;
        public Demo1()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package();

            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId._0x0200.ToUInt16Value(),
                ManualMsgNum = 126,
                TerminalPhoneNo = "123456789012"
            };

            JT808_0x0200 jT808_0x0200 = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-10-15 10:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>()
            };

            jT808_0x0200.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
            {
                Mileage = 100
            });

            jT808_0x0200.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
            {
                Oil = 125
            });

            jT808Package.Bodies = jT808_0x0200;

            byte[] data = JT808Serializer.Serialize(jT808Package);

            var hex = data.ToHexString();
            //"7E020000261234567890120001000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D016C7E"
            Assert.Equal("7E02000026123456789012007D02000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D01137E", hex);
            // 输出结果Hex：
            // 7E 02 00 00 26 12 34 56 78 90 12 00 7D 02 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 10 15 10 10 10 01 04 00 00 00 64 02 02 00 7D 01 13 7E
        }
    }
}
