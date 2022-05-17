using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8400Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8400 jT808_0X8400 = new JT808_0x8400
            {
                CallBack = Enums.JT808CallBackType.normal_call,
                PhoneNumber = "12345679810"
            };
            var hex = JT808Serializer.Serialize(jT808_0X8400).ToHexString();
            Assert.Equal("003132333435363739383130", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "00 31 32 33 34 35 36 37 39 38 31 30".ToHexBytes();
            JT808_0x8400 jT808_0X8400 = JT808Serializer.Deserialize<JT808_0x8400>(bytes);
            Assert.Equal(Enums.JT808CallBackType.normal_call, jT808_0X8400.CallBack);
            Assert.Equal("12345679810", jT808_0X8400.PhoneNumber);
        }

        [Fact]
        public void Test3()
        {
            var bytes = "00 31 32 33 34 35 36 37 39 38 31 30".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8400>(bytes);
        }
    }
}
