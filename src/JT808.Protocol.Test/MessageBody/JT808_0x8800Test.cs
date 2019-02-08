using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8800Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8800 jT808_0X8800 = new JT808_0x8800
            {
                MultimediaId = 129,
                RetransmitPackageIds = new byte[] { 0x01, 0x02, 0x03, 0x04 }
            };
            string hex = JT808Serializer.Serialize(jT808_0X8800).ToHexString();
            Assert.Equal("000000810201020304", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "000000810201020304".ToHexBytes();
            JT808_0x8800 jT808_0X8800 = JT808Serializer.Deserialize<JT808_0x8800>(bytes);
            Assert.Equal((uint)129, jT808_0X8800.MultimediaId);
            Assert.Equal(2, jT808_0X8800.RetransmitPackageCount);
            Assert.Equal(new byte[] { 0x01, 0x02, 0x03, 0x04 }, jT808_0X8800.RetransmitPackageIds);
        }
    }
}
