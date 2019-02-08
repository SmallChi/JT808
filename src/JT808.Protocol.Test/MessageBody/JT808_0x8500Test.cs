using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8500Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8500 jT808_0X8500 = new JT808_0x8500
            {
                ControlFlag = 12
            };
            var hex = JT808Serializer.Serialize(jT808_0X8500).ToHexString();
            Assert.Equal("0C", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "0C".ToHexBytes();
            JT808_0x8500 jT808_0X8500 = JT808Serializer.Deserialize<JT808_0x8500>(bytes);
            Assert.Equal(12, jT808_0X8500.ControlFlag);
        }
    }
}
