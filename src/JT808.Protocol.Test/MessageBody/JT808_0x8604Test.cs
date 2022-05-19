using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8604Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8604 jT808_0X8604 = new JT808_0x8604
            {
                AreaId = 1234,
                AreaProperty = JT808SettingProperty.append_region.ToByteValue(),
                StartTime = DateTime.Parse("2018-11-20 00:00:12"),
                EndTime = DateTime.Parse("2018-11-21 00:00:12"),
                HighestSpeed = 62,
                OverspeedDuration = 218,
                PeakItems = new List<JT808PeakProperty>()
            };
            jT808_0X8604.PeakItems.Add(new JT808PeakProperty
            {
                 Lat= 123456789,
                 Lng= 123456788
            });
            jT808_0X8604.PeakItems.Add(new JT808PeakProperty
            {
                Lat = 123456700,
                Lng = 123456701
            });
            var hex = JT808Serializer.Serialize(jT808_0X8604).ToHexString();
            Assert.Equal("000004D200011811200000121811210000120002075BCD15075BCD14075BCCBC075BCCBD", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "000004D200011811200000121811210000120002075BCD15075BCD14075BCCBC075BCCBD".ToHexBytes();
            JT808_0x8604 jT808_0X8604 = JT808Serializer.Deserialize<JT808_0x8604>(bytes);
            Assert.Equal((uint)1234, jT808_0X8604.AreaId);
            Assert.Equal(JT808SettingProperty.append_region.ToByteValue(), jT808_0X8604.AreaProperty);
            Assert.Null(jT808_0X8604.HighestSpeed);
            Assert.Null(jT808_0X8604.OverspeedDuration);

            Assert.Equal(DateTime.Parse("2018-11-20 00:00:12"),jT808_0X8604.StartTime);
            Assert.Equal(DateTime.Parse("2018-11-21 00:00:12"),jT808_0X8604.EndTime);
            Assert.Equal(2, jT808_0X8604.PeakItems.Count);
            Assert.Equal((uint)123456789, jT808_0X8604.PeakItems[0].Lat);
            Assert.Equal((uint)123456788, jT808_0X8604.PeakItems[0].Lng);
            Assert.Equal((uint)123456700, jT808_0X8604.PeakItems[1].Lat);
            Assert.Equal((uint)123456701, jT808_0X8604.PeakItems[1].Lng);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "000004D200011811200000121811210000120002075BCD15075BCD14075BCCBC075BCCBD".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8604>(bytes);
        }
    }
}
