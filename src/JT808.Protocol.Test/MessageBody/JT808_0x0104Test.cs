using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0104Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0104.ToUInt16Value(),
                    ManualMsgNum = 10,
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
            Assert.Equal(Enums.JT808MsgId._0x0104.ToUInt16Value(), jT808_0X8104.Header.MsgId);
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
                    MsgId = Enums.JT808MsgId._0x0104.ToUInt16Value(),
                    ManualMsgNum = 10,
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
            var hex0x0104 = JT808Serializer.Serialize(new JT808_0x0104
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
            }).ToHexString();
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            //7E0104001E000123456789000A00140200000001040000000A000000130F7777772E62616964752E636F6DF07E
            //7E0104001E000123456789000A00140200000001040000000A000000130D7777772E62616964752E636F6DF27E
            Assert.Equal("7E0104001E000123456789000A00140200000001040000000A000000130D7777772E62616964752E636F6DF27E", hex);
        }
        [Fact]
        public void Test3()
        {
            byte[] bodys = "00140200000001040000000A000000130D7777772E62616964752E636F6D".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0104>(bodys);
        }

        [Fact]
        public void Test2_1()
        {
            byte[] bytes = "7E0104001E000123456789000A00140200000001040000000A000000130D7777772E62616964752E636F6DF27E".ToHexBytes();
            JT808Package jT808_0X8104 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0104.ToUInt16Value(), jT808_0X8104.Header.MsgId);
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
        [Fact]
        public void Test2_2()
        {
            byte[] bytes = "7E010400C604052458039503D800020100000077BE09010101002D1400000064000500141400000320001F020101002D0F00000064000500191900000840001F030101002D0F00000064000500191900000840001F040101002D0F00000064000500191900000840001F050101002D0F00000064010500191900000840001F060101002D0F00000064000500191900000840001F070101002D0F000000640103001919000004CE001F080101002D0F00000064000500191900000840001F090101000F0F000000C8010500191900000400001F127E".ToHexBytes();
            JT808Package jT808_0X8104 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0104.ToUInt16Value(), jT808_0X8104.Header.MsgId);
            Assert.Equal(0x03d8, jT808_0X8104.Header.MsgNum);
            Assert.Equal("40524580395", jT808_0X8104.Header.TerminalPhoneNo);

            JT808_0x0104 JT808Bodies = (JT808_0x0104)jT808_0X8104.Bodies;
            Assert.Equal(0x0002, JT808Bodies.MsgNum);
            Assert.Equal(1, JT808Bodies.AnswerParamsCount);
            Assert.Null(JT808Bodies.ParamList);


        }
    }
}