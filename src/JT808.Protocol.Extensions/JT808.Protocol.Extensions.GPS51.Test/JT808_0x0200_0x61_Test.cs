using JT808.Protocol.Extensions.GPS51.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.GPS51.Test
{
    public class JT808_0x0200_0x61_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200_0x61_Test()
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
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_GPS51_Constants.JT808_0x0200_0x61, new JT808_0x0200_0x61
            {
                AttachInfoId = 0x61,
                AttachInfoLength = 1,
                   Volage = 200
            });
            var hex = JT808Serializer.Serialize<JT808_0x0200>(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C0000180715101010610100C8", hex);
        }

        [Fact]
        public void Deserialize()
        {
            var jt808_0x0200 = JT808Serializer.Deserialize<JT808_0x0200>("000000010000000200BA7F0E07E4F11C0028003C0000180715101010610100C8".ToHexBytes());
            jt808_0x0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0x61, out var value);
            var jt808_0x0200_0x61 = value as JT808_0x0200_0x61; 
            Assert.Equal(200, jt808_0x0200_0x61.Volage);

        }
        [Fact]
        public void Deserialize1()
        {
            //gps51 demo
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808Package>("7e0200002c0138083582460440000000000000000301d37b35063f5e4901a0014a011b210917171126010400000c4530011531010461021d74a27e".ToHexBytes());
            var body0200 = jT808UploadLocationRequest.Bodies as JT808_0x0200;
            body0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0x61 ,out var value);
            var jt808_0x0200_0x61= value as JT808_0x0200_0x61;
            Assert.Equal(0x1d74, jt808_0x0200_0x61.Volage);
            
        }
    }
}
