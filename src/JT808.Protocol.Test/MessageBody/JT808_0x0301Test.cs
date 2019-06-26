using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0301Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0301 jT808_0X0301 = new JT808_0x0301
            {
                EventId = 123
            };
            var hex = JT808Serializer.Serialize(jT808_0X0301).ToHexString();
            Assert.Equal("7B", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "7B".ToHexBytes();
            JT808_0x0301 jT808_0X8104 = JT808Serializer.Deserialize<JT808_0x0301>(bytes);
            Assert.Equal(123, jT808_0X8104.EventId);
        }
    }
}