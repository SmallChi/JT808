using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.CarDVR;
using JT808.Protocol.Test.MessageBody.JT808_0x0701BodiesImpl;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using static JT808.Protocol.MessageBody.CarDVR.JT808_CarDVR_Up_0x08;
using static JT808.Protocol.MessageBody.CarDVR.JT808_CarDVR_Up_0x09;
using static JT808.Protocol.MessageBody.CarDVR.JT808_CarDVR_Up_0x10;
using static JT808.Protocol.MessageBody.CarDVR.JT808_CarDVR_Up_0x11;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0700Test
    {
        JT808Serializer JT808Serializer;

        public JT808_0x0700Test()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Test_Analyze()
        {
            //0x00
            byte[] bytes = "000100557A00000200190135".ToHexBytes();
            var value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x01
            bytes = "000101557A0100120034333032323331393930303731323636383732".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x02
            bytes = "000102557A020006002003231010103B".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x03
            bytes = "000103557A03001400200322101010200323101010000010000000500079".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x04
            bytes = "000104557A04000800200323101010003201".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x05
            bytes = "000105557A050029003132333435363738393132333435363738D4C142313233343500000000D6D8D0CDBBF5B3B50000000007".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x06
            bytes = "000106557A06005700200323101010FFD7D4B6A8D2E531000000D7D4B6A8D2E532000000D7D4B6A8D2E533000000BDFCB9E2B5C600000000D4B6B9E2B5C600000000D3D2D7AACFF200000000D7F3D7AACFF200000000D6C6B6AF00000000000084".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x07
            bytes = "000107557A0700230031323334353637313233343536373839313233343536372003233132333431323334003A".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x08
            bytes = "000108557A08007E002003230000000A14FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF47".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x09
            bytes = "000109557A09029A0020032300000006C6431601691B8800320AFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF17".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x10
            bytes = "000110557A1000EA002003230000003433303232333139393030393230333639380A14FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF06C6431601691B8800329D".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x11
            bytes = "000111557A1100320034333032323331393930303932303336393820032200000020032300000006C6431601691B88003206C6431701691B89003C09".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x12
            bytes = "000112557A12001900200323000000343330323233313939303039323033363938012F".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x13
            bytes = "000113557A13000700200323000000013A".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x14
            bytes = "000114557A14000700200323000000013D".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
            //0x15
            bytes = "000115557A15008500012003220000002003230000003228FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA5".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x0700>(bytes);
        }
        [Fact]
        public void Test_Serialize_0x00()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x00;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x00
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x00
            {
                ModifyNumber = 1,
                StandardYear = "19"
            };

            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000100557A00000200190135", hex);
        }

        [Fact]
        public void Test_Deserilize_0x00()
        {
            byte[] bytes = "000100557A00000200190135".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var package = value.JT808CarDVRUpPackage as JT808CarDVRUpPackage;
            Assert.Equal(0x557a, package.Begin);
            Assert.Equal(0, package.KeepFields);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x00;
            Assert.Equal(1, body.ModifyNumber);
            Assert.Equal("19", body.StandardYear);
        }

        [Fact]
        public void Test_Serialize_0x01()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x01;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x01
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x01
            {
                DriverLicenseNo = "430223199007126687"
            };

            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000101557A0100120034333032323331393930303731323636383732", hex);
        }

        [Fact]
        public void Test_Deserilize_0x01()
        {
            byte[] bytes = "000101557A0100120034333032323331393930303731323636383732".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x01;
            Assert.Equal("430223199007126687", body.DriverLicenseNo);
        }

        [Fact]
        public void Test_Serialize_0x02()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x02;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x02
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x02
            {
                RealTime = Convert.ToDateTime("2020-03-23 10:10:10")
            };

            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000102557A020006002003231010103B", hex);
        }

        [Fact]
        public void Test_Deserilize_0x02()
        {
            byte[] bytes = "000102557A020006002003231010103B".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x02;
            Assert.Equal("2020-03-23 10:10:10", body.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        [Fact]
        public void Test_Serialize_0x03()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x03;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x03
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x03
            {
                FirstInstallTime = Convert.ToDateTime("2020-03-23 10:10:10"),
                RealTime = Convert.ToDateTime("2020-03-22 10:10:10"),
                FirstMileage = "1000",
                TotalMilage = "5000"
            };

            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000103557A03001400200322101010200323101010000010000000500079", hex);
        }

        [Fact]
        public void Test_Deserilize_0x03()
        {
            byte[] bytes = "000103557A03001400200322101010200323101010000010000000500079".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x03;
            Assert.Equal("2020-03-22 10:10:10", body.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal("2020-03-23 10:10:10", body.FirstInstallTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal("1000", body.FirstMileage);
            Assert.Equal("5000", body.TotalMilage);
        }
        [Fact]
        public void Test_Serialize_0x04()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x04;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x04
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x04
            {
                RealTime = Convert.ToDateTime("2020-03-23 10:10:10"),
                PulseCoefficient = 50
            };

            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000104557A04000800200323101010003201", hex);
        }

        [Fact]
        public void Test_Deserilize_0x04()
        {
            byte[] bytes = "000104557A04000800200323101010003201".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x04;
            Assert.Equal("2020-03-23 10:10:10", body.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(50, body.PulseCoefficient);
        }
        [Fact]
        public void Test_Serialize_0x05()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x05;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x05
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x05
            {
                VehicleNo = "粤B12345",
                VehicleType = "重型货车",
                Vin = "12345678912345678"
            };

            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000105557A050029003132333435363738393132333435363738D4C142313233343500000000D6D8D0CDBBF5B3B50000000007", hex);
        }

        [Fact]
        public void Test_Deserilize_0x05()
        {
            byte[] bytes = "000105557A050029003132333435363738393132333435363738D4C142313233343500000000D6D8D0CDBBF5B3B50000000007".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x05;
            Assert.Equal("粤B12345", body.VehicleNo);
            Assert.Equal("重型货车", body.VehicleType);
            Assert.Equal("12345678912345678", body.Vin);
        }
        [Fact]
        public void Test_Serialize_0x06()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x06;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x06
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x06
            {
                FarLight = "远光灯",
                Brake = "制动",
                D0 = "自定义1",
                D1 = "自定义2",
                D2 = "自定义3",
                LeftTurn = "左转向",
                NearLight = "近光灯",
                RealTime = Convert.ToDateTime("2020-03-23 10:10:10"),
                RightTurn = "右转向",
                SignalOperate = 255,
            };

            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000106557A06005700200323101010FFD7D4B6A8D2E531000000D7D4B6A8D2E532000000D7D4B6A8D2E533000000BDFCB9E2B5C600000000D4B6B9E2B5C600000000D3D2D7AACFF200000000D7F3D7AACFF200000000D6C6B6AF00000000000084", hex);
        }

        [Fact]
        public void Test_Deserilize_0x06()
        {
            byte[] bytes = "000106557A06005700200323101010FFD7D4B6A8D2E531000000D7D4B6A8D2E532000000D7D4B6A8D2E533000000BDFCB9E2B5C600000000D4B6B9E2B5C600000000D3D2D7AACFF200000000D7F3D7AACFF200000000D6C6B6AF00000000000084".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x06;
            Assert.Equal("远光灯", body.FarLight);
            Assert.Equal("制动", body.Brake);
            Assert.Equal("自定义1", body.D0);
            Assert.Equal("自定义2", body.D1);
            Assert.Equal("自定义3", body.D2);
            Assert.Equal("左转向", body.LeftTurn);
            Assert.Equal("近光灯", body.NearLight);
            Assert.Equal("2020-03-23 10:10:10", body.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal("右转向", body.RightTurn);
            Assert.Equal(255, body.SignalOperate);
        }
        [Fact]
        public void Test_Serialize_0x07()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x07;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x07
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x07
            {
                CertifiedProductModels = "1234567891234567",
                ProductionDate = Convert.ToDateTime("2020-03-23"),
                ProductionPlantCCCCertificationCode = "1234567",
                ProductProductionFlowNumber = "1234",
                Reversed = "1234"
            };

            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000107557A0700230031323334353637313233343536373839313233343536372003233132333431323334003A", hex);
        }

        [Fact]
        public void Test_Deserilize_0x07()
        {
            byte[] bytes = "000107557A0700230031323334353637313233343536373839313233343536372003233132333431323334003A".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x07;
            Assert.Equal("2020-03-23", body.ProductionDate.ToString("yyyy-MM-dd"));
            Assert.Equal("1234567891234567", body.CertifiedProductModels);
            Assert.Equal("1234567", body.ProductionPlantCCCCertificationCode);
            Assert.Equal("1234", body.ProductProductionFlowNumber);
            Assert.Equal("1234", body.Reversed);
        }
        [Fact]
        public void Test_Serialize_0x08()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x08;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x08
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x08
            {
                JT808_CarDVR_Up_0x08_SpeedPerMinutes = new List<JT808_CarDVR_Up_0x08_SpeedPerMinute> {
                        new JT808_CarDVR_Up_0x08_SpeedPerMinute{
                            StartTime=Convert.ToDateTime("2020-03-23"),
                            JT808_CarDVR_Up_0x08_SpeedPerSeconds=new List<JT808_CarDVR_Up_0x08_SpeedPerSecond>{
                                new JT808_CarDVR_Up_0x08_SpeedPerSecond{
                                            AvgSpeedAfterStartTime=10,
                                            StatusSignalAfterStartTime=20
                                        }
                                 }
                            }
                    }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000108557A08007E002003230000000A14FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF47", hex);
        }

        [Fact]
        public void Test_Deserilize_0x08()
        {
            byte[] bytes = "000108557A08007E002003230000000A14FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF47".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x08;
            Assert.Equal("2020-03-23", body.JT808_CarDVR_Up_0x08_SpeedPerMinutes[0].StartTime.ToString("yyyy-MM-dd"));
            Assert.Equal(10, body.JT808_CarDVR_Up_0x08_SpeedPerMinutes[0].JT808_CarDVR_Up_0x08_SpeedPerSeconds[0].AvgSpeedAfterStartTime);
            Assert.Equal(20, body.JT808_CarDVR_Up_0x08_SpeedPerMinutes[0].JT808_CarDVR_Up_0x08_SpeedPerSeconds[0].StatusSignalAfterStartTime);
        }
        [Fact]
        public void Test_Serialize_0x09()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x09;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x09
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x09
            {
                JT808_CarDVR_Up_0x09_PositionPerHours = new List<JT808_CarDVR_Up_0x09_PositionPerHour> {
                        new JT808_CarDVR_Up_0x09_PositionPerHour{
                            StartTime=Convert.ToDateTime("2020-03-23"),
                             JT808_CarDVR_Up_0x09_PositionPerMinutes=new List<JT808_CarDVR_Up_0x09_PositionPerMinute>{
                                new JT808_CarDVR_Up_0x09_PositionPerMinute{
                                            AvgSpeedAfterStartTime=10,
                                            GpsLat=23665544,
                                            GpsLng=113656598,
                                            Height=50
                                        }
                                 }
                        }
                    }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000109557A09029A0020032300000006C6431601691B8800320AFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF17", hex);
        }

        [Fact]
        public void Test_Deserilize_0x09()
        {
            byte[] bytes = "000109557A09029A0020032300000006C6431601691B8800320AFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF17".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x09;
            Assert.Equal("2020-03-23", body.JT808_CarDVR_Up_0x09_PositionPerHours[0].StartTime.ToString("yyyy-MM-dd"));
            Assert.Equal(10, body.JT808_CarDVR_Up_0x09_PositionPerHours[0].JT808_CarDVR_Up_0x09_PositionPerMinutes[0].AvgSpeedAfterStartTime);
            Assert.Equal(23665544, body.JT808_CarDVR_Up_0x09_PositionPerHours[0].JT808_CarDVR_Up_0x09_PositionPerMinutes[0].GpsLat);
            Assert.Equal(113656598, body.JT808_CarDVR_Up_0x09_PositionPerHours[0].JT808_CarDVR_Up_0x09_PositionPerMinutes[0].GpsLng);
            Assert.Equal(50, body.JT808_CarDVR_Up_0x09_PositionPerHours[0].JT808_CarDVR_Up_0x09_PositionPerMinutes[0].Height);
        }
        [Fact]
        public void Test_Serialize_0x10()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x10;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x10
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x10
            {
                 JT808_CarDVR_Up_0x10_AccidentSuspectins = new List<JT808_CarDVR_Up_0x10_AccidentSuspectin> {
                        new JT808_CarDVR_Up_0x10_AccidentSuspectin{
                                    EndTime=Convert.ToDateTime("2020-03-23"),
                                   GpsLat=23665544,
                                   GpsLng=113656598,
                                   Height=50,
                                   DriverLicenseNo="430223199009203698",
                                   JT808_CarDVR_Up_0x10_DrivingStatuss=new List<JT808_CarDVR_Up_0x10_DrivingStatus>
                                  {
                                        new JT808_CarDVR_Up_0x10_DrivingStatus{ 
                                            Speed=10,
                                            StatusSignal=20
                                        }
                                  }
                        }
                    }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000110557A1000EA002003230000003433303232333139393030393230333639380A14FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF06C6431601691B8800329D", hex);
        }

        [Fact]
        public void Test_Deserilize_0x10()
        {
            byte[] bytes = "000110557A1000EA002003230000003433303232333139393030393230333639380A14FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF06C6431601691B8800329D".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x10;
            Assert.Equal("2020-03-23", body.JT808_CarDVR_Up_0x10_AccidentSuspectins[0].EndTime.ToString("yyyy-MM-dd"));
            Assert.Equal(23665544, body.JT808_CarDVR_Up_0x10_AccidentSuspectins[0].GpsLat);
            Assert.Equal(113656598, body.JT808_CarDVR_Up_0x10_AccidentSuspectins[0].GpsLng);
            Assert.Equal(50, body.JT808_CarDVR_Up_0x10_AccidentSuspectins[0].Height);
            Assert.Equal("430223199009203698", body.JT808_CarDVR_Up_0x10_AccidentSuspectins[0].DriverLicenseNo);
            Assert.Equal(10, body.JT808_CarDVR_Up_0x10_AccidentSuspectins[0].JT808_CarDVR_Up_0x10_DrivingStatuss[0].Speed);
            Assert.Equal(20, body.JT808_CarDVR_Up_0x10_AccidentSuspectins[0].JT808_CarDVR_Up_0x10_DrivingStatuss[0].StatusSignal);
        }
        [Fact]
        public void Test_Serialize_0x11()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x11;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x11
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x11
            {
                 JT808_CarDVR_Up_0x11_DriveOverTimes = new List<JT808_CarDVR_Up_0x11_DriveOverTime>{
                        new JT808_CarDVR_Up_0x11_DriveOverTime{
                                    ContinueDrivingEndTime=Convert.ToDateTime("2020-03-23"),
                                    ContinueDrivingStartTime=Convert.ToDateTime("2020-03-22"),
                                    GpsStartLat=23665544,
                                    GpsStartLng=113656598,
                                    GpsEndLat=23665545,
                                    GpsEndLng=113656599,
                                    StartHeight=50,
                                    EndHeight=60,
                                    DriverLicenseNo="430223199009203698"
                        }
                    }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000111557A1100320034333032323331393930303932303336393820032200000020032300000006C6431601691B88003206C6431701691B89003C09", hex);
        }

        [Fact]
        public void Test_Deserilize_0x11()
        {
            byte[] bytes = "000111557A1100320034333032323331393930303932303336393820032200000020032300000006C6431601691B88003206C6431701691B89003C09".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x11;
            Assert.Equal("2020-03-23", body.JT808_CarDVR_Up_0x11_DriveOverTimes[0].ContinueDrivingEndTime.ToString("yyyy-MM-dd"));
            Assert.Equal("2020-03-22", body.JT808_CarDVR_Up_0x11_DriveOverTimes[0].ContinueDrivingStartTime.ToString("yyyy-MM-dd"));
            Assert.Equal(23665544, body.JT808_CarDVR_Up_0x11_DriveOverTimes[0].GpsStartLat);
            Assert.Equal(113656598, body.JT808_CarDVR_Up_0x11_DriveOverTimes[0].GpsStartLng);
            Assert.Equal(50, body.JT808_CarDVR_Up_0x11_DriveOverTimes[0].StartHeight);
            Assert.Equal(23665545, body.JT808_CarDVR_Up_0x11_DriveOverTimes[0].GpsEndLat);
            Assert.Equal(113656599, body.JT808_CarDVR_Up_0x11_DriveOverTimes[0].GpsEndLng);
            Assert.Equal(60, body.JT808_CarDVR_Up_0x11_DriveOverTimes[0].EndHeight);
            Assert.Equal("430223199009203698", body.JT808_CarDVR_Up_0x11_DriveOverTimes[0].DriverLicenseNo);
        }
        [Fact]
        public void Test_Serialize_0x12()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x12;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x12
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x12
            {
                 JT808_CarDVR_Up_0x12_DriveLogins = new  List<JT808_CarDVR_Up_0x12_DriveLogin>{
                        new JT808_CarDVR_Up_0x12_DriveLogin{
                            LoginTime=Convert.ToDateTime("2020-03-23"),
                            LoginType=1,
                            DriverLicenseNo="430223199009203698"
                        }
                    }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000112557A12001900200323000000343330323233313939303039323033363938012F", hex);
        }

        [Fact]
        public void Test_Deserilize_0x12()
        {
            byte[] bytes = "000112557A12001900200323000000343330323233313939303039323033363938012F".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x12;
            Assert.Equal("2020-03-23", body.JT808_CarDVR_Up_0x12_DriveLogins[0].LoginTime.ToString("yyyy-MM-dd"));
            Assert.Equal(1, body.JT808_CarDVR_Up_0x12_DriveLogins[0].LoginType);
            Assert.Equal("430223199009203698", body.JT808_CarDVR_Up_0x12_DriveLogins[0].DriverLicenseNo);
        }
        [Fact]
        public void Test_Serialize_0x13()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x13;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x13
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x13
            {
                 JT808_CarDVR_Up_0x13_ExternalPowerSupplys = new List<JT808_CarDVR_Up_0x13_ExternalPowerSupply>{
                        new JT808_CarDVR_Up_0x13_ExternalPowerSupply{
                            EventTime=Convert.ToDateTime("2020-03-23"),
                             EventType=1
                        }
                    }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000113557A13000700200323000000013A", hex);
        }

        [Fact]
        public void Test_Deserilize_0x13()
        {
            byte[] bytes = "000113557A13000700200323000000013A".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x13;
            Assert.Equal("2020-03-23", body.JT808_CarDVR_Up_0x13_ExternalPowerSupplys[0].EventTime.ToString("yyyy-MM-dd"));
            Assert.Equal(1, body.JT808_CarDVR_Up_0x13_ExternalPowerSupplys[0].EventType);
        }
        [Fact]
        public void Test_Serialize_0x14()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x14;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x14
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x14
            {
                 JT808_CarDVR_Up_0x14_ParameterModifys = new List<JT808_CarDVR_Up_0x14_ParameterModify>{
                        new JT808_CarDVR_Up_0x14_ParameterModify{
                            EventTime=Convert.ToDateTime("2020-03-23"),
                             EventType=1
                        }
                    }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000114557A14000700200323000000013D", hex);
        }

        [Fact]
        public void Test_Deserilize_0x14()
        {
            byte[] bytes = "000114557A14000700200323000000013D".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x14;
            Assert.Equal("2020-03-23", body.JT808_CarDVR_Up_0x14_ParameterModifys[0].EventTime.ToString("yyyy-MM-dd"));
            Assert.Equal(1, body.JT808_CarDVR_Up_0x14_ParameterModifys[0].EventType);
        }
        [Fact]
        public void Test_Serialize_0x15()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x15;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x15
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x15
            {
                 JT808_CarDVR_Up_0x15_SpeedStatusLogs = new List<JT808_CarDVR_Up_0x15_SpeedStatusLog>{
                        new JT808_CarDVR_Up_0x15_SpeedStatusLog{
                             SpeedStatusStartTime=Convert.ToDateTime("2020-03-22"),
                             SpeedStatusEndTime=Convert.ToDateTime("2020-03-23"),
                             SpeedStatus=1,
                               JT808_CarDVR_Up_0x15_SpeedPerSeconds=new List<JT808_CarDVR_Up_0x15_SpeedPerSecond>{ 
                                 new JT808_CarDVR_Up_0x15_SpeedPerSecond{
                                        RecordSpeed=50,
                                        ReferenceSpeed=40
                                  }
                               }
                        }
                    }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000115557A15008500012003220000002003230000003228FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA5", hex);
        }

        [Fact]
        public void Test_Deserilize_0x15()
        {
            byte[] bytes = "000115557A15008500012003220000002003230000003228FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA5".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x15;
            Assert.Equal("2020-03-22", body.JT808_CarDVR_Up_0x15_SpeedStatusLogs[0].SpeedStatusStartTime.ToString("yyyy-MM-dd"));
            Assert.Equal("2020-03-23", body.JT808_CarDVR_Up_0x15_SpeedStatusLogs[0].SpeedStatusEndTime.ToString("yyyy-MM-dd"));
            Assert.Equal(1, body.JT808_CarDVR_Up_0x15_SpeedStatusLogs[0].SpeedStatus);
            Assert.Equal(50, body.JT808_CarDVR_Up_0x15_SpeedStatusLogs[0].JT808_CarDVR_Up_0x15_SpeedPerSeconds[0].RecordSpeed);
            Assert.Equal(40, body.JT808_CarDVR_Up_0x15_SpeedStatusLogs[0].JT808_CarDVR_Up_0x15_SpeedPerSeconds[0].ReferenceSpeed);
        }

        [Fact]
        public void Test_Serialize_0x82()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0x82;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0x82
            };
            value.JT808CarDVRUpPackage.Bodies = new JT808_CarDVR_Up_0x82();
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("000182557A82000000AD", hex);
        }

        [Fact]
        public void Test_Deserilize_0x82()
        {
            byte[] bytes = "000182557A82000000AD".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            var body = value.JT808CarDVRUpPackage.Bodies as JT808_CarDVR_Up_0x82;
        }

        [Fact]
        public void Test_Serialize_Error()
        {
            JT808_0x0700 value = new JT808_0x0700();
            value.CommandId = 0xFA;
            value.ReplyMsgNum = 1;
            value.JT808CarDVRUpPackage = new JT808CarDVRUpPackage
            {
                CommandId = 0xFA
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("0001FA557AFA00D5", hex);
        }

        [Fact]
        public void Test_Deserilize_Error()
        {
            byte[] bytes = "0001FA557AFA00D5".ToHexBytes();
            JT808_0x0700 value = JT808Serializer.Deserialize<JT808_0x0700>(bytes);
            Assert.Equal(1, value.ReplyMsgNum);
            Assert.True(value.JT808CarDVRUpPackage.ErrorFlag);
        }

        [Fact]
        public void Test_Json_Error()
        {
            byte[] bytes = "0001FA557AFA00D5".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0700>(bytes);
        }
    }
}
