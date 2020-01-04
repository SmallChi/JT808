using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x0005Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0005 jT808_0X8005 = new JT808_0x0005
            {
                OriginalMsgNum = 1234,
                AgainPackageData = new byte[] { 0x01, 0x02, 0x02, 0x03 }
            };
            var hex = JT808Serializer.Serialize(jT808_0X8005).ToHexString();
            Assert.Equal("04D20201020203", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "04D20201020203".ToHexBytes();
            JT808_0x0005 jT808_0X8005 = JT808Serializer.Deserialize<JT808_0x0005>(bytes);
            Assert.Equal(1234, jT808_0X8005.OriginalMsgNum);
            Assert.Equal(new byte[] { 0x01, 0x02, 0x02, 0x03 }, jT808_0X8005.AgainPackageData);
            Assert.Equal(2, jT808_0X8005.AgainPackageCount);
        }

        [Fact]
        public void Test3()
        {
            var bytes = "04D20201020203".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0005>(bytes);
        }
    }
}
