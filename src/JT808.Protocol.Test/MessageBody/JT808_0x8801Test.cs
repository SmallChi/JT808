using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8801Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8801 jT808_0X8801 = new JT808_0x8801
            {
                ChannelId = 128,
                Chroma = 245,
                Contrast = 126,
                Lighting = 235,
                Resolution = 0x08,
                Saturability = 120,
                SaveFlag = 1,
                ShootingCommand = 0,
                VideoQuality = 9,
                VideoTime = 2686
            };
            string hex = JT808Serializer.Serialize(jT808_0X8801).ToHexString();
            Assert.Equal("8000000A7E010809EB7E78F5", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "8000000A7E010809EB7E78F5".ToHexBytes();
            JT808_0x8801 jT808_0X8801 = JT808Serializer.Deserialize<JT808_0x8801>(bytes);
            Assert.Equal(128, jT808_0X8801.ChannelId);
            Assert.Equal(245, jT808_0X8801.Chroma);
            Assert.Equal(126, jT808_0X8801.Contrast);
            Assert.Equal(235, jT808_0X8801.Lighting);
            Assert.Equal(0x08, jT808_0X8801.Resolution);
            Assert.Equal(120, jT808_0X8801.Saturability);
            Assert.Equal(1, jT808_0X8801.SaveFlag);
            Assert.Equal(0, jT808_0X8801.ShootingCommand);
            Assert.Equal(9, jT808_0X8801.VideoQuality);
            Assert.Equal(2686, jT808_0X8801.VideoTime);
        }
        [Fact]
        public void Test3()
        {
            byte[] bytes = "8000000A7E010809EB7E78F5".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8801>(bytes);
        }
    }
}
