using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x0800Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0800 jT808_0X0800 = new JT808_0x0800
            {
                MultimediaId = 5569,
                MultimediaType = 0x01,
                MultimediaCodingFormat = 0x02,
                ChannelId = 0x06,
                EventItemCoding = 0x01
            };
            var hex = JT808Serializer.Serialize(jT808_0X0800).ToHexString();
            Assert.Equal("000015C101020106", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "000015C101020106".ToHexBytes();
            JT808_0x0800 jT808_0X0800 = JT808Serializer.Deserialize<JT808_0x0800>(bytes);
            Assert.Equal((uint)5569, jT808_0X0800.MultimediaId);
            Assert.Equal(0x01, jT808_0X0800.MultimediaType);
            Assert.Equal(0x02, jT808_0X0800.MultimediaCodingFormat);
            Assert.Equal(0x06, jT808_0X0800.ChannelId);
            Assert.Equal(0x01, jT808_0X0800.EventItemCoding);
        }

        [Fact]
        public void Test3()
        {
            var bytes = "000015C101020106".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0800>(bytes);
        }
    }
}
