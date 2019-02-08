using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBodyReply
{
    public class JT808_0x0108Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0108 jT808_0X0108 = new JT808_0x0108
            {
                UpgradeType = JT808UpgradeType.北斗卫星定位模块,
                UpgradeResult = JT808UpgradeResult.成功
            };
            string hex = JT808Serializer.Serialize(jT808_0X0108).ToHexString();
            Assert.Equal("3400", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "34 00".ToHexBytes();
            JT808_0x0108 jT808_0X0108 = JT808Serializer.Deserialize<JT808_0x0108>(bytes);
            Assert.Equal(JT808UpgradeResult.成功, jT808_0X0108.UpgradeResult);
            Assert.Equal(JT808UpgradeType.北斗卫星定位模块, jT808_0X0108.UpgradeType);
        }
    }
}
