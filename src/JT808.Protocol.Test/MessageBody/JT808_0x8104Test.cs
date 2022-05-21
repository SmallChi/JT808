using JT808.Protocol.Extensions;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8104Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x8104.ToUInt16Value(),
                    ManualMsgNum = 1,
                    TerminalPhoneNo = "12345678900",
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E8104000001234567890000010D7E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E8104000001234567890000010D7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x8104.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);
            Assert.Null(jT808Package.Bodies);
        }

        [Fact]
        public void Test3()
        {
            //用户示例
            var bytes = "7E81040000010000000001341990550700016B7E".ToHexBytes();
            var jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes, Enums.JT808Version.JTT2019);
            var jT808HeaderPackage = JT808Serializer.HeaderDeserialize(bytes, Enums.JT808Version.JTT2019);


            //demo示例
            var data2013 = JT808.Protocol.Enums.JT808MsgId._0x8104.Create("12345678900");
            var hex = JT808Serializer.Serialize(data2013, Enums.JT808Version.JTT2019);

            var jT808HeaderPackage1= JT808Serializer.HeaderDeserialize(hex, Enums.JT808Version.JTT2019);
            Assert.Equal("12345678900", jT808HeaderPackage1.Header.TerminalPhoneNo);
            var jT808Package1 = JT808Serializer.Deserialize(hex, Enums.JT808Version.JTT2019);
            Assert.Equal("12345678900", jT808Package1.Header.TerminalPhoneNo);
        }
    }
}
