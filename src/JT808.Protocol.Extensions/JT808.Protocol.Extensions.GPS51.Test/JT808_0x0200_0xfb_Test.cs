using JT808.Protocol.Extensions.GPS51.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.GPS51.Test
{
    public class JT808_0x0200_0xfb_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200_0xfb_Test()
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure()
                               .AddGPS51Configure();

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
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_GPS51_Constants.JT808_0x0200_0xfb, new JT808_0x0200_0xfb
            {
                AttachInfoId = 0xfb,
                AttachInfoLength = 1,
                    Power = 1234,
                    PowerPercent=90,
                    Status=3,
            });
            var hex = JT808Serializer.Serialize<JT808_0x0200>(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C0000180715101010FB045A04D203", hex);
        }

        [Fact]
        public void Deserialize()
        {
            var jt808_0x0200 = JT808Serializer.Deserialize<JT808_0x0200>("000000010000000200BA7F0E07E4F11C0028003C0000180715101010FB045A04D203".ToHexBytes());
            jt808_0x0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0xfb, out var value);
            var jt808_0x0200_0xfb = value as JT808_0x0200_0xfb; 
            Assert.Equal(1234, jt808_0x0200_0xfb.Power);
            Assert.Equal(90, jt808_0x0200_0xfb.PowerPercent);
            Assert.Equal(3, jt808_0x0200_0xfb.Status);
        }
        [Fact]
        public void Deserialize1()
        {
            //gps51 demo
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808Package>("7e0200002f04121051313003940000000000002002015fe78006c325d50013000a00a0210924172909010400029d0f2a0200042b0450680014520103fb045F050701097e".ToHexBytes());
            var body0200 = jT808UploadLocationRequest.Bodies as JT808_0x0200;
            body0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0xfb ,out var value);
            var jt808_0x0200_0xfb = value as JT808_0x0200_0xfb;
            Assert.Equal(0x5F, jt808_0x0200_0xfb.PowerPercent);
            Assert.Equal(0x0507, jt808_0x0200_0xfb.Power);
            Assert.Equal(0x01, jt808_0x0200_0xfb.Status);
        }
    }
}
