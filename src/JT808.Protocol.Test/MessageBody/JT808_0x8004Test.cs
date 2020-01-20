using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8004Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8004 jT808_0X8004 = new JT808_0x8004
            {
                Time = DateTime.Parse("2019-11-26 15:58:50")
            };
            var hex = JT808Serializer.Serialize(jT808_0X8004).ToHexString();
            Assert.Equal("191126155850", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "191126155850".ToHexBytes();
            JT808_0x8004 jT808_0X8004 = JT808Serializer.Deserialize<JT808_0x8004>(bytes);
            Assert.Equal(DateTime.Parse("2019-11-26 15:58:50"), jT808_0X8004.Time);
        }

        [Fact]
        public void Test3()
        {
            var bytes = "191126155850".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8004>(bytes);
        }
    }
}
