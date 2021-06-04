using JT808.Protocol.Extensions.SuBiao.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.SuBiao.Test
{
    public class JT808_0x8103_0xF367_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x8103_0xF367_Test()
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
            JT808_0x8103 jT808UploadLocationRequest = new JT808_0x8103
            {                 
                ParamList=new List<JT808_0x8103_BodyBase> {
                  new JT808_0x8103_0xF367{
                     LateralRearApproachAlarmTimeThreshold=1,
                     RearApproachAlarmTimeThreshold=2
                  }
               }
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("010000F367020201", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x8103>("010000F367020201".ToHexBytes());
            JT808_0x8103_0xF367 jT808_0x8103_0xF367 = jT808UploadLocationRequest.ParamList[0] as JT808_0x8103_0xF367;
            Assert.Equal(1, jT808_0x8103_0xF367.LateralRearApproachAlarmTimeThreshold);
            Assert.Equal(2, jT808_0x8103_0xF367.RearApproachAlarmTimeThreshold);
        }
        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x8103>("010000F367020201".ToHexBytes());
        }
    }
}
