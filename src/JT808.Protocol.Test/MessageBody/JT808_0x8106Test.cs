using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8106Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8106 jT808_0X8106 = new JT808_0x8106
            {
                ParameterCount = 2,
                Parameters = new uint[] { 0x0001, 0x0002 }
            };

            string hex = JT808Serializer.Serialize(jT808_0X8106).ToHexString();
            Assert.Equal("020000000100000002", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "020000000100000002".ToHexBytes();
            JT808_0x8106 jT808_0X8106 = JT808Serializer.Deserialize<JT808_0x8106>(bytes);
            Assert.Equal(2, jT808_0X8106.ParameterCount);
            Assert.Equal(new uint[] { 0x0001, 0x0002 }, jT808_0X8106.Parameters);
        }
    }
}
