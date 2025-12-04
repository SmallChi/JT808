using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0304Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();

        [Fact]
        public void Test1_1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = 0x0304,
                    ManualMsgNum = 1203,
                    TerminalPhoneNo = "012345678900",
                },
                Bodies = new JT808_0x0304
                {
                    ReplyMsgNum=1,
                    MessageType= 0x4E,
                    Message= "SmallChi(Koike)"
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0304001401234567890004B300014E000F536D616C6C436869284B6F696B6529327E", hex);
        }

        [Fact]
        public void Test1_2()
        {
            var bytes = "7E0304001401234567890004B300014E000F536D616C6C436869284B6F696B6529327E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(0x0304, jT808Package.Header.MsgId);
            Assert.Equal(1203, jT808Package.Header.MsgNum);

            JT808_0x0304 JT808Bodies = (JT808_0x0304)jT808Package.Bodies;
            Assert.Equal(1, JT808Bodies.ReplyMsgNum);
            Assert.Equal(0x4E, JT808Bodies.MessageType);
            Assert.Equal("SmallChi(Koike)", JT808Bodies.Message);
        }

        [Fact]
        public void Test2_1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = 0x0304,
                    ManualMsgNum = 1203,
                    TerminalPhoneNo = "012345678900",
                },
                Bodies = new JT808_0x0304
                {
                    ReplyMsgNum = 1,
                    MessageType = 0x4F,
                    Message = "沙县小吃"
                }
            };
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E0304000D01234567890004B300014F0008C9B3CFD8D0A1B3D4097E", hex);
        }

        [Fact]
        public void Test2_2()
        {
            var bytes = "7E0304000D01234567890004B300014F0008C9B3CFD8D0A1B3D4097E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(0x0304, jT808Package.Header.MsgId);
            Assert.Equal(1203, jT808Package.Header.MsgNum);

            JT808_0x0304 JT808Bodies = (JT808_0x0304)jT808Package.Bodies;
            Assert.Equal(1, JT808Bodies.ReplyMsgNum);
            Assert.Equal(0x4F, JT808Bodies.MessageType);
            Assert.Equal("沙县小吃", JT808Bodies.Message);
        }


        [Fact]
        public void Test3_1()
        {
            var bytes = "7E0304001401234567890004B300014E000F536D616C6C436869284B6F696B6529327E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
        }

        [Fact]
        public void Test4_1()
        {
            var bytes = "7E0304000D01234567890004B300014F0008C9B3CFD8D0A1B3D4097E".ToHexBytes();
            string json = JT808Serializer.Analyze(bytes);
        }
    }
}
