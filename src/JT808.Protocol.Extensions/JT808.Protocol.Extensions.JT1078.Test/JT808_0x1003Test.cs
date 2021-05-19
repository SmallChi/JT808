using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public  class JT808_0x1003Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x1003Test()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1
                            .AddJT808Configure()
                            .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<IJT808Config>();
            JT808Serializer = defaultConfig.GetSerializer();
        }

        [Fact]
        public void Test1()
        {
            JT808_0x1003 jT808_0x1003 = new JT808_0x1003()
            {
                AudioFrameLength = 1,
                EnterAudioChannelsNumber = 2,
                EnterAudioEncoding = 3,
                EnterAudioSampleDigits = 4,
                EnterAudioSampleRate = 5,
                IsSupportedAudioOutput = 1,
                VideoEncoding = 6,
                TerminalSupportedMaxNumberOfAudioPhysicalChannels = 7,
                TerminalSupportedMaxNumberOfVideoPhysicalChannels = 8
            };
            var hex = JT808Serializer.Serialize(jT808_0x1003).ToHexString();
            Assert.Equal("03020504000101060708", hex);
        }

        [Fact]
        public void Test2()
        {
            JT808_0x1003 jT808_0x1003 = JT808Serializer.Deserialize<JT808_0x1003>("03020504000101060708".ToHexBytes());
            Assert.Equal(1,jT808_0x1003.AudioFrameLength);
            Assert.Equal(2, jT808_0x1003.EnterAudioChannelsNumber);
            Assert.Equal(3, jT808_0x1003.EnterAudioEncoding);
            Assert.Equal(4, jT808_0x1003.EnterAudioSampleDigits);
            Assert.Equal(5, jT808_0x1003.EnterAudioSampleRate);
            Assert.Equal(1, jT808_0x1003.IsSupportedAudioOutput);
            Assert.Equal(6, jT808_0x1003.VideoEncoding);
            Assert.Equal(7, jT808_0x1003.TerminalSupportedMaxNumberOfAudioPhysicalChannels);
            Assert.Equal(8, jT808_0x1003.TerminalSupportedMaxNumberOfVideoPhysicalChannels);
        }
        [Fact]
        public void Test3()
        {
            var json = JT808Serializer.Analyze<JT808_0x1003>("03020504000101060708".ToHexBytes());
        }
    }
}
