using JT808.Protocol.Extensions.GPS51.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.GPS51.Test
{
    public class JT808_0x0200_0xfa_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200_0xfa_Test()
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
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_GPS51_Constants.JT808_0x0200_0xfa, new JT808_0x0200_0xfa
            {
                AttachInfoId = 0xfa,
                AttachInfoLength = 1,
                    Alarm = 1234
            });
            var hex = JT808Serializer.Serialize<JT808_0x0200>(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C0000180715101010FA04000004D2", hex);
        }

        [Fact]
        public void Deserialize()
        {
            var jt808_0x0200 = JT808Serializer.Deserialize<JT808_0x0200>("000000010000000200BA7F0E07E4F11C0028003C0000180715101010FA04000004D2".ToHexBytes());
            jt808_0x0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0xfa, out var value);
            var jt808_0x0200_0xfa = value as JT808_0x0200_0xfa; 
            Assert.Equal(1234u, jt808_0x0200_0xfa.Alarm);

        }
        [Fact]
        public void Deserialize1()
        {
            //gps51 demo
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808Package>("7e0200006701300001111100d200010000800c00c101585b2406c96b7000000000000021101117593601040012d6ee0202000003020000140480000005150400000008160400000000170200072504000000002b040000000030011f310100ef0d49249249260049249202221103fa0400000001937e".ToHexBytes());
            var body0200 = jT808UploadLocationRequest.Bodies as JT808_0x0200;
            body0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0xfa ,out var value);
            var jt808_0x0200_0xfa = value as JT808_0x0200_0xfa;
            Assert.Equal(1u, jt808_0x0200_0xfa.Alarm);
        }
    }
}
