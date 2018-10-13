using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public  class JT808_0x8001Test : JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.平台通用应答,
                MsgNum = 10,
                TerminalPhoneNo = "12345678900",
            };
            jT808Package.Bodies = new JT808_0x8001
            {
                 MsgId= Enums.JT808MsgId.位置信息汇报,
                 JT808PlatformResult= Enums.JT808PlatformResult.Success,
                 MsgNum=100
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
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E 80 01 00 05 01 23 45 67 89 00 00 0A 00 64 02 00 00 61 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.平台通用应答, jT808Package.Header.MsgId);
            Assert.Equal(10, jT808Package.Header.MsgNum);
            Assert.Equal("12345678900", jT808Package.Header.TerminalPhoneNo);

            JT808_0x8001 JT808Bodies = (JT808_0x8001)jT808Package.Bodies;
            Assert.Equal(Enums.JT808MsgId.位置信息汇报, JT808Bodies.MsgId);
            Assert.Equal(100, JT808Bodies.MsgNum);
            Assert.Equal(Enums.JT808PlatformResult.Success, JT808Bodies.JT808PlatformResult);
        }
    }
}
