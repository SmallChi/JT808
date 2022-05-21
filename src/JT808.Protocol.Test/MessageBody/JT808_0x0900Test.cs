using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Test.JT808_0x0900_BodiesImpl;
using System.Reflection;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0900Test
    {
        JT808Serializer JT808Serializer;

        public JT808_0x0900Test()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.FormatterFactory.SetMap<JT808_0x0900_0x83>();
            jT808Config.JT808_0x0900_Custom_Factory.SetMap<JT808_0x0900_0x83>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Test1()
        {
            JT808Package jT808_0X0900 = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0900.ToUInt16Value(),
                    ManualMsgNum = 10,
                    TerminalPhoneNo = "123456789",
                },
                Bodies = new JT808_0x0900
                {
                    JT808_0x0900_BodyBase = new JT808_0x0900_0x83() { PassthroughContent = "smallchi" },
                    PassthroughType = 0x83
                }
            };
            var hex = JT808Serializer.Serialize(jT808_0X0900).ToHexString();
            Assert.Equal("7E09000009000123456789000A83736D616C6C6368691D7E", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "7E 09 00 00 09 00 01 23 45 67 89 00 0A 83 73 6D 61 6C 6C 63 68 69 1D 7E".ToHexBytes();
            JT808Package jT808_0X0900 = JT808Serializer.Deserialize(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0900.ToUInt16Value(), jT808_0X0900.Header.MsgId);
            Assert.Equal(10, jT808_0X0900.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X0900.Header.TerminalPhoneNo);
            JT808_0x0900 JT808Bodies = (JT808_0x0900)jT808_0X0900.Bodies;
            JT808_0x0900_0x83 jT808_0x0900_0x83 = (JT808_0x0900_0x83)JT808Bodies.JT808_0x0900_BodyBase;
            Assert.Equal("smallchi", jT808_0x0900_0x83.PassthroughContent);
            Assert.Equal(0x83, JT808Bodies.PassthroughType);
        }

        [Fact]
        public void Test1_3()
        {
            byte[] bytes = "7E 09 00 00 09 00 01 23 45 67 89 00 0A 83 73 6D 61 6C 6C 63 68 69 1D 7E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
        }
    }
}
