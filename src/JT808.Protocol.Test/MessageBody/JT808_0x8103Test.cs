using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Test.MessageBody.JT808_0x8103CustomIdExtensions;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8103Test
    {
        JT808Serializer JT808Serializer;

        public JT808_0x8103Test()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.JT808_0X8103_Custom_Factory.SetMap<JT808_0x8103_0x0075>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.设置终端参数.ToUInt16Value(),
                    MsgNum = 10,
                    TerminalPhoneNo = "123456789",
                },
                Bodies = new JT808_0x8103
                {
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
            Assert.Equal("7E8103000A000123456789000A0100000001040000000A057E", hex);
        }

        [Fact]
        public void Test1_1()
        {
            //7E
            //81 03
            //00 0A
            //00 
            //01 23 45 67 89 00 
            //0A 01 
            //00 00 00 01 
            //04
            // 00 00 00 0A 
            //05
            //7E

            byte[] bytes = "7E8103000A000123456789000A0100000001040000000A057E".ToHexBytes();
            JT808Package jT808_0X8103 = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.设置终端参数.ToUInt16Value(), jT808_0X8103.Header.MsgId);
            Assert.Equal(10, jT808_0X8103.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X8103.Header.TerminalPhoneNo);

            JT808_0x8103 JT808Bodies = (JT808_0x8103)jT808_0X8103.Bodies;
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
            var JT808_0x8103 = new JT808_0x8103
            {
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
            };
            var hex = JT808Serializer.Serialize(JT808_0x8103).ToHexString();
            //"0200000001040000000A000000130D7777772E62616964752E636F6D"
            Assert.Equal("0200000001040000000A000000130D7777772E62616964752E636F6D", hex);
        }

        [Fact]
        public void Test2_1()
        {
            byte[] bytes = "0200000001040000000A000000130D7777772E62616964752E636F6D".ToHexBytes();
            JT808_0x8103 jT808_0X8103 = JT808Serializer.Deserialize<JT808_0x8103>(bytes);

            foreach (var item in jT808_0X8103.ParamList)
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
        public void Test3()
        {
            var JT808_0x8103 = new JT808_0x8103
            {
                 ParamList=new List<JT808_0x8103_BodyBase>(),
                 CustomParamList = new List<JT808_0x8103_CustomBodyBase> {
                    new JT808_0x8103_0x0075() {
                            AudioOutputEnabled=1,
                            OSD=2,
                            RTS_EncodeMode=3,
                            RTS_KF_Interval=4,
                            RTS_Resolution=5,
                            RTS_Target_CodeRate=6,
                            RTS_Target_FPS=7,
                            StreamStore_EncodeMode=8,
                            StreamStore_KF_Interval=9,
                            StreamStore_Resolution=10,
                            StreamStore_Target_CodeRate=11,
                            StreamStore_Target_FPS=12
                    }
                }
            };
            var customParams = Newtonsoft.Json.JsonConvert.SerializeObject(JT808_0x8103.CustomParamList);
            //"[{\"ParamId\":117,\"ParamLength\":21,\"RTS_EncodeMode\":3,\"RTS_Resolution\":5,\"RTS_KF_Interval\":4,\"RTS_Target_FPS\":7,\"RTS_Target_CodeRate\":6,\"StreamStore_EncodeMode\":8,\"StreamStore_Resolution\":10,\"StreamStore_KF_Interval\":9,\"StreamStore_Target_FPS\":12,\"StreamStore_Target_CodeRate\":11,\"OSD\":2,\"AudioOutputEnabled\":1}]"
            var hex = JT808Serializer.Serialize(JT808_0x8103).ToHexString();
            //"010000007515030500040700000006080A00090C0000000B000201"
            Assert.Equal("010000007515030500040700000006080A00090C0000000B000201", hex);
        }

        [Fact]
        public void Test3_1()
        {
            var customParams = "[{\"ParamId\":117,\"ParamLength\":21,\"RTS_EncodeMode\":3,\"RTS_Resolution\":5,\"RTS_KF_Interval\":4,\"RTS_Target_FPS\":7,\"RTS_Target_CodeRate\":6,\"StreamStore_EncodeMode\":8,\"StreamStore_Resolution\":10,\"StreamStore_KF_Interval\":9,\"StreamStore_Target_FPS\":12,\"StreamStore_Target_CodeRate\":11,\"OSD\":2,\"AudioOutputEnabled\":1}]";
            byte[] bytes = "010000007515030500040700000006080A00090C0000000B000201".ToHexBytes();
            JT808_0x8103 jT808_0X8103 = JT808Serializer.Deserialize<JT808_0x8103>(bytes);

            Assert.Equal(customParams, Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0X8103.CustomParamList));
        }


    }
}
