using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8300Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            //"7E 83 00 00 0D 01 23 45 67 89 00 00 01 05 73 6D 61 6C 6C 63 68 69 20 35 31 38 02 7E"
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x8300.ToUInt16Value(),
                    ManualMsgNum = 1,
                    TerminalPhoneNo = "012345678900",
                }
            };
            JT808_0x8300 jT808TextSend = new JT808_0x8300
            {
                TextInfo = "smallchi 518",
                TextFlag = 5
            };
            jT808Package.Bodies = jT808TextSend;
            string hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E8300000D012345678900000105736D616C6C63686920353138027E", hex);
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

        [Fact]
        public void Test_2019_1()
        {
            //"7E 83 00 00 0D 01 23 45 67 89 00 00 01 05 73 6D 61 6C 6C 63 68 69 20 35 31 38 02 7E"
            JT808HeaderMessageBodyProperty jT808HeaderMessageBodyProperty = new JT808HeaderMessageBodyProperty(true);
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MessageBodyProperty = jT808HeaderMessageBodyProperty,
                    MsgId = Enums.JT808MsgId._0x8300.ToUInt16Value(),
                    ManualMsgNum = 1,
                    TerminalPhoneNo = "012345678900",
                }
            };
            JT808_0x8300 jT808TextSend = new JT808_0x8300
            {
                TextType=1,
                TextInfo = "smallchi 518",
                TextFlag = 5
            };
            jT808Package.Bodies = jT808TextSend;
            string hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E8300400E010000000001234567890000010501736D616C6C63686920353138417E", hex);
        }

        [Fact]
        public void Test_2019_2()
        {
            byte[] bytes = "7E8300400E010000000001234567890000010501736D616C6C63686920353138417E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize(bytes);
            JT808_0x8300 jT808TextSend = (JT808_0x8300)jT808Package.Bodies;
            Assert.Equal("smallchi 518", jT808TextSend.TextInfo);
            Assert.Equal(5, jT808TextSend.TextFlag);
            Assert.Equal(1, jT808TextSend.TextType);
        }

        [Fact]
        public void Test_2019_3()
        {
            byte[] bytes = "7E8300400E010000000001234567890000010501736D616C6C63686920353138417E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
        }
    }
}
