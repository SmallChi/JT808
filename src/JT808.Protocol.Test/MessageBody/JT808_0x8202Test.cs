using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.MessageBodySend
{
    public class JT808_0x8202Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8202 jT808_0X8202 = new JT808_0x8202();
            jT808_0X8202.Interval = 69;
            jT808_0X8202.LocationTrackingValidity = 123;
            string hex = JT808Serializer.Serialize(jT808_0X8202).ToHexString();
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "00 45 00 00 00 7B".ToHexBytes();
            JT808_0x8202 jT808_0X8108 = JT808Serializer.Deserialize<JT808_0x8202>(bytes);
            Assert.Equal(69, jT808_0X8108.Interval);
            Assert.Equal(123, jT808_0X8108.LocationTrackingValidity);
        }
    }
}
