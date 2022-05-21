using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0001Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();

        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId._0x0001.ToUInt16Value(),
                    ManualMsgNum = 1203,
                    TerminalPhoneNo = "012345678900",
                },
                Bodies = new JT808_0x0001
                {
                    ReplyMsgId = Enums.JT808MsgId._0x0002.ToUInt16Value(),
                    ReplyMsgNum = 1000,
                    TerminalResult = Enums.JT808TerminalResult.Success
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
            Assert.Equal(Enums.JT808MsgId._0x0001.ToValue(), jT808Package.Header.MsgId);
            Assert.Equal(1203, jT808Package.Header.MsgNum);

            JT808_0x0001 JT808Bodies = (JT808_0x0001)jT808Package.Bodies;
            Assert.Equal(Enums.JT808MsgId._0x0002.ToUInt16Value(), JT808Bodies.ReplyMsgId);
            Assert.Equal(1000, JT808Bodies.ReplyMsgNum);
            Assert.Equal(Enums.JT808TerminalResult.Success, JT808Bodies.TerminalResult);
        }

        [Fact]
        public void Test3()
        {
            var bytes = "7E 00 01 00 05 01 23 45 67 89 00 04 B3 03 E8 00 02 00 D3 7E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
            //{"[7E]\u5F00\u59CB":126,"[0001]\u6D88\u606FId":1,"\u6D88\u606F\u4F53\u5C5E\u6027\u5BF9\u8C61":{"[0000000000000101]\u6D88\u606F\u4F53\u5C5E\u6027":5,"[0]\u4FDD\u7559":0,"[0]\u4FDD\u7559":0,"[0]\u662F\u5426\u5206\u5305":false,"[000]\u6570\u636E\u52A0\u5BC6":"None","[000000101]\u6D88\u606F\u4F53\u957F\u5EA6":5},"[12345678900]\u7EC8\u7AEF\u624B\u673A\u53F7":"12345678900","[04B3]\u6D88\u606F\u6D41\u6C34\u53F7":1203,"\u6570\u636E\u4F53\u5BF9\u8C61":{"\u7EC8\u7AEF\u901A\u7528\u5E94\u7B54":"03E8000200","[03E8]\u5E94\u7B54\u6D41\u6C34\u53F7":1000,"[0002]\u5E94\u7B54\u6D88\u606FId":2,"[0]\u7ED3\u679C":"Success"},"[D3]\u6821\u9A8C\u7801":211,"[D3]\u7ED3\u675F":211}
        }
    }
}
