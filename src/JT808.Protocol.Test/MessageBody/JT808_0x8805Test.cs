using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8805Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8805 jT808_0X8805 = new JT808_0x8805
            {
                MultimediaId = 10000,
                MultimediaDeleted = Enums.JT808MultimediaDeleted.reserve.ToByteValue()
            };
            string hex = JT808Serializer.Serialize(jT808_0X8805).ToHexString();
            Assert.Equal("0000271000", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "00 00 27 10 00".ToHexBytes();
            JT808_0x8805 jT808_0X8805 = JT808Serializer.Deserialize<JT808_0x8805>(bytes);
            Assert.Equal(Enums.JT808MultimediaDeleted.reserve.ToByteValue(), jT808_0X8805.MultimediaDeleted);
            Assert.Equal((uint)10000, jT808_0X8805.MultimediaId);
        }
        [Fact]
        public void Test3()
        {
            byte[] bytes = "0000271000".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8805>(bytes);
        }
    }
}
