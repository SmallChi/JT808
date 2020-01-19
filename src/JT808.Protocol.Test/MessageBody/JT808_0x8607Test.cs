using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x8607Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();

        [Fact]
        public void Test1()
        {
            JT808_0x8607 value = new JT808_0x8607();
            value.AreaIds = new List<uint>()
            {
                1,2,3,4
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("0400000001000000020000000300000004", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0400000001000000020000000300000004".ToHexBytes();
            JT808_0x8607 value = JT808Serializer.Deserialize<JT808_0x8607>(bytes);
            Assert.Equal(4, value.AreaCount);
            Assert.Equal(1u, value.AreaIds[0]);
            Assert.Equal(2u, value.AreaIds[1]);
            Assert.Equal(3u, value.AreaIds[2]);
            Assert.Equal(4u, value.AreaIds[3]);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "0400000001000000020000000300000004".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8607>(bytes);
        }
    }
}
