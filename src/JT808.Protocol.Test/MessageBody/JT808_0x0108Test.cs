using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0108Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0108 jT808_0X0108 = new JT808_0x0108
            {
                UpgradeType = JT808UpgradeType.beidou_module,
                UpgradeResult = JT808UpgradeResult.success
            };
            string hex = JT808Serializer.Serialize(jT808_0X0108).ToHexString();
            Assert.Equal("3400", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "34 00".ToHexBytes();
            JT808_0x0108 jT808_0X0108 = JT808Serializer.Deserialize<JT808_0x0108>(bytes);
            Assert.Equal(JT808UpgradeResult.success, jT808_0X0108.UpgradeResult);
            Assert.Equal(JT808UpgradeType.beidou_module, jT808_0X0108.UpgradeType);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "34 00".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0108>(bytes);
        }
    }
}
