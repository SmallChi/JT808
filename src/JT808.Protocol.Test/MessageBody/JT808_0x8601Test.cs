using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8601Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8601 jT808_0X8601 = new JT808_0x8601();
            jT808_0X8601.AreaIds = new List<uint>()
            {
                1,2
            };
            var hex = JT808Serializer.Serialize(jT808_0X8601).ToHexString();
            Assert.Equal("020000000100000002", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "020000000100000002".ToHexBytes();
            JT808_0x8601 jT808_0X8601 = JT808Serializer.Deserialize<JT808_0x8601>(bytes);
            Assert.Equal(2, jT808_0X8601.AreaCount);
            Assert.Equal((uint)1, jT808_0X8601.AreaIds[0]);
            Assert.Equal((uint)2, jT808_0X8601.AreaIds[1]);
        }

        [Fact]
        public void Test3()
        {
            var bytes = "020000000100000002".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8601>(bytes);
        }
    }
}
