using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBodySend
{
    public class JT808_0x8108Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8108 jT808_0X8108 = new JT808_0x8108
            {
                UpgradeType = JT808UpgradeType.北斗卫星定位模块,
                MakerId = "asdfg",
                VersionNum = "qscvhiuytrew",
                UpgradePackage = new byte[] { 1, 2, 3, 4, 5 }
            };
            //"34 61 73 64 66 67 0C 71 73 63 76 68 69 75 79 74 72 65 77 00 00 00 05 01 02 03 04 05"
            string hex = JT808Serializer.Serialize(jT808_0X8108).ToHexString();
            Assert.Equal("3461736466670C717363766869757974726577000000050102030405", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "34 61 73 64 66 67 0C 71 73 63 76 68 69 75 79 74 72 65 77 00 00 00 05 01 02 03 04 05".ToHexBytes();
            JT808_0x8108 jT808_0X8108 = JT808Serializer.Deserialize<JT808_0x8108>(bytes);
            Assert.Equal(JT808UpgradeType.北斗卫星定位模块, jT808_0X8108.UpgradeType);
            Assert.Equal("asdfg", jT808_0X8108.MakerId);
            Assert.Equal("qscvhiuytrew", jT808_0X8108.VersionNum);
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5 }, jT808_0X8108.UpgradePackage);
        }
    }
}
