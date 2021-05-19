using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Logging;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public class JT808_0x9102Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9102Test()
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
            JT808_0x9102 jT808_0X9102 = new JT808_0x9102();
            jT808_0X9102.ChannelNo = 1;
            jT808_0X9102.ControlCmd = 1;
            jT808_0X9102.CloseAVData = 0;
            jT808_0X9102.StreamType = 0;
            var hex = JT808Serializer.Serialize(jT808_0X9102).ToHexString();
            Assert.Equal("01010000", hex);
        }

        [Fact]
        public void Test2()
        {
            JT808_0x9102 jT808_0X9102 = JT808Serializer.Deserialize<JT808_0x9102>("01010000".ToHexBytes());
            Assert.Equal(1, jT808_0X9102.ChannelNo);
            Assert.Equal(1, jT808_0X9102.ControlCmd);
            Assert.Equal(0, jT808_0X9102.CloseAVData);
            Assert.Equal(0, jT808_0X9102.StreamType);
        }

        [Fact]
        public void Test3()
        {
            JT808Package jT808Package = new JT808Package();
            JT808Header header = new JT808Header();
            header.MsgId = 0x9102;
            header.ManualMsgNum = 1;
            header.TerminalPhoneNo = "12345679810";
            jT808Package.Header = header;
            JT808_0x9102 jT808_0X9102 = new JT808_0x9102();
            jT808_0X9102.ChannelNo = 1;
            jT808_0X9102.ControlCmd = 1;
            jT808_0X9102.CloseAVData = 0;
            jT808_0X9102.StreamType = 0;
            jT808Package.Bodies = jT808_0X9102;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            //7E910200040123456798100001010100001E7E
            Assert.Equal("7E910200040123456798100001010100001E7E", hex);
        }
        [Fact]
        public void Test4()
        {
            var jT808_0rX9102 = JT808Serializer.Analyze<JT808_0x9102>("01010000".ToHexBytes());
        }
    }
}
