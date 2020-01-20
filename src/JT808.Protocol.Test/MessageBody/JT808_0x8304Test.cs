using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x8304Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8304 jT808_0X8304 = new JT808_0x8304
            {
                InformationType = 123,
                InformationContent = "信息内容"
            };
            var hex = JT808Serializer.Serialize(jT808_0X8304).ToHexString();
            Assert.Equal("7B0008D0C5CFA2C4DAC8DD", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7B0008D0C5CFA2C4DAC8DD".ToHexBytes();
            JT808_0x8304 jT808_0X8304 = JT808Serializer.Deserialize<JT808_0x8304>(bytes);
            Assert.Equal(123, jT808_0X8304.InformationType);
            Assert.Equal("信息内容", jT808_0X8304.InformationContent);
            Assert.Equal(8, jT808_0X8304.InformationLength);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "7B0008D0C5CFA2C4DAC8DD".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8304>(bytes);
        }
    }
}
