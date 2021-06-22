using JT808.Protocol.Extensions.YueBiao.MessageBody;
using JT808.Protocol.Extensions.YueBiao.Metadata;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.YueBiao.Test
{
    public class JT808_0x8103_0xF366_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x8103_0xF366_Test()
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure()
                                        .AddYueBiaoConfigure();
            IJT808Config jT808Config = serviceDescriptors.BuildServiceProvider().GetRequiredService<IJT808Config>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Serializer()
        {
            JT808_0x8103 jT808UploadLocationRequest = new JT808_0x8103
            {
                ParamList = new List<JT808_0x8103_BodyBase> {
                  new JT808_0x8103_0xF366{
                        HighTemperatureThreshold=1,
                        HighVoltageThreshold=2,
                        LowVoltageThreshold=3,
                        NormalFetalPressure=4,
                        Retain=new byte[]{1,2,3,4,5,6 },
                        SlowLeakageThreshold=5,
                        ThresholdUnbalancedTirePressure=6,
                        TimedReportingInterval=7,
                        TyrePressureUnit=8,
                        TyreSpecificationType="999999999999",
                        VoltageThreshold=10
                  }
               }
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("010000F366243939393939393939393939390008000400060005000300020001000A0007010203040506", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x8103>("010000F366243939393939393939393939390008000400060005000300020001000A0007010203040506".ToHexBytes());
            JT808_0x8103_0xF366 jT808_0x8103_0xF366 = jT808UploadLocationRequest.ParamList[0] as JT808_0x8103_0xF366;
            Assert.Equal(1, jT808_0x8103_0xF366.HighTemperatureThreshold);
            Assert.Equal(2, jT808_0x8103_0xF366.HighVoltageThreshold);
            Assert.Equal(3, jT808_0x8103_0xF366.LowVoltageThreshold);
            Assert.Equal(4, jT808_0x8103_0xF366.NormalFetalPressure);
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5, 6 }.ToHexString(), jT808_0x8103_0xF366.Retain.ToHexString());
            Assert.Equal(5, jT808_0x8103_0xF366.SlowLeakageThreshold);
            Assert.Equal(6, jT808_0x8103_0xF366.ThresholdUnbalancedTirePressure);
            Assert.Equal(7, jT808_0x8103_0xF366.TimedReportingInterval);
            Assert.Equal(8, jT808_0x8103_0xF366.TyrePressureUnit);
            Assert.Equal("999999999999", jT808_0x8103_0xF366.TyreSpecificationType);
            Assert.Equal(10, jT808_0x8103_0xF366.VoltageThreshold);
        }
        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x8103>("010000F366243939393939393939393939390008000400060005000300020001000A0007010203040506".ToHexBytes());
        }
    }
}
