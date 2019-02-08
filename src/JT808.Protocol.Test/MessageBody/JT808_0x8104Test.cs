using JT808.Protocol.Extensions;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8104Test
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.查询终端参数.ToUInt16Value(),
                    MsgNum = 1,
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
            Assert.Equal(Enums.JT808MsgId.查询终端参数.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);
            Assert.Null(jT808Package.Bodies);
        }
    }
}
