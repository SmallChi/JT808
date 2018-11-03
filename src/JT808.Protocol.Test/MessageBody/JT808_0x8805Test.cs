using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8805Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8805 jT808_0X8805 = new JT808_0x8805();
            jT808_0X8805.MultimediaId = 10000;
            jT808_0X8805.MultimediaDeleted = Enums.JT808MultimediaDeleted.保留.ToByteValue();
            string hex = JT808Serializer.Serialize(jT808_0X8805).ToHexString();
            Assert.Equal("0000271000", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "00 00 27 10 00".ToHexBytes();
            JT808_0x8805 jT808_0X8805 = JT808Serializer.Deserialize<JT808_0x8805>(bytes);
            Assert.Equal(Enums.JT808MultimediaDeleted.保留.ToByteValue(), jT808_0X8805.MultimediaDeleted);
            Assert.Equal((uint)10000, jT808_0X8805.MultimediaId);
        }
    }
}
