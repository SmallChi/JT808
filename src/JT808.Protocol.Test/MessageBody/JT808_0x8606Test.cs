using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8606Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8606 jT808_0X8606 = new JT808_0x8606
            {
                RouteId = 9999,
                RouteProperty = 1268,
                StartTime = DateTime.Parse("2018-11-20 00:00:12"),
                EndTime = DateTime.Parse("2018-11-21 00:00:12"),
                InflectionPointItems = new List<JT808InflectionPointProperty>()
            };
            jT808_0X8606.InflectionPointItems.Add(new JT808InflectionPointProperty()
            {
                InflectionPointId = 1000,
                InflectionPointLat = 123456789,
                InflectionPointLng = 123456788,
                SectionDrivingUnderThreshold = 123,
                SectionHighestSpeed = 69,
                SectionId = 1287,
                SectionLongDrivingThreshold = 50,
                SectionOverspeedDuration = 23,
                SectionProperty = 89,
                SectionWidth = 56
            });
            jT808_0X8606.InflectionPointItems.Add(new JT808InflectionPointProperty()
            {
                InflectionPointId = 1001,
                InflectionPointLat = 123456780,
                InflectionPointLng = 123456781,
                SectionDrivingUnderThreshold = 124,
                SectionHighestSpeed = 42,
                SectionId = 12007,
                SectionLongDrivingThreshold = 58,
                SectionOverspeedDuration = 26,
                SectionProperty = 50,
                SectionWidth = 75
            });
            var hex = JT808Serializer.Serialize(jT808_0X8606).ToHexString();
            //00 00 27 0F 
            //04 F4
            //00 02
            //00 00 03 E8
            //00 00 05 07
            //07 5B CD 15
            //07 5B CD 14
            //38
            //59    //‭0101 1001‬
            //00 32 
            //00 7B
            //00 00 03 E9 
            //00 00 2E E7 
            //07 5B CD 0C 
            //07 5B CD 0D
            //4B 
            //32    //‭‭0011 0010‬
            //00 2A
            //1A
            Assert.Equal("0000270F04F40002000003E800000507075BCD15075BCD1438590032007B000003E900002EE7075BCD0C075BCD0D4B32002A1A", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0000270F04F40002000003E800000507075BCD15075BCD1438590032007B000003E900002EE7075BCD0C075BCD0D4B32002A1A".ToHexBytes();
            JT808_0x8606 jT808_0X8606 = JT808Serializer.Deserialize<JT808_0x8606>(bytes);

            Assert.Equal((uint)9999, jT808_0X8606.RouteId);
            Assert.Equal((uint)1268, jT808_0X8606.RouteProperty);
            //Assert.Equal(DateTime.Parse("2018-11-20 00:00:12"), jT808_0X8606.StartTime);
            //Assert.Equal(DateTime.Parse("2018-11-21 00:00:12"), jT808_0X8606.EndTime);
            Assert.Null(jT808_0X8606.StartTime);
            Assert.Null(jT808_0X8606.EndTime);

            Assert.Equal(2, jT808_0X8606.InflectionPointItems.Count);

            Assert.Equal((uint)1000, jT808_0X8606.InflectionPointItems[0].InflectionPointId);
            Assert.Equal((uint)123456789, jT808_0X8606.InflectionPointItems[0].InflectionPointLat);
            Assert.Equal((uint)123456788, jT808_0X8606.InflectionPointItems[0].InflectionPointLng);

            Assert.Equal((ushort)123, jT808_0X8606.InflectionPointItems[0].SectionDrivingUnderThreshold);
            //Assert.Equal((ushort)69, jT808_0X8606.InflectionPointItems[0].SectionHighestSpeed);
            Assert.Null(jT808_0X8606.InflectionPointItems[0].SectionHighestSpeed);
            Assert.Equal((uint)1287, jT808_0X8606.InflectionPointItems[0].SectionId);
            Assert.Equal((ushort)50, jT808_0X8606.InflectionPointItems[0].SectionLongDrivingThreshold);
            //Assert.Equal((byte)23, jT808_0X8606.InflectionPointItems[0].SectionOverspeedDuration);
            Assert.Equal(89, jT808_0X8606.InflectionPointItems[0].SectionProperty);
            Assert.Equal(56, jT808_0X8606.InflectionPointItems[0].SectionWidth);

            Assert.Equal((uint)1001, jT808_0X8606.InflectionPointItems[1].InflectionPointId);
            Assert.Equal((uint)123456780, jT808_0X8606.InflectionPointItems[1].InflectionPointLat);
            Assert.Equal((uint)123456781, jT808_0X8606.InflectionPointItems[1].InflectionPointLng);
            //Assert.Equal((ushort)124, jT808_0X8606.InflectionPointItems[1].SectionDrivingUnderThreshold);
            Assert.Null(jT808_0X8606.InflectionPointItems[1].SectionDrivingUnderThreshold);
            Assert.Equal((ushort)42, jT808_0X8606.InflectionPointItems[1].SectionHighestSpeed);
            Assert.Equal((uint)12007, jT808_0X8606.InflectionPointItems[1].SectionId);
            //Assert.Equal((ushort)58, jT808_0X8606.InflectionPointItems[1].SectionLongDrivingThreshold);
            Assert.Null(jT808_0X8606.InflectionPointItems[1].SectionLongDrivingThreshold);

            Assert.Equal((byte)26, jT808_0X8606.InflectionPointItems[1].SectionOverspeedDuration);
            Assert.Equal(50, jT808_0X8606.InflectionPointItems[1].SectionProperty);
            Assert.Equal(75, jT808_0X8606.InflectionPointItems[1].SectionWidth);
        }

        [Fact]
        public void Test3()
        {
            JT808_0x8606 jT808_0X8606 = new JT808_0x8606
            {
                RouteId = 9999,
                RouteProperty = 51,
                StartTime = DateTime.Parse("2018-11-20 00:00:12"),
                EndTime = DateTime.Parse("2018-11-21 00:00:12"),
                InflectionPointItems = new List<JT808InflectionPointProperty>()
            };
            jT808_0X8606.InflectionPointItems.Add(new JT808InflectionPointProperty()
            {
                InflectionPointId = 1000,
                InflectionPointLat = 123456789,
                InflectionPointLng = 123456788,
                SectionDrivingUnderThreshold = 123,
                SectionHighestSpeed = 69,
                SectionId = 1287,
                SectionLongDrivingThreshold = 50,
                SectionOverspeedDuration = 23,
                SectionProperty = 3,
                SectionWidth = 56
            });
            jT808_0X8606.InflectionPointItems.Add(new JT808InflectionPointProperty()
            {
                InflectionPointId = 1001,
                InflectionPointLat = 123456780,
                InflectionPointLng = 123456781,
                SectionDrivingUnderThreshold = 124,
                SectionHighestSpeed = 42,
                SectionId = 12007,
                SectionLongDrivingThreshold = 58,
                SectionOverspeedDuration = 26,
                SectionProperty = 3,
                SectionWidth = 75
            });
            var hex = JT808Serializer.Serialize(jT808_0X8606).ToHexString();
            //00 00 27 0F 
            //04 F4
            //00 02
            //00 00 03 E8
            //00 00 05 07
            //07 5B CD 15
            //07 5B CD 14
            //38
            //59    //‭0000000000110011
            //00 32 
            //00 7B
            //00 00 03 E9 
            //00 00 2E E7 
            //07 5B CD 0C 
            //07 5B CD 0D
            //4B 
            //32    //‭‭0000000000000011
            //00 2A
            //1A
            Assert.Equal("0000270F00331811200000121811210000120002000003E800000507075BCD15075BCD1438030032007B004517000003E900002EE7075BCD0C075BCD0D4B03003A007C002A1A", hex);
        }

        [Fact]
        public void Test4()
        {
            byte[] bytes = "0000270F00331811200000121811210000120002000003E800000507075BCD15075BCD1438030032007B004517000003E900002EE7075BCD0C075BCD0D4B03003A007C002A1A".ToHexBytes();
            JT808_0x8606 jT808_0X8606 = JT808Serializer.Deserialize<JT808_0x8606>(bytes);

            Assert.Equal((uint)9999, jT808_0X8606.RouteId);
            Assert.Equal((uint)51, jT808_0X8606.RouteProperty);
            Assert.Equal(DateTime.Parse("2018-11-20 00:00:12"), jT808_0X8606.StartTime);
            Assert.Equal(DateTime.Parse("2018-11-21 00:00:12"), jT808_0X8606.EndTime);

            Assert.Equal(2, jT808_0X8606.InflectionPointItems.Count);

            Assert.Equal((uint)1000, jT808_0X8606.InflectionPointItems[0].InflectionPointId);
            Assert.Equal((uint)123456789, jT808_0X8606.InflectionPointItems[0].InflectionPointLat);
            Assert.Equal((uint)123456788, jT808_0X8606.InflectionPointItems[0].InflectionPointLng);

            Assert.Equal((ushort)123, jT808_0X8606.InflectionPointItems[0].SectionDrivingUnderThreshold);
            Assert.Equal((ushort)69, jT808_0X8606.InflectionPointItems[0].SectionHighestSpeed);
            Assert.Equal((uint)1287, jT808_0X8606.InflectionPointItems[0].SectionId);
            Assert.Equal((ushort)50, jT808_0X8606.InflectionPointItems[0].SectionLongDrivingThreshold);
            Assert.Equal((byte)23, jT808_0X8606.InflectionPointItems[0].SectionOverspeedDuration);
            Assert.Equal(3, jT808_0X8606.InflectionPointItems[0].SectionProperty);
            Assert.Equal(56, jT808_0X8606.InflectionPointItems[0].SectionWidth);

            Assert.Equal((uint)1001, jT808_0X8606.InflectionPointItems[1].InflectionPointId);
            Assert.Equal((uint)123456780, jT808_0X8606.InflectionPointItems[1].InflectionPointLat);
            Assert.Equal((uint)123456781, jT808_0X8606.InflectionPointItems[1].InflectionPointLng);
            Assert.Equal((ushort)124, jT808_0X8606.InflectionPointItems[1].SectionDrivingUnderThreshold);
            Assert.Equal((ushort)42, jT808_0X8606.InflectionPointItems[1].SectionHighestSpeed);
            Assert.Equal((uint)12007, jT808_0X8606.InflectionPointItems[1].SectionId);
            Assert.Equal((ushort)58, jT808_0X8606.InflectionPointItems[1].SectionLongDrivingThreshold);
            Assert.Equal((byte)26, jT808_0X8606.InflectionPointItems[1].SectionOverspeedDuration);
            Assert.Equal(3, jT808_0X8606.InflectionPointItems[1].SectionProperty);
            Assert.Equal(75, jT808_0X8606.InflectionPointItems[1].SectionWidth);
        }

        [Fact]
        public void Test5()
        {
            byte[] bytes = "0000270F00331811200000121811210000120002000003E800000507075BCD15075BCD1438030032007B004517000003E900002EE7075BCD0C075BCD0D4B03003A007C002A1A".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8606>(bytes);
        }

        [Fact]
        public void Test5_2019()
        {
            JT808_0x8606 jT808_0X8606 = new JT808_0x8606
            {
                RouteId = 9999,
                RouteProperty = 51,
                StartTime = DateTime.Parse("2020-01-04 00:00:12"),
                EndTime = DateTime.Parse("2020-01-04 00:00:12"),
                InflectionPointItems = new List<JT808InflectionPointProperty>()
            };
            jT808_0X8606.InflectionPointItems.Add(new JT808InflectionPointProperty()
            {
                InflectionPointId = 1000,
                InflectionPointLat = 123456789,
                InflectionPointLng = 123456788,
                SectionDrivingUnderThreshold = 123,
                SectionHighestSpeed = 69,
                SectionId = 1287,
                SectionLongDrivingThreshold = 50,
                SectionOverspeedDuration = 23,
                SectionProperty = 3,
                SectionWidth = 56,
                NightMaximumSpeed = 80
            });
            jT808_0X8606.InflectionPointItems.Add(new JT808InflectionPointProperty()
            {
                InflectionPointId = 1001,
                InflectionPointLat = 123456780,
                InflectionPointLng = 123456781,
                SectionDrivingUnderThreshold = 124,
                SectionHighestSpeed = 42,
                SectionId = 12007,
                SectionLongDrivingThreshold = 58,
                SectionOverspeedDuration = 26,
                SectionProperty = 3,
                SectionWidth = 75,
                NightMaximumSpeed = 66
            });
            jT808_0X8606.RouteName = "koike518";
            var hex = JT808Serializer.Serialize(jT808_0X8606, JT808Version.JTT2019).ToHexString();
            Assert.Equal("0000270F00332001040000122001040000120002000003E800000507075BCD15075BCD1438030032007B0045170050000003E900002EE7075BCD0C075BCD0D4B03003A007C002A1A004200086B6F696B65353138", hex);
        }

        [Fact]
        public void Test6_2019()
        {
            byte[] bytes = "0000270F00332001040000122001040000120002000003E800000507075BCD15075BCD1438030032007B0045170050000003E900002EE7075BCD0C075BCD0D4B03003A007C002A1A004200086B6F696B65353138".ToHexBytes();
            JT808_0x8606 jT808_0X8606 = JT808Serializer.Deserialize<JT808_0x8606>(bytes, JT808Version.JTT2019);

            Assert.Equal((uint)9999, jT808_0X8606.RouteId);
            Assert.Equal((uint)51, jT808_0X8606.RouteProperty);
            Assert.Equal(DateTime.Parse("2020-01-04 00:00:12"), jT808_0X8606.StartTime);
            Assert.Equal(DateTime.Parse("2020-01-04 00:00:12"), jT808_0X8606.EndTime);

            Assert.Equal(2, jT808_0X8606.InflectionPointItems.Count);

            Assert.Equal((uint)1000, jT808_0X8606.InflectionPointItems[0].InflectionPointId);
            Assert.Equal((uint)123456789, jT808_0X8606.InflectionPointItems[0].InflectionPointLat);
            Assert.Equal((uint)123456788, jT808_0X8606.InflectionPointItems[0].InflectionPointLng);

            Assert.Equal((ushort)123, jT808_0X8606.InflectionPointItems[0].SectionDrivingUnderThreshold);
            Assert.Equal((ushort)69, jT808_0X8606.InflectionPointItems[0].SectionHighestSpeed);
            Assert.Equal((uint)1287, jT808_0X8606.InflectionPointItems[0].SectionId);
            Assert.Equal((ushort)50, jT808_0X8606.InflectionPointItems[0].SectionLongDrivingThreshold);
            Assert.Equal((byte)23, jT808_0X8606.InflectionPointItems[0].SectionOverspeedDuration);
            Assert.Equal(3, jT808_0X8606.InflectionPointItems[0].SectionProperty);
            Assert.Equal(56, jT808_0X8606.InflectionPointItems[0].SectionWidth);
            Assert.Equal(80, jT808_0X8606.InflectionPointItems[0].NightMaximumSpeed.Value);

            Assert.Equal((uint)1001, jT808_0X8606.InflectionPointItems[1].InflectionPointId);
            Assert.Equal((uint)123456780, jT808_0X8606.InflectionPointItems[1].InflectionPointLat);
            Assert.Equal((uint)123456781, jT808_0X8606.InflectionPointItems[1].InflectionPointLng);
            Assert.Equal((ushort)124, jT808_0X8606.InflectionPointItems[1].SectionDrivingUnderThreshold);
            Assert.Equal((ushort)42, jT808_0X8606.InflectionPointItems[1].SectionHighestSpeed);
            Assert.Equal((uint)12007, jT808_0X8606.InflectionPointItems[1].SectionId);
            Assert.Equal((ushort)58, jT808_0X8606.InflectionPointItems[1].SectionLongDrivingThreshold);
            Assert.Equal((byte)26, jT808_0X8606.InflectionPointItems[1].SectionOverspeedDuration);
            Assert.Equal(3, jT808_0X8606.InflectionPointItems[1].SectionProperty);
            Assert.Equal(75, jT808_0X8606.InflectionPointItems[1].SectionWidth);
            Assert.Equal(66, jT808_0X8606.InflectionPointItems[1].NightMaximumSpeed.Value);

            Assert.Equal("koike518", jT808_0X8606.RouteName);
        }

        [Fact]
        public void Test6_2019_1()
        {
            byte[] bytes = "0000270F00332001040000122001040000120002000003E800000507075BCD15075BCD1438030032007B0045170050000003E900002EE7075BCD0C075BCD0D4B03003A007C002A1A004200086B6F696B65353138".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8606>(bytes, JT808Version.JTT2019);
        }
    }
}
