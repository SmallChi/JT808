using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody.CarDVR;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x8701Test
    {
        JT808Serializer JT808Serializer;

        public JT808_0x8701Test()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Test_Analyze()
        {
            //0x82
            byte[] bytes = "82557A820027003132333435363738393132333435363738D4C142313233343500000000D6D8D0CDBBF5B3B500008E".ToHexBytes();
            var value = JT808Serializer.Analyze<JT808_0x8701>(bytes);
            // 0x83
            bytes = "83557A83000600200323101010BA".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x8701>(bytes);
            //0x84
            bytes = "84557A84005700200323101010FFD7D4B6A8D2E531000000D7D4B6A8D2E532000000D7D4B6A8D2E533000000BDFCB9E2B5C600000000D4B6B9E2B5C600000000D3D2D7AACFF200000000D7F3D7AACFF200000000D6C6B6AF00000000000006".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x8701>(bytes);
            // 0xC2
            bytes = "C2557AC2000600200323101010FB".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x8701>(bytes);
            //0xC3
            bytes = "C3557AC30008002003231010100032C6".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x8701>(bytes);
            // 0xC4
            bytes = "C4557AC40014002003231010102003221010100000100000005000BE".ToHexBytes();
            value = JT808Serializer.Analyze<JT808_0x8701>(bytes);
        }
        [Fact]
        public void Test_Serilize_0x82()
        {
            JT808_0x8701 value = new JT808_0x8701();
            value.CommandId = 0x82;
            value.JT808CarDVRDownPackage = new JT808CarDVRDownPackage
            {
                CommandId = 0x82,
                Bodies = new JT808_CarDVR_Down_0x82() { 
                   VehicleNo="粤B12345",
                   VehicleType="重型货车",
                   Vin="12345678912345678"
                }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("82557A820027003132333435363738393132333435363738D4C142313233343500000000D6D8D0CDBBF5B3B500008E", hex);
        }

        [Fact]
        public void Test_Deserilize_0x82()
        {
            byte[] bytes = "82557A820027003132333435363738393132333435363738D4C142313233343500000000D6D8D0CDBBF5B3B500008E".ToHexBytes();
            JT808_0x8701 value = JT808Serializer.Deserialize<JT808_0x8701>(bytes);
            Assert.Equal(0x82, value.CommandId);
            var body = value.JT808CarDVRDownPackage as JT808CarDVRDownPackage;
            Assert.Equal(0x82, body.CommandId);
            var subBody = body.Bodies as JT808_CarDVR_Down_0x82;
            Assert.Equal("粤B12345", subBody.VehicleNo);
            Assert.Equal("重型货车", subBody.VehicleType);
            Assert.Equal("12345678912345678", subBody.Vin);
        }
        [Fact]
        public void Test_Serilize_0x83()
        {
            JT808_0x8701 value = new JT808_0x8701();
            value.CommandId = 0x83;
            value.JT808CarDVRDownPackage = new JT808CarDVRDownPackage
            {
                CommandId = 0x83,
                Bodies = new JT808_CarDVR_Down_0x83()
                {
                    RealTime = Convert.ToDateTime("2020-03-23 10:10:10")
                }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("83557A83000600200323101010BA", hex);
        }

        [Fact]
        public void Test_Deserilize_0x83()
        {
            byte[] bytes = "83557A83000600200323101010BA".ToHexBytes();
            JT808_0x8701 value = JT808Serializer.Deserialize<JT808_0x8701>(bytes);
            Assert.Equal(0x83, value.CommandId);
            var body = value.JT808CarDVRDownPackage as JT808CarDVRDownPackage;
            Assert.Equal(0x83, body.CommandId);
            var subBody = body.Bodies as JT808_CarDVR_Down_0x83;
            Assert.Equal("2020-03-23 10:10:10", subBody.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        [Fact]
        public void Test_Serilize_0x84()
        {
            JT808_0x8701 value = new JT808_0x8701();
            value.CommandId = 0x84;
            value.JT808CarDVRDownPackage = new JT808CarDVRDownPackage
            {
                CommandId = 0x84,
                Bodies = new JT808_CarDVR_Down_0x84()
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
                }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("84557A84005700200323101010FFD7D4B6A8D2E531000000D7D4B6A8D2E532000000D7D4B6A8D2E533000000BDFCB9E2B5C600000000D4B6B9E2B5C600000000D3D2D7AACFF200000000D7F3D7AACFF200000000D6C6B6AF00000000000006", hex);
        }

        [Fact]
        public void Test_Deserilize_0x84()
        {
            byte[] bytes = "84557A84005700200323101010FFD7D4B6A8D2E531000000D7D4B6A8D2E532000000D7D4B6A8D2E533000000BDFCB9E2B5C600000000D4B6B9E2B5C600000000D3D2D7AACFF200000000D7F3D7AACFF200000000D6C6B6AF00000000000006".ToHexBytes();
            JT808_0x8701 value = JT808Serializer.Deserialize<JT808_0x8701>(bytes);
            Assert.Equal(0x84, value.CommandId);
            var body = value.JT808CarDVRDownPackage as JT808CarDVRDownPackage;
            Assert.Equal(0x84, body.CommandId);
            var subBody = body.Bodies as JT808_CarDVR_Down_0x84;
            Assert.Equal("远光灯", subBody.FarLight);
            Assert.Equal("制动", subBody.Brake);
            Assert.Equal("自定义1", subBody.D0);
            Assert.Equal("自定义2", subBody.D1);
            Assert.Equal("自定义3", subBody.D2);
            Assert.Equal("左转向", subBody.LeftTurn);
            Assert.Equal("近光灯", subBody.NearLight);
            Assert.Equal("2020-03-23 10:10:10", subBody.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal("右转向", subBody.RightTurn);
            Assert.Equal(255, subBody.SignalOperate);
        }
        [Fact]
        public void Test_Serilize_0xC2()
        {
            JT808_0x8701 value = new JT808_0x8701();
            value.CommandId = 0xC2;
            value.JT808CarDVRDownPackage = new JT808CarDVRDownPackage
            {
                CommandId = 0xC2,
                Bodies = new JT808_CarDVR_Down_0xC2()
                {                     
                    RealTime = Convert.ToDateTime("2020-03-23 10:10:10")
                }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("C2557AC2000600200323101010FB", hex);
        }

        [Fact]
        public void Test_Deserilize_0xC2()
        {
            byte[] bytes = "C2557AC2000600200323101010FB".ToHexBytes();
            JT808_0x8701 value = JT808Serializer.Deserialize<JT808_0x8701>(bytes);
            Assert.Equal(0xC2, value.CommandId);
            var body = value.JT808CarDVRDownPackage as JT808CarDVRDownPackage;
            Assert.Equal(0xC2, body.CommandId);
            var subBody = body.Bodies as JT808_CarDVR_Down_0xC2;
            Assert.Equal("2020-03-23 10:10:10", subBody.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        [Fact]
        public void Test_Serilize_0xC3()
        {
            JT808_0x8701 value = new JT808_0x8701();
            value.CommandId = 0xC3;
            value.JT808CarDVRDownPackage = new JT808CarDVRDownPackage
            {
                CommandId = 0xC3,
                Bodies = new JT808_CarDVR_Down_0xC3()
                {
                    PulseCoefficient=50,
                    RealTime = Convert.ToDateTime("2020-03-23 10:10:10")
                }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("C3557AC30008002003231010100032C6", hex);
        }

        [Fact]
        public void Test_Deserilize_0xC3()
        {
            byte[] bytes = "C3557AC30008002003231010100032C6".ToHexBytes();
            JT808_0x8701 value = JT808Serializer.Deserialize<JT808_0x8701>(bytes);
            Assert.Equal(0xC3, value.CommandId);
            var body = value.JT808CarDVRDownPackage as JT808CarDVRDownPackage;
            Assert.Equal(0xC3, body.CommandId);
            var subBody = body.Bodies as JT808_CarDVR_Down_0xC3;
            Assert.Equal("2020-03-23 10:10:10", subBody.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(50, subBody.PulseCoefficient);
        }
        [Fact]
        public void Test_Serilize_0xC4()
        {
            JT808_0x8701 value = new JT808_0x8701();
            value.CommandId = 0xC4;
            value.JT808CarDVRDownPackage = new JT808CarDVRDownPackage
            {
                CommandId = 0xC4,
                Bodies = new JT808_CarDVR_Down_0xC4()
                {
                    FirstInstallTime = Convert.ToDateTime("2020-03-22 10:10:10"),
                    RealTime = Convert.ToDateTime("2020-03-23 10:10:10"),
                    FirstMileage="1000",
                    TotalMilage="5000"
                }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("C4557AC40014002003231010102003221010100000100000005000BE", hex);
        }

        [Fact]
        public void Test_Deserilize_0xC4()
        {
            byte[] bytes = "C4557AC40014002003231010102003221010100000100000005000BE".ToHexBytes();
            JT808_0x8701 value = JT808Serializer.Deserialize<JT808_0x8701>(bytes);
            Assert.Equal(0xC4, value.CommandId);
            var body = value.JT808CarDVRDownPackage as JT808CarDVRDownPackage;
            Assert.Equal(0xC4, body.CommandId);
            var subBody = body.Bodies as JT808_CarDVR_Down_0xC4;
            Assert.Equal("2020-03-22 10:10:10", subBody.FirstInstallTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal("2020-03-23 10:10:10", subBody.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal("1000", subBody.FirstMileage);
            Assert.Equal("5000", subBody.TotalMilage);
        }
    }
}
