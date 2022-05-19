using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0802Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0802 jT808_0X0802 = new JT808_0x0802
            {
                MsgNum = 12369,
                MultimediaSearchItems = new List<JT808MultimediaSearchProperty>()
            };

            jT808_0X0802.MultimediaSearchItems.Add(new JT808MultimediaSearchProperty()
            {
                ChannelId = 123,
                EventItemCoding = JT808EventItemCoding.regular_action.ToByteValue(),
                MultimediaId = 258,
                MultimediaType = JT808MultimediaType.image.ToByteValue(),
                Position = new JT808_0x0200()
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-11-16 20:20:20"),
                    Lat = 12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    StatusFlag = 2,
                }
            });

            jT808_0X0802.MultimediaSearchItems.Add(new JT808MultimediaSearchProperty()
            {
                ChannelId = 124,
                EventItemCoding = JT808EventItemCoding.platform_delivery_order.ToByteValue(),
                MultimediaId = 259,
                MultimediaType = JT808MultimediaType.video.ToByteValue(),
                Position = new JT808_0x0200()
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-11-16 22:22:22"),
                    Lat = 12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    StatusFlag = 2,
                }
            });

            string hex = JT808Serializer.Serialize(jT808_0X0802).ToHexString();
            Assert.Equal("3051000200000102007B01000000010000000200BA7F0E07E4F11C0028003C000018111620202000000103027C00000000010000000200BA7F0E07E4F11C0028003C0000181116222222", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "3051000200000102007B01000000010000000200BA7F0E07E4F11C0028003C000018111620202000000103027C00000000010000000200BA7F0E07E4F11C0028003C0000181116222222".ToHexBytes();
            JT808_0x0802 jT808_0X0802 = JT808Serializer.Deserialize<JT808_0x0802>(bytes);
            Assert.Equal(12369, jT808_0X0802.MsgNum);
            Assert.Equal(2, jT808_0X0802.MultimediaItemCount);


            Assert.Equal(123, jT808_0X0802.MultimediaSearchItems[0].ChannelId);
            Assert.Equal(JT808EventItemCoding.regular_action.ToByteValue(), jT808_0X0802.MultimediaSearchItems[0].EventItemCoding);
            Assert.Equal(JT808MultimediaType.image.ToByteValue(), jT808_0X0802.MultimediaSearchItems[0].MultimediaType);
            Assert.Equal((uint)258, jT808_0X0802.MultimediaSearchItems[0].MultimediaId);

            Assert.Equal((uint)1, jT808_0X0802.MultimediaSearchItems[0].Position.AlarmFlag);
            Assert.Equal((ushort)40, jT808_0X0802.MultimediaSearchItems[0].Position.Altitude);
            Assert.Equal(DateTime.Parse("2018-11-16 20:20:20"), jT808_0X0802.MultimediaSearchItems[0].Position.GPSTime);
            Assert.Equal(12222222, jT808_0X0802.MultimediaSearchItems[0].Position.Lat);
            Assert.Equal(132444444, jT808_0X0802.MultimediaSearchItems[0].Position.Lng);
            Assert.Equal((ushort)60, jT808_0X0802.MultimediaSearchItems[0].Position.Speed);
            Assert.Equal((ushort)0, jT808_0X0802.MultimediaSearchItems[0].Position.Direction);
            Assert.Equal((uint)2, jT808_0X0802.MultimediaSearchItems[0].Position.StatusFlag);

            Assert.Equal(124, jT808_0X0802.MultimediaSearchItems[1].ChannelId);
            Assert.Equal(JT808EventItemCoding.platform_delivery_order.ToByteValue(), jT808_0X0802.MultimediaSearchItems[1].EventItemCoding);
            Assert.Equal(JT808MultimediaType.video.ToByteValue(), jT808_0X0802.MultimediaSearchItems[1].MultimediaType);
            Assert.Equal((uint)259, jT808_0X0802.MultimediaSearchItems[1].MultimediaId);

            Assert.Equal((uint)1, jT808_0X0802.MultimediaSearchItems[1].Position.AlarmFlag);
            Assert.Equal((ushort)40, jT808_0X0802.MultimediaSearchItems[1].Position.Altitude);
            Assert.Equal(DateTime.Parse("2018-11-16 22:22:22"), jT808_0X0802.MultimediaSearchItems[1].Position.GPSTime);
            Assert.Equal(12222222, jT808_0X0802.MultimediaSearchItems[1].Position.Lat);
            Assert.Equal(132444444, jT808_0X0802.MultimediaSearchItems[1].Position.Lng);
            Assert.Equal((ushort)60, jT808_0X0802.MultimediaSearchItems[1].Position.Speed);
            Assert.Equal((ushort)0, jT808_0X0802.MultimediaSearchItems[1].Position.Direction);
            Assert.Equal((uint)2, jT808_0X0802.MultimediaSearchItems[1].Position.StatusFlag);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "3051000200000102007B01000000010000000200BA7F0E07E4F11C0028003C000018111620202000000103027C00000000010000000200BA7F0E07E4F11C0028003C0000181116222222".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0802>(bytes);
        }

        [Fact]
        public void Test2011_1()
        {
            JT808_0x0802 jT808_0X0802 = new JT808_0x0802
            {
                MsgNum = 12369,
                MultimediaSearchItems = new List<JT808MultimediaSearchProperty>()
            };

            jT808_0X0802.MultimediaSearchItems.Add(new JT808MultimediaSearchProperty()
            {
                ChannelId = 123,
                EventItemCoding = JT808EventItemCoding.regular_action.ToByteValue(),
                MultimediaType = JT808MultimediaType.image.ToByteValue(),
                Position = new JT808_0x0200()
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-11-16 20:20:20"),
                    Lat = 12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    StatusFlag = 2,
                }
            });

            jT808_0X0802.MultimediaSearchItems.Add(new JT808MultimediaSearchProperty()
            {
                ChannelId = 124,
                EventItemCoding = JT808EventItemCoding.platform_delivery_order.ToByteValue(),
                MultimediaType = JT808MultimediaType.video.ToByteValue(),
                Position = new JT808_0x0200()
                {
                    AlarmFlag = 1,
                    Altitude = 40,
                    GPSTime = DateTime.Parse("2018-11-16 22:22:22"),
                    Lat = 12222222,
                    Lng = 132444444,
                    Speed = 60,
                    Direction = 0,
                    StatusFlag = 2,
                }
            });

            string hex = JT808Serializer.Serialize(jT808_0X0802,JT808Version.JTT2011).ToHexString();
            Assert.Equal("30510002007B01000000010000000200BA7F0E07E4F11C0028003C0000181116202020027C00000000010000000200BA7F0E07E4F11C0028003C0000181116222222", hex);
        }

        [Fact]
        public void Test2011_2()
        {
            byte[] bytes = "30510002007B01000000010000000200BA7F0E07E4F11C0028003C0000181116202020027C00000000010000000200BA7F0E07E4F11C0028003C0000181116222222".ToHexBytes();
            JT808_0x0802 jT808_0X0802 = JT808Serializer.Deserialize<JT808_0x0802>(bytes);
            Assert.Equal(12369, jT808_0X0802.MsgNum);
            Assert.Equal(2, jT808_0X0802.MultimediaItemCount);


            Assert.Equal(123, jT808_0X0802.MultimediaSearchItems[0].ChannelId);
            Assert.Equal(JT808EventItemCoding.regular_action.ToByteValue(), jT808_0X0802.MultimediaSearchItems[0].EventItemCoding);
            Assert.Equal(JT808MultimediaType.image.ToByteValue(), jT808_0X0802.MultimediaSearchItems[0].MultimediaType);

            Assert.Equal((uint)1, jT808_0X0802.MultimediaSearchItems[0].Position.AlarmFlag);
            Assert.Equal((ushort)40, jT808_0X0802.MultimediaSearchItems[0].Position.Altitude);
            Assert.Equal(DateTime.Parse("2018-11-16 20:20:20"), jT808_0X0802.MultimediaSearchItems[0].Position.GPSTime);
            Assert.Equal(12222222, jT808_0X0802.MultimediaSearchItems[0].Position.Lat);
            Assert.Equal(132444444, jT808_0X0802.MultimediaSearchItems[0].Position.Lng);
            Assert.Equal((ushort)60, jT808_0X0802.MultimediaSearchItems[0].Position.Speed);
            Assert.Equal((ushort)0, jT808_0X0802.MultimediaSearchItems[0].Position.Direction);
            Assert.Equal((uint)2, jT808_0X0802.MultimediaSearchItems[0].Position.StatusFlag);

            Assert.Equal(124, jT808_0X0802.MultimediaSearchItems[1].ChannelId);
            Assert.Equal(JT808EventItemCoding.platform_delivery_order.ToByteValue(), jT808_0X0802.MultimediaSearchItems[1].EventItemCoding);
            Assert.Equal(JT808MultimediaType.video.ToByteValue(), jT808_0X0802.MultimediaSearchItems[1].MultimediaType);

            Assert.Equal((uint)1, jT808_0X0802.MultimediaSearchItems[1].Position.AlarmFlag);
            Assert.Equal((ushort)40, jT808_0X0802.MultimediaSearchItems[1].Position.Altitude);
            Assert.Equal(DateTime.Parse("2018-11-16 22:22:22"), jT808_0X0802.MultimediaSearchItems[1].Position.GPSTime);
            Assert.Equal(12222222, jT808_0X0802.MultimediaSearchItems[1].Position.Lat);
            Assert.Equal(132444444, jT808_0X0802.MultimediaSearchItems[1].Position.Lng);
            Assert.Equal((ushort)60, jT808_0X0802.MultimediaSearchItems[1].Position.Speed);
            Assert.Equal((ushort)0, jT808_0X0802.MultimediaSearchItems[1].Position.Direction);
            Assert.Equal((uint)2, jT808_0X0802.MultimediaSearchItems[1].Position.StatusFlag);
        }

        [Fact]
        public void Test2011_3()
        {
            byte[] bytes = "30510002007B01000000010000000200BA7F0E07E4F11C0028003C0000181116202020027C00000000010000000200BA7F0E07E4F11C0028003C0000181116222222".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0802>(bytes);
        }
    }
}
