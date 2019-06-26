using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0303Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0303 jT808_0X0303 = new JT808_0x0303
            {
                Flag = 123,
                InformationType = 12
            };
            var hex = JT808Serializer.Serialize(jT808_0X0303).ToHexString();
            Assert.Equal("0C7B", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "0C7B".ToHexBytes();
            JT808_0x0303 jT808_0X0303 = JT808Serializer.Deserialize<JT808_0x0303>(bytes);
            Assert.Equal(123, jT808_0X0303.Flag);
            Assert.Equal(12, jT808_0X0303.InformationType);
        }
    }
}