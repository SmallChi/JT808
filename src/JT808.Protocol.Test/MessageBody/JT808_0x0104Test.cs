using System.Collections.Generic;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
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

        [Theory]
        [InlineData("7E0104004B01801550511313AE00000900000001040000000F00000002040000000A00000013103232332E3130382E3133332E31363300000000170100000000180400000328000000550400000078000000560400000000027E")]
        public void Test4(string hex)
        {
            var bytes = hex.ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize(bytes);
            Assert.IsType<JT808_0x0104>(jT808Package.Bodies);
        }

        [Fact]
        public void Test5()
        {
            byte[] bodys = "7E0104020D019019000002002381042300000001040000007800000013106465766963652E67707335312E636F6D00000018040000032800000029040000001E00000030040000000F000000800400000ED90000009004000000020000F00010313735373732393638372C32383830300000F001020A780000F0025941504E3A61706E3A2C636F70733A302C322C223436303030222C372C61706E5F6375723A312C22222C22222C22222C312C61706E5F696E666F3A636D696F74786178642E6A732E4D4E433030382E4D43433436302E475052530000F0030943656E746F724E6F3A0000F0040200640000F00501000000F00601000000F00701000000F0080F3836383130383037343239323936340000F0091438393836303444333130323244303535373638330000F00A0A45533130315F303830340000F00C0A45533130315F563330310000F00D0F3436303038353332313630373638330000F00E01000000F00F011E0000F01001F60000F01101000000F01201030000F01304000186A00000F0142D303039352D303032342D2D303030392D303432372D303334332D303331332D303431302D303032322D303030380000F01501460000F01601000000F01701010000F01801000000F0190E312C312C31302C312C332C302C330000F0201C363030322C326337632C5175656374656C2C4547383030414B2D434E0000F0210F3131363635313438333838383433320000F0220100747E".ToHexBytes();
            string json = JT808Serializer.Analyze(bodys);
        }
    }
}