using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0003Test
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                 MsgId= Enums.JT808MsgId.终端注销,
                 MsgNum=1,
                 TerminalPhoneNo="12345678900",
            };
            //"7E 00 03 00 00 01 23 45 67 89 00 00 01 8B 7E"
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E 00 03 00 00 01 23 45 67 89 00 00 01 8B 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.终端注销, jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);
            Assert.Null(jT808Package.Bodies);
        }
    }
}
