using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0900Test
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808_0X0900 = new JT808Package();
            jT808_0X0900.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.数据上行透传,
                MsgNum = 10,
                TerminalPhoneNo = "123456789",
            };
            jT808_0X0900.Bodies = new JT808_0x0900
            {
                JT808_0x0900_BodyBase = new JT808_0x0900_0x83() { PassthroughContent = "smallchi" },
                PassthroughType = 0x83
            };
            var hex = JT808Serializer.Serialize(jT808_0X0900).ToHexString();
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "7E 09 00 00 09 00 01 23 45 67 89 00 0A 83 73 6D 61 6C 6C 63 68 69 1D 7E".ToHexBytes();
            JT808Package jT808_0X0900 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.数据上行透传, jT808_0X0900.Header.MsgId);
            Assert.Equal(10, jT808_0X0900.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X0900.Header.TerminalPhoneNo);

            JT808_0x0900 JT808Bodies = (JT808_0x0900)jT808_0X0900.Bodies;
            Assert.Equal("smallchi", ((JT808_0x0900_0x83)JT808Bodies.JT808_0x0900_BodyBase).PassthroughContent);
            Assert.Equal(0x83, JT808Bodies.PassthroughType);
        }
    }
}
