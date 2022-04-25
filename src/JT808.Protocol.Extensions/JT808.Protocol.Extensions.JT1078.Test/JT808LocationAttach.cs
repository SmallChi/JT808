using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public class JT808LocationAttach
    {
        JT808Serializer JT808Serializer;

        public JT808LocationAttach()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1.AddJT808Configure(new DefaultGlobalConfig())
                .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<DefaultGlobalConfig>();
            JT808Serializer = new JT808Serializer(defaultConfig);
        }
        [Fact]
        public void Test1()
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
            jT808UploadLocationRequest.CustomLocationAttachData.Add(0x14, new JT808_0x0200_0x14
            {
                VideoRelateAlarm = 100
            });
            jT808UploadLocationRequest.CustomLocationAttachData.Add(0x15, new JT808_0x0200_0x15
            {
                VideoSignalLoseAlarmStatus = 100
            });
            jT808UploadLocationRequest.CustomLocationAttachData.Add(0x16, new JT808_0x0200_0x16
            {
                VideoSignalOcclusionAlarmStatus = 100
            });
            jT808UploadLocationRequest.CustomLocationAttachData.Add(0x17, new JT808_0x0200_0x17
            {
                StorageFaultAlarmStatus = 100
            });
            jT808UploadLocationRequest.CustomLocationAttachData.Add(0x18, new JT808_0x0200_0x18
            {
                AbnormalDrivingBehaviorAlarmType = 100,
                FatigueLevel = 88
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C0000180715101010140400000064150400000064160400000064170200641803006458", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bodys = "000000010000000200BA7F0E07E4F11C0028003C0000180715101010140400000064150400000064160400000064170200641803006458".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal(1u, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2u, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100u, ((JT808_0x0200_0x14)jT808UploadLocationRequest.CustomLocationAttachData[0x14]).VideoRelateAlarm);
            Assert.Equal(100u, ((JT808_0x0200_0x15)jT808UploadLocationRequest.CustomLocationAttachData[0x15]).VideoSignalLoseAlarmStatus);
            Assert.Equal(100u, ((JT808_0x0200_0x16)jT808UploadLocationRequest.CustomLocationAttachData[0x16]).VideoSignalOcclusionAlarmStatus);
            Assert.Equal(100u, ((JT808_0x0200_0x17)jT808UploadLocationRequest.CustomLocationAttachData[0x17]).StorageFaultAlarmStatus);
            Assert.Equal(100u, ((JT808_0x0200_0x18)jT808UploadLocationRequest.CustomLocationAttachData[0x18]).AbnormalDrivingBehaviorAlarmType);
            Assert.Equal(88, ((JT808_0x0200_0x18)jT808UploadLocationRequest.CustomLocationAttachData[0x18]).FatigueLevel);
        }

        [Fact]
        public void Test3()
        {
            byte[] bodys = "000000010000000200BA7F0E07E4F11C0028003C0000180715101010140400000064150400000064160400000064170200641803006458".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0200>(bodys);
        }
    }
}
