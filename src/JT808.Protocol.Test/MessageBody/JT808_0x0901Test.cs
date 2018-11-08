using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x0901Test
    {
        const string UserName = "smallchi";

        [Fact]
        public void Test1()
        {
            JT808_0x0901 jT808_0X0901 = new JT808_0x0901();
            jT808_0X0901.CompressMessage = Encoding.UTF8.GetBytes(UserName);
            var hex = JT808Serializer.Serialize(jT808_0X0901).ToHexString();
            Assert.Equal("0000001C1F8B080000000000000B2BCE4DCCC949CEC804001D27DD9008000000", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0000001C1F8B080000000000000B2BCE4DCCC949CEC804001D27DD9008000000".ToHexBytes();
            JT808_0x0901 jT808_0X8600 = JT808Serializer.Deserialize<JT808_0x0901>(bytes);
            Assert.Equal((uint)28, jT808_0X8600.CompressMessageLength);
            Assert.Equal(Encoding.UTF8.GetBytes(UserName), jT808_0X8600.CompressMessage);
        }

    }
}
