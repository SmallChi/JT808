using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8605Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8605 jT808_0X8605 = new JT808_0x8605();
            jT808_0X8605.AreaIds = new List<uint>()
            {
                2838,1717,772
            };

            var hex = JT808Serializer.Serialize(jT808_0X8605).ToHexString();
            Assert.Equal("0300000B16000006B500000304", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0300000B16000006B500000304".ToHexBytes();
            JT808_0x8605 jT808_0X8605 = JT808Serializer.Deserialize<JT808_0x8605>(bytes);
            Assert.Equal((uint)2838, jT808_0X8605.AreaIds[0]);
            Assert.Equal((uint)1717, jT808_0X8605.AreaIds[1]);
            Assert.Equal((uint)772, jT808_0X8605.AreaIds[2]);
            Assert.Equal(3, jT808_0X8605.AreaCount);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "0300000B16000006B500000304".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8605>(bytes);
        }
    }
}
