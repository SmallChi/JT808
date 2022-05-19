using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8803Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8803 jT808_0X8803 = new JT808_0x8803
            {
                ChannelId = 128,
                EventItemCoding = JT808EventItemCoding.platform_delivery_order.ToByteValue(),
                MultimediaDeleted = JT808MultimediaDeleted.delete.ToByteValue(),
                MultimediaType = JT808MultimediaType.image.ToByteValue(),
                StartTime = DateTime.Parse("2018-11-16 22:00:21"),
                EndTime = DateTime.Parse("2018-11-16 23:00:21")
            };
            string hex = JT808Serializer.Serialize(jT808_0X8803).ToHexString();
            Assert.Equal("00800018111622002118111623002101", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "00800018111622002118111623002101".ToHexBytes();
            JT808_0x8803 jT808_0X8803 = JT808Serializer.Deserialize<JT808_0x8803>(bytes);
            Assert.Equal(128, jT808_0X8803.ChannelId);
            Assert.Equal(JT808EventItemCoding.platform_delivery_order.ToByteValue(), jT808_0X8803.EventItemCoding);
            Assert.Equal(JT808MultimediaDeleted.delete.ToByteValue(), jT808_0X8803.MultimediaDeleted);
            Assert.Equal(JT808MultimediaType.image.ToByteValue(), jT808_0X8803.MultimediaType);
            Assert.Equal(DateTime.Parse("2018-11-16 22:00:21"), jT808_0X8803.StartTime);
            Assert.Equal(DateTime.Parse("2018-11-16 23:00:21"), jT808_0X8803.EndTime);
        }
        [Fact]
        public void Test3()
        {
            byte[] bytes = "00800018111622002118111623002101".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8803>(bytes);
        }
    }
}
