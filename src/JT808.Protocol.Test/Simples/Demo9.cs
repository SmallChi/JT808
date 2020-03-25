using JT808.Protocol.Enums;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System.Text.Json;
using JT808.Protocol.MessageBody.CarDVR;

namespace JT808.Protocol.Test.Simples
{
    public class Demo9
    {
        JT808CarDVRSerializer JT808CarDVRSerializer;
        JT808Serializer JT808Serializer;
        IJT808Config jT808Config;
        public Demo9()
        {
            jT808Config = new DefaultGlobalConfig();
            JT808CarDVRSerializer = new JT808CarDVRSerializer(jT808Config);
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test1()
        {
            JT808CarDVRSerializer jT808CarDVRSerializer = jT808Config.GetCarDVRSerializer();
            Assert.Equal(jT808CarDVRSerializer.SerializerId, JT808CarDVRSerializer.SerializerId);
        }

        [Fact]
        public void Test2()
        {
            JT808CarDVRDownPackage jT808CarDVRDownPackage = new JT808CarDVRDownPackage();
            jT808CarDVRDownPackage.CommandId = JT808CarDVRCommandID.设置初始里程.ToByteValue();
            jT808CarDVRDownPackage.Bodies = new JT808_CarDVR_Down_0xC4()
            {
                 FirstInstallTime=DateTime.Parse("2020-03-25 10:26:01"),
                 FirstMileage="1234",
                 RealTime = DateTime.Parse("2020-03-25 10:26:01"),
                 TotalMilage="123456"
            };
            byte[] downData = JT808CarDVRSerializer.Serialize(jT808CarDVRDownPackage);
            var downHex = downData.ToHexString();
            Assert.Equal("557AC40014002003251026012003251026010000123400123456A9", downHex);
        }

        [Fact]
        public void Test3()
        {
            var data = "557AC40014002003251026012003251026010000123400123456A9".ToHexBytes();
            JT808CarDVRDownPackage jT808CarDVRDownPackage = JT808CarDVRSerializer.DownDeserialize(data);
            Assert.Equal(JT808CarDVRCommandID.设置初始里程.ToByteValue(), jT808CarDVRDownPackage.CommandId);
            var value = jT808CarDVRDownPackage.Bodies as JT808_CarDVR_Down_0xC4;    
            Assert.Equal(DateTime.Parse("2020-03-25 10:26:01"), value.FirstInstallTime);
            Assert.Equal(DateTime.Parse("2020-03-25 10:26:01"), value.RealTime);
            Assert.Equal("1234", value.FirstMileage);
            Assert.Equal("123456", value.TotalMilage);
        }

        [Fact]
        public void Test4()
        {
            JT808CarDVRUpPackage package = new JT808CarDVRUpPackage();
            package.CommandId = JT808CarDVRCommandID.设置初始里程.ToByteValue();
            package.Bodies = new JT808_CarDVR_Up_0xC4()
            {
                
            };
            byte[] data = JT808CarDVRSerializer.Serialize(package);
            var hex = data.ToHexString();
            Assert.Equal("557AC4000000EB", hex);
        }

        [Fact]
        public void Test5()
        {
            var data = "557AC4000000EB".ToHexBytes();
            JT808CarDVRUpPackage package = JT808CarDVRSerializer.UpDeserialize(data);
            Assert.Equal(JT808CarDVRCommandID.设置初始里程.ToByteValue(), package.CommandId);
            Assert.Null(package.Bodies);
        }

        [Fact]
        public void Test6()
        {
            JT808_0x8701 value = new JT808_0x8701();
            value.CommandId = 0x82;
            value.JT808CarDVRDownPackage = new JT808CarDVRDownPackage
            {
                CommandId = 0x82,
                Bodies = new JT808_CarDVR_Down_0x82()
                {
                    VehicleNo = "粤B12345",
                    VehicleType = "重型货车",
                    Vin = "12345678912345678"
                }
            };
            var hex = JT808Serializer.Serialize(value).ToHexString();
            Assert.Equal("82557A820027003132333435363738393132333435363738D4C142313233343500000000D6D8D0CDBBF5B3B500008E", hex);
        }

        [Fact]
        public void Test7()
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
    }
}
