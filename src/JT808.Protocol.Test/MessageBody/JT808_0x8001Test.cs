using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x8001Test : JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.平台通用应答.ToUInt16Value(),
                    MsgNum = 10,
                    TerminalPhoneNo = "12345678900",
                },
                Bodies = new JT808_0x8001
                {
                    MsgId = Enums.JT808MsgId.位置信息汇报.ToUInt16Value(),
                    JT808PlatformResult = Enums.JT808PlatformResult.成功,
                    MsgNum = 100
                }
            };
            //"7E 
            //80 01 
            //00 05 
            //01 23 45 67 89 00 
            //00 0A 
            //00 64
            //02 00 
            //00 
            //61 
            //7E"
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E80010005012345678900000A0064020000617E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E 80 01 00 05 01 23 45 67 89 00 00 0A 00 64 02 00 00 61 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.平台通用应答.ToUInt16Value(), jT808Package.Header.MsgId);
            Assert.Equal(10, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);

            JT808_0x8001 JT808Bodies = (JT808_0x8001)jT808Package.Bodies;
            Assert.Equal(Enums.JT808MsgId.位置信息汇报.ToUInt16Value(), JT808Bodies.MsgId);
            Assert.Equal(100, JT808Bodies.MsgNum);
            Assert.Equal(Enums.JT808PlatformResult.成功, JT808Bodies.JT808PlatformResult);
        }
    }
}
