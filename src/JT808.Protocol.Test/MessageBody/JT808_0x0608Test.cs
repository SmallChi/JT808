using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0608Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();

        [Fact]
        public void Test1()
        {
            JT808_0x0608 value = new JT808_0x0608
            {
                 QueryType=1,
                 Ids=new List<uint>() { 1,2,3}
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("0100000003000000010000000200000003", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0100000003000000010000000200000003".ToHexBytes();
            JT808_0x0608 value = JT808Serializer.Deserialize<JT808_0x0608>(bytes);
            Assert.Equal(1,  value.QueryType);
            Assert.Equal(3u, value.Count);
            Assert.Equal(1u, value.Ids[0]);
            Assert.Equal(2u, value.Ids[1]);
            Assert.Equal(3u, value.Ids[2]);
        }
    }
}
