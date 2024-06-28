using JT808.Protocol.Extensions.GPS51.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.GPS51.Test
{
    public class JT808_0x0200_0x58_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200_0x58_Test()
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
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_GPS51_Constants.JT808_0x0200_0x58, new JT808_0x0200_0x58
            {
                AttachInfoId = 0x58,
                AttachInfoLength = 1, 
                 Humiditys=new List<ushort> { 
                    12,34
                 }
            });
            var hex = JT808Serializer.Serialize<JT808_0x0200>(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C00001807151010105804000C0022", hex);
        }

        [Fact]
        public void Deserialize()
        {
            var jt808_0x0200 = JT808Serializer.Deserialize<JT808_0x0200>("000000010000000200BA7F0E07E4F11C0028003C00001807151010105804000C0022".ToHexBytes());
            jt808_0x0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0x58, out var value);
            var jt808_0x0200_0x58 = value as JT808_0x0200_0x58; 
            Assert.Equal(new List<ushort> {12,34 }, jt808_0x0200_0x58.Humiditys.ToArray());
        }
        [Fact]
        public void Deserialize1()
        {
            //gps51 demo
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808Package>("7E0200005B0138916061090003000000000000000301595D5206C8EE4C038E000100001710191146400104000000942504000000005110010001010104010301040103010401045708000000000000000058100285026302700298029A027B02860281520103F300EB7E".ToHexBytes());
            var body0200 = jT808UploadLocationRequest.Bodies as JT808_0x0200;
            body0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0x58 ,out var value);
            var jt808_0x0200_0x58 = value as JT808_0x0200_0x58;
            //Assert.Equal(0x03, jt808_0x0200_0x58.Direction);
            Assert.Equal(new List<ushort> { 645, 611, 624, 664, 666, 635, 646, 641 }, jt808_0x0200_0x58.Humiditys.ToArray());
        }
    }
}
