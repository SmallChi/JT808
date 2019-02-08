using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0001Test : JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.终端通用应答.ToUInt16Value(),
                    MsgNum = 1203,
                    TerminalPhoneNo = "012345678900"
                },
                Bodies = new JT808_0x0001
                {
                    MsgId = Enums.JT808MsgId.终端心跳.ToUInt16Value(),
                    MsgNum = 1000,
                    JT808TerminalResult = Enums.JT808TerminalResult.Success
                }
            };
            //"7E 00 01 00 05 01 23 45 67 89 00 04 B3 03 E8 00 02 00 D3 7E"
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0001000501234567890004B303E8000200D37E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E 00 01 00 05 01 23 45 67 89 00 04 B3 03 E8 00 02 00 D3 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.终端通用应答.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(1203, jT808Package.Header.MsgNum);

            JT808_0x0001 JT808Bodies = (JT808_0x0001)jT808Package.Bodies;
            Assert.Equal(Enums.JT808MsgId.终端心跳.ToUInt16Value(), JT808Bodies.MsgId);
            Assert.Equal(1000, JT808Bodies.MsgNum);
            Assert.Equal(Enums.JT808TerminalResult.Success, JT808Bodies.JT808TerminalResult);
        }
    }
}
