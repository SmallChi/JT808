using JT808.Protocol.Extensions.GPS51.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.GPS51.Test
{
    public class JT808_0x0200_0x54_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200_0x54_Test()
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
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_GPS51_Constants.JT808_0x0200_0x54, new JT808_0x0200_0x54
            {
                AttachInfoId = 0x54,
                AttachInfoLength = 1,
                Count = 1,
                WifiInfos = new List<WifiInfo> {
                       new WifiInfo{
                            SingnalStrength=12,
                             WifiMac="E4FDA120FFF1"
                       }
                  }
            });
            var hex = JT808Serializer.Serialize<JT808_0x0200>(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C0000180715101010540001E4FDA120FFF10C", hex);
        }

        [Fact]
        public void Deserialize()
        {
            var jt808_0x0200 = JT808Serializer.Deserialize<JT808_0x0200>("000000010000000200BA7F0E07E4F11C0028003C0000180715101010540001E4FDA120FFF10C".ToHexBytes());
            jt808_0x0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0x54, out var value);
            var jt808_0x0200_0x54 = value as JT808_0x0200_0x54;
            Assert.Equal(12, jt808_0x0200_0x54.WifiInfos[0].SingnalStrength);
            Assert.Equal("E4FDA120FFF1", jt808_0x0200_0x54.WifiInfos[0].WifiMac);
        }
        [Fact]
        public void Deserialize1()
        {
            //gps51 demo
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808Package>("7e020100f503570526020700331aa900000000200c00000159236c06cc037c007d020000010c21112213040001040000056f300119603d0601cc0024a402bb25153b01cc0024a40d1d0f4b0001cc0024a402bb25010001cc0024a40d1d0f4e0001cc0024a40d1d0f4f0001cc0024a40996ab4300542405f092b4a22a39ca70af6a1cc981c98ca6df48ce12c888403b68e784c7e4fda120fff0c4f026e60b05f092b4a22a39ca70af6a1cc981c98ca6df48ce12c888403b68e784c7e4fda120fff0c4fe3fe60200014807002a573130305f56312e3033443b4c54453a32353b47423a302c313b4d323a33302c3330303b423a342e303020000a898604811920701659546b7e".ToHexBytes());
            var body0201 = jT808UploadLocationRequest.Bodies as JT808_0x0201;
            body0201.Position.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0x54 ,out var value);
            var jt808_0x0200_0x54 = value as JT808_0x0200_0x54;
            Assert.Equal(202, jt808_0x0200_0x54.WifiInfos[0].SingnalStrength);
            Assert.Equal("F092B4A22A39", jt808_0x0200_0x54.WifiInfos[0].WifiMac);
            Assert.Equal(201, jt808_0x0200_0x54.WifiInfos[1].SingnalStrength);
            Assert.Equal("70AF6A1CC981", jt808_0x0200_0x54.WifiInfos[1].WifiMac);
            Assert.Equal(200, jt808_0x0200_0x54.WifiInfos[2].SingnalStrength);
            Assert.Equal("8CA6DF48CE12", jt808_0x0200_0x54.WifiInfos[2].WifiMac);
            Assert.Equal(199, jt808_0x0200_0x54.WifiInfos[3].SingnalStrength);
            Assert.Equal("88403B68E784", jt808_0x0200_0x54.WifiInfos[3].WifiMac);
            Assert.Equal(196, jt808_0x0200_0x54.WifiInfos[4].SingnalStrength);
            Assert.Equal("E4FDA120FFF0", jt808_0x0200_0x54.WifiInfos[4].WifiMac);
        }
    }
}
