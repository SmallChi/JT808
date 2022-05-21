using JT808.Protocol.Extensions;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0003Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0003.ToUInt16Value(),
                    ManualMsgNum = 1,
                    TerminalPhoneNo = "12345678900",
                }
            };
            //"7E 00 03 00 00 01 23 45 67 89 00 00 01 8B 7E"
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0003000001234567890000018B7E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E 00 03 00 00 01 23 45 67 89 00 00 01 8B 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId._0x0003.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(1, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);
            Assert.Null(jT808Package.Bodies);
        }
    }
}
