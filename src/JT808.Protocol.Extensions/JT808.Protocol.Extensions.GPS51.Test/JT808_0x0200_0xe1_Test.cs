using JT808.Protocol.Extensions.GPS51.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.GPS51.Test
{
    public class JT808_0x0200_0xe1_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200_0xe1_Test()
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
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_GPS51_Constants.JT808_0x0200_0xe1, new JT808_0x0200_0xe1
            {
                AttachInfoId = 0xe1,
                AttachInfoLength = 1,
                MCC = 1,
                MNC = 2,
                BaseStations = new List<BaseStation> { 
                        new BaseStation {
                            LAC=3,
                            CI=4,
                            Signal=5,
                       }
                  }
            });
            var hex = JT808Serializer.Serialize<JT808_0x0200>(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C0000180715101010E10A00010002000300000004", hex);
        }

        [Fact]
        public void Deserialize()
        {
            var jt808_0x0200 = JT808Serializer.Deserialize<JT808_0x0200>("000000010000000200BA7F0E07E4F11C0028003C0000180715101010E10A00010002000300000004".ToHexBytes());
            jt808_0x0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0xe1, out var value);
            var jt808_0x0200_0xe1 = value as JT808_0x0200_0xe1; 
            Assert.Equal(1, jt808_0x0200_0xe1.MCC);
            Assert.Equal(2, jt808_0x0200_0xe1.MNC);
            Assert.Equal(3, jt808_0x0200_0xe1.BaseStations[0].LAC);
            Assert.Equal<uint>(4, jt808_0x0200_0xe1.BaseStations[0].CI);
            if (jt808_0x0200_0xe1.BaseStations.Count != 1) {
                Assert.Equal(5, jt808_0x0200_0xe1.BaseStations[0].Signal);
            }
        }
        [Fact]
        public void Deserialize1()
        {
            //gps51 demo
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808Package>("7e020000440186070970130dfe0000000000040002019279fa06d5f408001a00000000210917084332e10a01cc0000696a0863a8d0e41701000000000000810e3c756e6b6e6f776e20737369643e3101393f7e".ToHexBytes());
            var body0200 = jT808UploadLocationRequest.Bodies as JT808_0x0200;
            body0200.CustomLocationAttachData.TryGetValue(JT808_GPS51_Constants.JT808_0x0200_0xe1 ,out var value);
            var jt808_0x0200_0xe1= value as JT808_0x0200_0xe1;
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jt808_0x0200_0xe1), "{\"AttachInfoId\":225,\"AttachInfoLength\":10,\"MCC\":460,\"MNC\":0,\"BaseStations\":[{\"LAC\":26986,\"CI\":140749008,\"Signal\":0}]}");
        }
    }
}
