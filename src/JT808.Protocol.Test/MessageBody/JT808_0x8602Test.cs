using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8602Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8602 jT808_0X8602 = new JT808_0x8602
            {
                SettingAreaProperty = JT808SettingProperty.append_region.ToByteValue(),
                AreaItems = new List<JT808RectangleAreaProperty>()
            };
            jT808_0X8602.AreaItems.Add(new JT808RectangleAreaProperty
            {
                AreaId = 1522,
                AreaProperty = 222,
                LowRightPointLat= 123456789,
                LowRightPointLng= 123456788,
                UpLeftPointLat= 123456787,
                UpLeftPointLng= 123456786,
                StartTime = DateTime.Parse("2018-11-20 00:00:12"),
                EndTime = DateTime.Parse("2018-11-21 00:00:12"),
                HighestSpeed = 60,
                OverspeedDuration = 200
            });
            jT808_0X8602.AreaItems.Add(new JT808RectangleAreaProperty
            {
                AreaId = 1523,
                AreaProperty = 10,
                LowRightPointLat = 123456700,
                LowRightPointLng = 123456701,
                UpLeftPointLat = 123456702,
                UpLeftPointLng = 123456703,
                StartTime = DateTime.Parse("2018-11-20 12:12:12"),
                EndTime = DateTime.Parse("2018-11-21 11:11:11"),
                HighestSpeed = 60,
                OverspeedDuration = 200
            });
            var hex = JT808Serializer.Serialize(jT808_0X8602).ToHexString();
            Assert.Equal("0102000005F200DE075BCD13075BCD12075BCD15075BCD14003CC8000005F3000A075BCCBE075BCCBF075BCCBC075BCCBD003CC8", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0102000005F200DE075BCD13075BCD12075BCD15075BCD14003CC8000005F3000A075BCCBE075BCCBF075BCCBC075BCCBD003CC8".ToHexBytes();
            JT808_0x8602 jT808_0X8602 = JT808Serializer.Deserialize<JT808_0x8602>(bytes);

            Assert.Equal(JT808SettingProperty.append_region.ToByteValue(), jT808_0X8602.SettingAreaProperty);
            Assert.Equal(2, jT808_0X8602.AreaCount);

            var item0 = jT808_0X8602.AreaItems[0];
            Assert.Equal((uint)1522, item0.AreaId);
            Assert.Equal((ushort)222, item0.AreaProperty);

            Assert.Equal((uint)123456789, item0.LowRightPointLat);
            Assert.Equal((uint)123456788, item0.LowRightPointLng);
            Assert.Equal((uint)123456787, item0.UpLeftPointLat);
            Assert.Equal((uint)123456786, item0.UpLeftPointLng);

            Assert.Null(item0.StartTime);
            Assert.Null(item0.EndTime);
            Assert.Equal((ushort)60, item0.HighestSpeed);
            Assert.Equal((byte)200, item0.OverspeedDuration);

            var item1 = jT808_0X8602.AreaItems[1];
            Assert.Equal((uint)1523, item1.AreaId);
            Assert.Equal(10, item1.AreaProperty);

            Assert.Equal((uint)123456700, item1.LowRightPointLat);
            Assert.Equal((uint)123456701, item1.LowRightPointLng);
            Assert.Equal((uint)123456702, item1.UpLeftPointLat);
            Assert.Equal((uint)123456703, item1.UpLeftPointLng);

            Assert.Null(item1.StartTime);
            Assert.Null(item1.EndTime);
            Assert.Equal((ushort)60, item1.HighestSpeed);
            Assert.Equal((byte)200, item1.OverspeedDuration);
        }
        [Fact]
        public void Test3()
        {
            byte[] bytes = "0102000005F200DE075BCD13075BCD12075BCD15075BCD14003CC8000005F3000A075BCCBE075BCCBF075BCCBC075BCCBD003CC8".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8602>(bytes);
        }
    }
}
