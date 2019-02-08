using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0104Test
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.查询终端参数应答.ToUInt16Value(),
                    MsgNum = 10,
                    TerminalPhoneNo = "123456789",
                },
                Bodies = new JT808_0x0104
                {
                    MsgNum = 20,
                    AnswerParamsCount = 1,
                    ParamList = new List<JT808_0x8103_BodyBase> {
                    new JT808_0x8103_0x0001() {
                         ParamId=0x0001,
                         ParamLength=4,
                         ParamValue=10
                    }
                }
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0104000C000123456789000A00140100000001040000000A907E", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "7E0104000C000123456789000A00140100000001040000000A907E".ToHexBytes();
            JT808Package jT808_0X8104 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.查询终端参数应答.ToUInt16Value(), jT808_0X8104.Header.MsgId);
            Assert.Equal(10, jT808_0X8104.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X8104.Header.TerminalPhoneNo);

            JT808_0x0104 JT808Bodies = (JT808_0x0104)jT808_0X8104.Bodies;
            Assert.Equal(20, JT808Bodies.MsgNum);
            Assert.Equal(1, JT808Bodies.AnswerParamsCount);
            foreach (var item in JT808Bodies.ParamList)
            {
                Assert.Equal(0x0001u, ((JT808_0x8103_0x0001)item).ParamId);
                Assert.Equal(4, ((JT808_0x8103_0x0001)item).ParamLength);
                Assert.Equal(10u, ((JT808_0x8103_0x0001)item).ParamValue);
            }
        }


        [Fact]
        public void Test2()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.查询终端参数应答.ToUInt16Value(),
                    MsgNum = 10,
                    TerminalPhoneNo = "123456789",
                },
                Bodies = new JT808_0x0104
                {
                    MsgNum = 20,
                    AnswerParamsCount = 2,
                    ParamList = new List<JT808_0x8103_BodyBase> {
                    new JT808_0x8103_0x0001() {
                         ParamId=0x0001,
                         ParamLength=4,
                         ParamValue=10
                    },
                    new JT808_0x8103_0x0013(){
                         ParamId=0x0013,
                         ParamValue="www.baidu.com"
                    }
                }
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0104001E000123456789000A00140200000001040000000A000000130D7777772E62616964752E636F6DF27E", hex);
        }

        [Fact]
        public void Test2_1()
        {
            byte[] bytes = "7E0104001E000123456789000A00140200000001040000000A000000130D7777772E62616964752E636F6DF27E".ToHexBytes();
            JT808Package jT808_0X8104 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.查询终端参数应答.ToUInt16Value(), jT808_0X8104.Header.MsgId);
            Assert.Equal(10, jT808_0X8104.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X8104.Header.TerminalPhoneNo);

            JT808_0x0104 JT808Bodies = (JT808_0x0104)jT808_0X8104.Bodies;
            Assert.Equal(20, JT808Bodies.MsgNum);
            Assert.Equal(2, JT808Bodies.AnswerParamsCount);
            foreach (var item in JT808Bodies.ParamList)
            {
                switch (item.ParamId)
                {
                    case 0x0001:
                        Assert.Equal(0x0001u, ((JT808_0x8103_0x0001)item).ParamId);
                        Assert.Equal(4, ((JT808_0x8103_0x0001)item).ParamLength);
                        Assert.Equal(10u, ((JT808_0x8103_0x0001)item).ParamValue);
                        break;
                    case 0x0013:
                        Assert.Equal(0x0013u, ((JT808_0x8103_0x0013)item).ParamId);
                        Assert.Equal("www.baidu.com", ((JT808_0x8103_0x0013)item).ParamValue);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}