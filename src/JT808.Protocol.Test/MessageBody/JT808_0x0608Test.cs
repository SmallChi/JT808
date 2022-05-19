using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0608Test
    {
        readonly JT808Serializer JT808Serializer = new JT808Serializer();

        [Fact]
        public void Test1()
        {
            JT808_0x0608 value = new JT808_0x0608
            {
                 QueryType=5,
                 Ids=new List<uint>() { 1,2,3}
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("0500000003000000010000000200000003", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0500000003000000010000000200000003".ToHexBytes();
            JT808_0x0608 value = JT808Serializer.Deserialize<JT808_0x0608>(bytes);
            Assert.Equal(5,  value.QueryType);
            Assert.Equal(3u, value.Count);
            Assert.Equal(1u, value.Ids[0]);
            Assert.Equal(2u, value.Ids[1]);
            Assert.Equal(3u, value.Ids[2]);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "0500000003000000010000000200000003".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0608>(bytes);
        }

        [Fact]
        public void Test_JT808_0X8600s_1()
        {
            JT808_0x0608 value = new JT808_0x0608
            {
                QueryType = 1,
                JT808_0x8600s=new List<JT808_0x8600>()
                {
                    new JT808_0x8600
                    {
                         AreaItems =new List<JT808CircleAreaProperty>
                         {
                             new JT808CircleAreaProperty
                             {
                                AreaId = 1522,
                                AreaProperty = 222,
                                CenterPointLat = 123456789,
                                CenterPointLng = 123456789,
                                Radius = 200,
                                StartTime = DateTime.Parse("2020-01-07 00:00:12"),
                                EndTime = DateTime.Parse("2020-01-07 00:00:13"),
                                HighestSpeed = 60,
                                OverspeedDuration = 200,
                                NightMaximumSpeed=666,
                                AreaName="SmallChi"
                             }
                         },
                         SettingAreaProperty= JT808SettingProperty.append_region.ToByteValue()
                    }
                }
            };
            var hex = JT808Serializer.Serialize(value, JT808Version.JTT2019).ToHexString();
            Assert.Equal("01000000010101000005F200DE075BCD15075BCD15000000C8003CC8029A0008536D616C6C436869", hex);
        }

        [Fact]
        public void Test_JT808_0X8600s_2()
        {
            byte[] bytes = "01000000010101000005F200DE075BCD15075BCD15000000C8003CC8029A0008536D616C6C436869".ToHexBytes();
            JT808_0x0608 value = JT808Serializer.Deserialize<JT808_0x0608>(bytes, JT808Version.JTT2019);
            Assert.Equal(1, value.QueryType);
            Assert.Equal(1u, value.Count);
            var jT808_0X8600 = value.JT808_0x8600s[0];
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
        public void Test_JT808_0X8600s_3()
        {
            byte[] bytes = "01000000010101000005F200DE075BCD15075BCD15000000C8003CC8029A0008536D616C6C436869".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0608>(bytes, JT808Version.JTT2019);
        }

        [Fact]
        public void Test_JT808_0X8602s_1()
        {
            JT808_0x0608 value = new JT808_0x0608
            {
                QueryType = 2,
                JT808_0x8602s = new List<JT808_0x8602>()
                {
                    new JT808_0x8602
                    {
                         AreaItems =new List<JT808RectangleAreaProperty>
                         {
                             new JT808RectangleAreaProperty
                            {
                                AreaId = 1522,
                                AreaProperty = 222,
                                LowRightPointLat= 123456789,
                                LowRightPointLng= 123456788,
                                UpLeftPointLat= 123456787,
                                UpLeftPointLng= 123456786,
                                StartTime = DateTime.Parse("2020-01-07 00:00:12"),
                                EndTime = DateTime.Parse("2020-01-07 00:00:13"),
                                HighestSpeed = 60,
                                OverspeedDuration = 200,
                                AreaName="smallchi"
                            }
                         },
                         SettingAreaProperty= JT808SettingProperty.append_region.ToByteValue()
                    }
                }
            };
            var hex = JT808Serializer.Serialize(value, JT808Version.JTT2019).ToHexString();
            Assert.Equal("02000000010101000005F200DE075BCD13075BCD12075BCD15075BCD14003CC800000008736D616C6C636869", hex);
        }

        [Fact]
        public void Test_JT808_0X8602s_2()
        {
            byte[] bytes = "02000000010101000005F200DE075BCD13075BCD12075BCD15075BCD14003CC800000008736D616C6C636869".ToHexBytes();
            JT808_0x0608 value = JT808Serializer.Deserialize<JT808_0x0608>(bytes, JT808Version.JTT2019);
            Assert.Equal(2, value.QueryType);
            Assert.Equal(1u, value.Count);
            var jT808_0X8602 = value.JT808_0x8602s[0];
            Assert.Equal(JT808SettingProperty.append_region.ToByteValue(), jT808_0X8602.SettingAreaProperty);
            Assert.Equal(1, jT808_0X8602.AreaCount);
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
            Assert.Equal(0, item0.NightMaximumSpeed);
            Assert.Equal("smallchi", item0.AreaName);
        }

        [Fact]
        public void Test_JT808_0X8602s_3()
        {
            byte[] bytes = "02000000010101000005F200DE075BCD13075BCD12075BCD15075BCD14003CC800000008736D616C6C636869".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0608>(bytes, JT808Version.JTT2019);
        }

        [Fact]
        public void Test_JT808_0x8604s_1()
        {
            JT808_0x0608 value = new JT808_0x0608
            {
                QueryType = 3,
                JT808_0x8604s = new List<JT808_0x8604>()
                {
                    new JT808_0x8604
                    {   AreaId = 1234,
                        AreaProperty = JT808SettingProperty.append_region.ToByteValue(),
                        StartTime = DateTime.Parse("2020-01-07 00:00:12"),
                        EndTime = DateTime.Parse("2020-01-07 00:00:13"),
                        HighestSpeed = 62,
                        OverspeedDuration = 218,
                        PeakItems = new List<JT808PeakProperty>()
                        {
                            new JT808PeakProperty
                            {
                                    Lat= 123456789,
                                    Lng= 123456788
                            },
                            new JT808PeakProperty
                            {
                                Lat = 123456700,
                                Lng = 123456701
                            }
                        },
                        AreaName="smallchi",
                        NightMaximumSpeed=66
                    }
                }
            };
            var hex = JT808Serializer.Serialize(value, JT808Version.JTT2019).ToHexString();
            Assert.Equal("0300000001000004D200012001070000122001070000130002075BCD15075BCD14075BCCBC075BCCBD0008736D616C6C636869", hex);
        }

        [Fact]
        public void Test_JT808_0X8604s_2()
        {
            byte[] bytes = "0300000001000004D200012001070000122001070000130002075BCD15075BCD14075BCCBC075BCCBD0008736D616C6C636869".ToHexBytes();
            JT808_0x0608 value = JT808Serializer.Deserialize<JT808_0x0608>(bytes, JT808Version.JTT2019);
            Assert.Equal(3, value.QueryType);
            Assert.Equal(1u, value.Count);
            var jT808_0X8604 = value.JT808_0x8604s[0];
            Assert.Equal((uint)1234, jT808_0X8604.AreaId);
            Assert.Equal(JT808SettingProperty.append_region.ToByteValue(), jT808_0X8604.AreaProperty);
            Assert.Null(jT808_0X8604.HighestSpeed);
            Assert.Null(jT808_0X8604.OverspeedDuration);
            Assert.Equal(DateTime.Parse("2020-01-07 00:00:12"), jT808_0X8604.StartTime);
            Assert.Equal(DateTime.Parse("2020-01-07 00:00:13"), jT808_0X8604.EndTime);
            Assert.Equal(2, jT808_0X8604.PeakItems.Count);
            Assert.Equal((uint)123456789, jT808_0X8604.PeakItems[0].Lat);
            Assert.Equal((uint)123456788, jT808_0X8604.PeakItems[0].Lng);
            Assert.Equal((uint)123456700, jT808_0X8604.PeakItems[1].Lat);
            Assert.Equal((uint)123456701, jT808_0X8604.PeakItems[1].Lng);
            Assert.Equal(0, jT808_0X8604.NightMaximumSpeed);
            Assert.Equal("smallchi", jT808_0X8604.AreaName);
         }

        [Fact]
        public void Test_JT808_0X8604s_3()
        {
            byte[] bytes = "0300000001000004D200012001070000122001070000130002075BCD15075BCD14075BCCBC075BCCBD0008736D616C6C636869".ToHexBytes();
            string value = JT808Serializer.Analyze<JT808_0x0608>(bytes, JT808Version.JTT2019);
        }

        [Fact]
        public void Test_JT808_0x8606s_1()
        {
            JT808_0x0608 value = new JT808_0x0608
            {
                QueryType = 4,
                JT808_0x8606s = new List<JT808_0x8606>()
                {
                    new JT808_0x8606
                    {
                        RouteId = 9999,
                        RouteProperty = 51,
                        StartTime = DateTime.Parse("2020-01-07 00:00:12"),
                        EndTime = DateTime.Parse("2020-01-07 00:00:12"),
                        InflectionPointItems = new List<JT808InflectionPointProperty>()
                        {
                            new JT808InflectionPointProperty()
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
                                NightMaximumSpeed=80
                            }
                        },
                        RouteName = "koike518"
                    }
                }
            };
            var hex = JT808Serializer.Serialize(value, JT808Version.JTT2019).ToHexString();
            Assert.Equal("04000000010000270F00332001070000122001070000120001000003E800000507075BCD15075BCD1438030032007B004517005000086B6F696B65353138", hex);
        }

        [Fact]
        public void Test_JT808_0X8606s_2()
        {
            byte[] bytes = "04000000010000270F00332001070000122001070000120001000003E800000507075BCD15075BCD1438030032007B004517005000086B6F696B65353138".ToHexBytes();
            JT808_0x0608 value = JT808Serializer.Deserialize<JT808_0x0608>(bytes, JT808Version.JTT2019);
            Assert.Equal(4, value.QueryType);
            Assert.Equal(1u, value.Count);
            var jT808_0X8606 = value.JT808_0x8606s[0];
            Assert.Equal((uint)9999, jT808_0X8606.RouteId);
            Assert.Equal((uint)51, jT808_0X8606.RouteProperty);
            Assert.Equal(DateTime.Parse("2020-01-07 00:00:12"), jT808_0X8606.StartTime);
            Assert.Equal(DateTime.Parse("2020-01-07 00:00:12"), jT808_0X8606.EndTime);

            Assert.Single(jT808_0X8606.InflectionPointItems);

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

            Assert.Equal("koike518", jT808_0X8606.RouteName);
        }

        [Fact]
        public void Test_JT808_0X8606s_3()
        {
            byte[] bytes = "04000000010000270F00332001070000122001070000120001000003E800000507075BCD15075BCD1438030032007B004517005000086B6F696B65353138".ToHexBytes();
            string value = JT808Serializer.Analyze<JT808_0x0608>(bytes, JT808Version.JTT2019);
        }
    }
}
