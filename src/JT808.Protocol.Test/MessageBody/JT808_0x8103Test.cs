using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808_0x8103_Body;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.MessageBody
{
   public  class JT808_0x8103Test
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.设置终端参数.ToUInt16Value(),
                MsgNum = 10,
                TerminalPhoneNo = "123456789",
            };
            jT808Package.Bodies = new JT808_0x8103
            {
                ParamList =new List<JT808_0x8103_BodyBase> {
                    new JT808_0x8103_0x0001() {
                         ParamId=0x0001,
                        ParamLength=4,
                            ParamValue=10
                    }
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E8103000A000123456789000A0100000001040000000A057E", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "7E 81 03 00 0A 00 01 23 45 67 89 00 0A 01 00 00 00 01 04 00 00 00 0A 05 7E".ToHexBytes();
            JT808Package jT808_0X8103 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.设置终端参数.ToUInt16Value(), jT808_0X8103.Header.MsgId);
            Assert.Equal(10, jT808_0X8103.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X8103.Header.TerminalPhoneNo);

            JT808_0x8103 JT808Bodies = (JT808_0x8103)jT808_0X8103.Bodies;
            foreach (var item in JT808Bodies.ParamList)
            {
                Assert.Equal("10", ((JT808_0x8103_0x0001)item).ParamValue.ToString());
            }
        }
    }
}
