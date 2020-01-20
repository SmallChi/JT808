using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8202Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8202 jT808_0X8202 = new JT808_0x8202
            {
                Interval = 69,
                LocationTrackingValidity = 123
            };
            string hex = JT808Serializer.Serialize(jT808_0X8202).ToHexString();
            Assert.Equal("00450000007B", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "00 45 00 00 00 7B".ToHexBytes();
            JT808_0x8202 jT808_0X8108 = JT808Serializer.Deserialize<JT808_0x8202>(bytes);
            Assert.Equal(69, jT808_0X8108.Interval);
            Assert.Equal(123, jT808_0X8108.LocationTrackingValidity);
        }
        [Fact]
        public void Test3()
        {
            byte[] bytes = "00 45 00 00 00 7B".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8202>(bytes);
        }
    }
}
