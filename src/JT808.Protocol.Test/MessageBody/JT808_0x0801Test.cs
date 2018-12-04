using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0801Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0801 jT808_0X0801 = new JT808_0x0801();
            jT808_0X0801.ChannelId = 123;
            jT808_0X0801.EventItemCoding = JT808EventItemCoding.定时动作.ToByteValue();
            jT808_0X0801.MultimediaCodingFormat = JT808MultimediaCodingFormat.JPEG.ToByteValue();
            jT808_0X0801.MultimediaId = 2567;
            jT808_0X0801.MultimediaType = JT808MultimediaType.图像.ToByteValue();
            jT808_0X0801.MultimediaDataPackage = new byte[] { 0x01, 0x02, 0x03, 0x04 };
            jT808_0X0801.Position = new JT808_0x0200();
            jT808_0X0801.Position.AlarmFlag = 1;
            jT808_0X0801.Position.Altitude = 40;
            jT808_0X0801.Position.GPSTime = DateTime.Parse("2018-11-15 23:26:10");
            jT808_0X0801.Position.Lat = 12222222;
            jT808_0X0801.Position.Lng = 132444444;
            jT808_0X0801.Position.Speed = 60;
            jT808_0X0801.Position.Direction = 0;
            jT808_0X0801.Position.StatusFlag = 2;
            string hex = JT808Serializer.Serialize(jT808_0X0801).ToHexString();
            Assert.Equal("00000A070000017B000000010000000200BA7F0E07E4F11C0028003C000018111523261001020304", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "00000A070000017B000000010000000200BA7F0E07E4F11C0028003C000018111523261001020304".ToHexBytes();
            JT808_0x0801 jT808_0X0801 = JT808Serializer.Deserialize<JT808_0x0801>(bytes);
            Assert.Equal(123, jT808_0X0801.ChannelId);
            Assert.Equal(JT808EventItemCoding.定时动作.ToByteValue(), jT808_0X0801.EventItemCoding);
            Assert.Equal(JT808MultimediaCodingFormat.JPEG.ToByteValue(), jT808_0X0801.MultimediaCodingFormat);
            Assert.Equal((uint)2567, jT808_0X0801.MultimediaId);
            Assert.Equal(JT808MultimediaType.图像.ToByteValue(), jT808_0X0801.MultimediaType);
            Assert.Equal(new byte[] { 0x01, 0x02, 0x03, 0x04 }, jT808_0X0801.MultimediaDataPackage);
            Assert.Equal((uint)1, jT808_0X0801.Position.AlarmFlag);
            Assert.Equal(40, jT808_0X0801.Position.Altitude);
            Assert.Equal(DateTime.Parse("2018-11-15 23:26:10"), jT808_0X0801.Position.GPSTime);
            Assert.Equal(40, jT808_0X0801.Position.Altitude);
            Assert.Equal(12222222, jT808_0X0801.Position.Lat);
            Assert.Equal(132444444, jT808_0X0801.Position.Lng);
            Assert.Equal(60, jT808_0X0801.Position.Speed);
            Assert.Equal(0, jT808_0X0801.Position.Direction);
            Assert.Equal((uint)2, jT808_0X0801.Position.StatusFlag);
        }
    }
}
