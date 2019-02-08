using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x8302Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8302 jT808_0X8302 = new JT808_0x8302
            {
                AnswerId = 128,
                AnswerContent = "123456",
                Flag = 1,
                Issue = "sdddaff"
            };
            var hex = JT808Serializer.Serialize(jT808_0X8302).ToHexString();
            //01
            //07
            //73 64 64 64 61 66 66
            //80
            //06 00 
            //31 32 33 34 35 36
            Assert.Equal("010773646464616666800006313233343536", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "010773646464616666800006313233343536".ToHexBytes();
            JT808_0x8302 jT808_0X8302 = JT808Serializer.Deserialize<JT808_0x8302>(bytes);
            Assert.Equal(128, jT808_0X8302.AnswerId);
            Assert.Equal("123456", jT808_0X8302.AnswerContent);
            Assert.Equal(1, jT808_0X8302.Flag);
            Assert.Equal("sdddaff", jT808_0X8302.Issue);
            Assert.Equal(6, jT808_0X8302.AnswerContentLength);
            Assert.Equal(7, jT808_0X8302.IssueContentLength);
        }
    }
}
