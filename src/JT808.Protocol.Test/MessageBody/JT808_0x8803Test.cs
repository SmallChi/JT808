using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8803Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8803 jT808_0X8803 = new JT808_0x8803
            {
                ChannelId = 128,
                EventItemCoding = JT808EventItemCoding.平台下发指令.ToByteValue(),
                MultimediaDeleted = JT808MultimediaDeleted.删除.ToByteValue(),
                MultimediaType = JT808MultimediaType.图像.ToByteValue(),
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
            Assert.Equal(JT808EventItemCoding.平台下发指令.ToByteValue(), jT808_0X8803.EventItemCoding);
            Assert.Equal(JT808MultimediaDeleted.删除.ToByteValue(), jT808_0X8803.MultimediaDeleted);
            Assert.Equal(JT808MultimediaType.图像.ToByteValue(), jT808_0X8803.MultimediaType);
            Assert.Equal(DateTime.Parse("2018-11-16 22:00:21"), jT808_0X8803.StartTime);
            Assert.Equal(DateTime.Parse("2018-11-16 23:00:21"), jT808_0X8803.EndTime);
        }
    }
}
