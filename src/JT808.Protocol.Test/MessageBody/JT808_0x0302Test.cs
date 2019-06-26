using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0302Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0302 jT808_0X0302 = new JT808_0x0302
            {
                AnswerId = 123,
                ReplySNo = 4521
            };
            var hex = JT808Serializer.Serialize(jT808_0X0302).ToHexString();
            Assert.Equal("11A97B", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "11A97B".ToHexBytes();
            JT808_0x0302 jT808_0X0302 = JT808Serializer.Deserialize<JT808_0x0302>(bytes);
            Assert.Equal(123, jT808_0X0302.AnswerId);
            Assert.Equal(4521, jT808_0X0302.ReplySNo);
        }
    }
}