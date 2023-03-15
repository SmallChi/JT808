using JT808.Protocol.Extensions.SuBiao.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.SuBiao.Test
{
    public class JT808_0x0200_0x64_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200_0x64_Test()
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure()
                                        .AddSuBiaoConfigure();
            IJT808Config jT808Config = serviceDescriptors.BuildServiceProvider().GetRequiredService<IJT808Config>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Serializer()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>()
            };
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_SuBiao_Constants.JT808_0X0200_0x64, new JT808_0x0200_0x64
            {
                AlarmId = 1,
                AlarmIdentification = new Metadata.AlarmIdentificationProperty
                {
                    AttachCount = 2,
                    SN = 3,
                    TerminalID = "4444444",
                    Time = Convert.ToDateTime("2019-12-10 18:31:00")
                },
                AlarmLevel = 5,
                AlarmOrEventType = 6,
                AlarmTime = Convert.ToDateTime("2019-12-11 18:31:00"),
                Altitude = 7,
                CarOrPedestrianDistanceAhead = 10,
                DeviateType = 11,
                FlagState = 12,
                Latitude = 13,
                Longitude = 14,
                RoadSignIdentificationData = 15,
                RoadSignIdentificationType = 16,
                Speed = 17,
                VehicleSpeed = 18,
                VehicleState = 19
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C0000180715101010642F000000010C0605120A0B100F1100070000000D0000000E191211183100001334343434343434191210183100030200", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>("000000010000000200BA7F0E07E4F11C0028003C00001807151010106420000000010C0605120A0B100F1100070000000D0000000E191211183100001334343434343434191210183100030200".ToHexBytes());
            jT808UploadLocationRequest.CustomLocationAttachData.TryGetValue(JT808_SuBiao_Constants.JT808_0X0200_0x64, out var value);
            JT808_0x0200_0x64 jT808_0X0200_0X64 = value as JT808_0x0200_0x64;
            Assert.Equal(1u, jT808_0X0200_0X64.AlarmId);
            Assert.Equal(2, jT808_0X0200_0X64.AlarmIdentification.AttachCount);
            Assert.Equal(3, jT808_0X0200_0X64.AlarmIdentification.SN);
            Assert.Equal("4444444", jT808_0X0200_0X64.AlarmIdentification.TerminalID);
            Assert.Equal(Convert.ToDateTime("2019-12-10 18:31:00"), jT808_0X0200_0X64.AlarmIdentification.Time);
            Assert.Equal(5, jT808_0X0200_0X64.AlarmLevel);
            Assert.Equal(6, jT808_0X0200_0X64.AlarmOrEventType);
            Assert.Equal(Convert.ToDateTime("2019-12-11 18:31:00"), jT808_0X0200_0X64.AlarmTime);
            Assert.Equal(7, jT808_0X0200_0X64.Altitude);
            Assert.Equal(0x64, jT808_0X0200_0X64.AttachInfoId);
            Assert.Equal(32, jT808_0X0200_0X64.AttachInfoLength);
            Assert.Equal(10, jT808_0X0200_0X64.CarOrPedestrianDistanceAhead);
            Assert.Equal(11, jT808_0X0200_0X64.DeviateType);
            Assert.Equal(12, jT808_0X0200_0X64.FlagState);
            Assert.Equal(13, jT808_0X0200_0X64.Latitude);
            Assert.Equal(14, jT808_0X0200_0X64.Longitude);
            Assert.Equal(15, jT808_0X0200_0X64.RoadSignIdentificationData);
            Assert.Equal(16, jT808_0X0200_0X64.RoadSignIdentificationType);
            Assert.Equal(17, jT808_0X0200_0X64.Speed);
            Assert.Equal(18, jT808_0X0200_0X64.VehicleSpeed);
            Assert.Equal(19, jT808_0X0200_0X64.VehicleState);
        }

        [Fact]
        public void Deserialize1()
        {
            var json = JT808Serializer.Analyze<JT808_0x0200>("000000010000000200BA7F0E07E4F11C0028003C00001807151010106420000000010C0605120A0B100F1100070000000D0000000E191211183100001334343434343434191210183100030200".ToHexBytes());
        }

        [Fact]
        public void Deserialize2()
        {
            JT808Serializer.Instance.Register(JT808_SuBiao_Constants.GetCurrentAssembly());
            var json = JT808Serializer.Instance.Analyze<JT808_0x0200>("000000010000000200BA7F0E07E4F11C0028003C00001807151010106420000000010C0605120A0B100F1100070000000D0000000E191211183100001334343434343434191210183100030200".ToHexBytes());
        }
    }
}
