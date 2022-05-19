using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8600Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8600 jT808_0X8600 = new JT808_0x8600
            {
                SettingAreaProperty = JT808SettingProperty.append_region.ToByteValue(),
                AreaItems = new List<JT808CircleAreaProperty>()
            };
            jT808_0X8600.AreaItems.Add(new JT808CircleAreaProperty
            {
                AreaId = 1522,
                AreaProperty = 222,
                CenterPointLat = 123456789,
                CenterPointLng = 123456789,
                Radius = 200,
                StartTime = DateTime.Parse("2018-10-18 00:00:12"),
                EndTime = DateTime.Parse("2018-10-19 00:00:12"),
                HighestSpeed = 60,
                OverspeedDuration = 200
            });
            jT808_0X8600.AreaItems.Add(new JT808CircleAreaProperty
            {
                AreaId = 1523,
                AreaProperty = 0,
                CenterPointLat = 123456789,
                CenterPointLng = 123456789,
                Radius = 200,
                StartTime = DateTime.Parse("2018-10-18 00:00:12"),
                EndTime = DateTime.Parse("2018-10-19 00:00:12"),
                HighestSpeed = 60,
                OverspeedDuration = 200
            });
            jT808_0X8600.AreaItems.Add(new JT808CircleAreaProperty
            {
                AreaId = 1524,
                AreaProperty = 2211,
                CenterPointLat = 123456789,
                CenterPointLng = 123456789,
                Radius = 200,
                StartTime = DateTime.Parse("2018-10-18 00:00:12"),
                EndTime = DateTime.Parse("2018-10-19 00:00:12"),
                HighestSpeed = 60,
                OverspeedDuration = 200
            });
            var hex = JT808Serializer.Serialize(jT808_0X8600).ToHexString();
            //"01 03 00 00 05 F2 00 DE 07 5B CD 15 07 5B CD 15 00 00 00 C8 00 3C C8 00 00 05 F3 00 00 07 5B CD 15 07 5B CD 15 00 00 00 C8 00 00 05 F4 08 A3 07 5B CD 15 07 5B CD 15 00 00 00 C8 18 10 18 00 00 12 18 10 19 00 00 12 00 3C C8"
            Assert.Equal("0103000005F200DE075BCD15075BCD15000000C8003CC8000005F30000075BCD15075BCD15000000C8000005F408A3075BCD15075BCD15000000C8181018000012181019000012003CC8", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "01 03 00 00 05 F2 00 DE 07 5B CD 15 07 5B CD 15 00 00 00 C8 00 3C C8 00 00 05 F3 00 00 07 5B CD 15 07 5B CD 15 00 00 00 C8 00 00 05 F4 08 A3 07 5B CD 15 07 5B CD 15 00 00 00 C8 18 10 18 00 00 12 18 10 19 00 00 12 00 3C C8".ToHexBytes();
            JT808_0x8600 jT808_0X8600 = JT808Serializer.Deserialize<JT808_0x8600>(bytes);

            Assert.Equal(JT808SettingProperty.append_region.ToByteValue(), jT808_0X8600.SettingAreaProperty);
            Assert.Equal(3, jT808_0X8600.AreaCount);

            var item0 = jT808_0X8600.AreaItems[0];
            Assert.Equal((uint)1522, item0.AreaId);
            Assert.Equal((ushort)222, item0.AreaProperty);
            Assert.Equal((uint)123456789, item0.CenterPointLat);
            Assert.Equal((uint)123456789, item0.CenterPointLng);
            Assert.Equal((uint)200, item0.Radius);
            Assert.Null(item0.StartTime);
            Assert.Null(item0.EndTime);
            Assert.Equal((ushort)60, item0.HighestSpeed);
            Assert.Equal((byte)200, item0.OverspeedDuration);

            var item1 = jT808_0X8600.AreaItems[1];
            Assert.Equal((uint)1523, item1.AreaId);
            Assert.Equal(0, item1.AreaProperty);
            Assert.Equal((uint)123456789, item1.CenterPointLat);
            Assert.Equal((uint)123456789, item1.CenterPointLng);
            Assert.Equal((uint)200, item1.Radius);
            Assert.Null(item1.StartTime);
            Assert.Null(item1.EndTime);
            Assert.Null(item1.HighestSpeed);
            Assert.Null(item1.OverspeedDuration);


            var item2 = jT808_0X8600.AreaItems[2];
            Assert.Equal((uint)1524, item2.AreaId);
            Assert.Equal((ushort)2211, item2.AreaProperty);
            Assert.Equal((uint)123456789, item2.CenterPointLat);
            Assert.Equal((uint)123456789, item2.CenterPointLng);
            Assert.Equal((uint)200, item2.Radius);
            Assert.Equal(DateTime.Parse("2018-10-18 00:00:12"), item2.StartTime);
            Assert.Equal(DateTime.Parse("2018-10-19 00:00:12"), item2.EndTime);
            Assert.Equal((ushort)60, item2.HighestSpeed);
            Assert.Equal((byte)200, item2.OverspeedDuration);

        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "01 03 00 00 05 F2 00 DE 07 5B CD 15 07 5B CD 15 00 00 00 C8 00 3C C8 00 00 05 F3 00 00 07 5B CD 15 07 5B CD 15 00 00 00 C8 00 00 05 F4 08 A3 07 5B CD 15 07 5B CD 15 00 00 00 C8 18 10 18 00 00 12 18 10 19 00 00 12 00 3C C8".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8600>(bytes);
        }


        [Fact]
        public void Test_2019_1()
        {
            JT808_0x8600 jT808_0X8600 = new JT808_0x8600
            {
                SettingAreaProperty = JT808SettingProperty.append_region.ToByteValue(),
                AreaItems = new List<JT808CircleAreaProperty>()
            };
            jT808_0X8600.AreaItems.Add(new JT808CircleAreaProperty
            {
                AreaId = 1522,
                AreaProperty = 222,
                CenterPointLat = 123456789,
                CenterPointLng = 123456789,
                Radius = 200,
                StartTime = DateTime.Parse("2019-11-30 00:00:12"),
                EndTime = DateTime.Parse("2019-11-30 00:00:12"),
                HighestSpeed = 60,
                OverspeedDuration = 200,
                NightMaximumSpeed=666,
                AreaName="SmallChi"
            });
            var hex = JT808Serializer.Serialize(jT808_0X8600,JT808Version.JTT2019).ToHexString();
            
            Assert.Equal("0101000005F200DE075BCD15075BCD15000000C8003CC8029A0008536D616C6C436869", hex);
        }

        [Fact]
        public void Test_2019_2()
        {
            byte[] bytes = "0101000005F200DE075BCD15075BCD15000000C8003CC8029A0008536D616C6C436869".ToHexBytes();
            JT808_0x8600 jT808_0X8600 = JT808Serializer.Deserialize<JT808_0x8600>(bytes, JT808Version.JTT2019);

            Assert.Equal(JT808SettingProperty.append_region.ToByteValue(), jT808_0X8600.SettingAreaProperty);
            Assert.Equal(1, jT808_0X8600.AreaCount);
            var item0 = jT808_0X8600.AreaItems[0];
            Assert.Equal((uint)1522, item0.AreaId);
            Assert.Equal((ushort)222, item0.AreaProperty);
            Assert.Equal((uint)123456789, item0.CenterPointLat);
            Assert.Equal((uint)123456789, item0.CenterPointLng);
            Assert.Equal((uint)200, item0.Radius);
            Assert.Null(item0.StartTime);
            Assert.Null(item0.EndTime);
            Assert.Equal((ushort)60, item0.HighestSpeed);
            Assert.Equal((byte)200, item0.OverspeedDuration);
            Assert.Equal(666, item0.NightMaximumSpeed);
            Assert.Equal("SmallChi", item0.AreaName);
        }

        [Fact]
        public void Test_2019_3()
        {
            byte[] bytes = "0101000005F200DE075BCD15075BCD15000000C8003CC8029A0008536D616C6C436869".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8600>(bytes, JT808Version.JTT2019);
        }
    }
}
