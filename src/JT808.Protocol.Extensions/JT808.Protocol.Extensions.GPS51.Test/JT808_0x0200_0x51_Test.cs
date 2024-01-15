using JT808.Protocol.Extensions.GPS51.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.GPS51.Test
{
    public class JT808_0x0200_0x51_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200_0x51_Test()
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
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_GPS51_Constants.JT808_0x0200_0x51, new JT808_0x0200_0x51
            {
                AttachInfoId = 0x2b,
                AttachInfoLength = 4,
                 Temperatures = new List<short> {
                   12,-34
                  }
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C00001807151010102B04000C0022", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808Package>("7e020000470158666660580571000000000000000201d59c1c06a36599006e0000000021091719313901040000eb8830011d3101192a0200005201002b040000000051080134011a04f604f6eb06000400ce04f8847e".ToHexBytes());
            var body0200 = jT808UploadLocationRequest.Bodies as JT808_0x0200;
            body0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0x51, out var value);
            var jt808_0x0200_0x51 = value as JT808_0x0200_0x51;
            //2b049203a46f
            Assert.Equal(0x0134, jt808_0x0200_0x51.Temperatures[0]);
            Assert.Equal(0x011A, jt808_0x0200_0x51.Temperatures[1]);
            Assert.Equal(0x04F6, jt808_0x0200_0x51.Temperatures[2]);
            Assert.Equal(0x04F6, jt808_0x0200_0x51.Temperatures[3]);
            
        }
    }
}
