using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8608Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();

        [Fact]
        public void Test1()
        {
            JT808_0x8608 value = new JT808_0x8608
            {
                 QueryType=0,
                 Ids=new List<uint>() { 1,2,3}
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("0000000003000000010000000200000003", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0000000003000000010000000200000003".ToHexBytes();
            JT808_0x8608 value = JT808Serializer.Deserialize<JT808_0x8608>(bytes);
            Assert.Equal(0,  value.QueryType);
            Assert.Equal(3u, value.Count);
            Assert.Equal(1u, value.Ids[0]);
            Assert.Equal(2u, value.Ids[1]);
            Assert.Equal(3u, value.Ids[2]);
        }

        [Fact]
        public void Test3()
        {
            JT808_0x8608 value = new JT808_0x8608
            {
                QueryType = 1
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("0100000000", hex);
        }

        [Fact]
        public void Test4()
        {
            byte[] bytes = "0100000000".ToHexBytes();
            JT808_0x8608 value = JT808Serializer.Deserialize<JT808_0x8608>(bytes);
            Assert.Equal(1, value.QueryType);
            Assert.Equal(0u, value.Count);
        }

        [Fact]
        public void Test5()
        {
            byte[] bytes = "0000000003000000010000000200000003".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8608>(bytes);
        }
    }
}
