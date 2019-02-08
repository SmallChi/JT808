using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8802Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8802 jT808_0X8802 = new JT808_0x8802
            {
                ChannelId = 123,
                EventItemCoding = JT808EventItemCoding.碰撞侧翻报警触发.ToByteValue(),
                MultimediaType = JT808MultimediaType.视频.ToByteValue(),
                StartTime = DateTime.Parse("2018-11-16 21:00:08"),
                EndTime = DateTime.Parse("2018-11-16 22:00:08")
            };
            string hex = JT808Serializer.Serialize(jT808_0X8802).ToHexString();
            Assert.Equal("027B03181116210008181116220008", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "027B03181116210008181116220008".ToHexBytes();
            JT808_0x8802 jT808_0X8802 = JT808Serializer.Deserialize<JT808_0x8802>(bytes);
            Assert.Equal(123, jT808_0X8802.ChannelId);
            Assert.Equal(JT808EventItemCoding.碰撞侧翻报警触发.ToByteValue(), jT808_0X8802.EventItemCoding);
            Assert.Equal(JT808MultimediaType.视频.ToByteValue(), jT808_0X8802.MultimediaType);
            Assert.Equal(DateTime.Parse("2018-11-16 21:00:08"), jT808_0X8802.StartTime);
            Assert.Equal(DateTime.Parse("2018-11-16 22:00:08"), jT808_0X8802.EndTime);
        }
    }
}
