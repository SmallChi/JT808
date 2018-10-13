using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Test.MessageBodySend
{
    public class JT808_0x8300Test
    {
        [Fact]
        public void Test1()
        {
            //"7E 83 00 00 0D 01 23 45 67 89 00 00 01 05 73 6D 61 6C 6C 63 68 69 20 35 31 38 02 7E"
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.文本信息下发,
                MsgNum = 1,
                TerminalPhoneNo = "012345678900",
            };
            JT808_0x8300 jT808TextSend = new JT808_0x8300();
            jT808TextSend.TextInfo = "smallchi 518";
            jT808TextSend.TextFlag = 5;
            jT808Package.Bodies = jT808TextSend;
            string hex = JT808Serializer.Serialize(jT808Package).ToHexString();
        }

        [Fact]
        public void Test1_2()
        {
            byte[] bytes = "7E 83 00 00 0D 01 23 45 67 89 00 00 01 05 73 6D 61 6C 6C 63 68 69 20 35 31 38 02 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            JT808_0x8300 jT808TextSend = (JT808_0x8300)jT808Package.Bodies;
            Assert.Equal("smallchi 518", jT808TextSend.TextInfo);
            Assert.Equal(5, jT808TextSend.TextFlag);
        }
    }
}
