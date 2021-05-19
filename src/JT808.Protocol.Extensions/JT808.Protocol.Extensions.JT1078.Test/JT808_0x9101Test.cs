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
    public  class JT808_0x9101Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9101Test()
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
            JT808_0x9101 jT808_0X9101 = new JT808_0x9101();
            jT808_0X9101.ServerIp = "127.0.0.1";
            jT808_0X9101.TcpPort = 1888;
            jT808_0X9101.UdpPort = 0;
            jT808_0X9101.ChannelNo= 1;
            jT808_0X9101.DataType= 1;
            jT808_0X9101.StreamType= 1;
            var hex = JT808Serializer.Serialize(jT808_0X9101).ToHexString();
            Assert.Equal("093132372E302E302E3107600000010101", hex);
        }

        [Fact]
        public void Test2()
        {
            JT808_0x9101 jT808_0X9101= JT808Serializer.Deserialize<JT808_0x9101>("093132372E302E302E3107600000010101".ToHexBytes());
            Assert.Equal("127.0.0.1", jT808_0X9101.ServerIp);
            Assert.Equal(9, jT808_0X9101.ServerIpLength);
            Assert.Equal(1888, jT808_0X9101.TcpPort);
            Assert.Equal(0, jT808_0X9101.UdpPort);
            Assert.Equal(1, jT808_0X9101.ChannelNo);
            Assert.Equal(1, jT808_0X9101.DataType);
            Assert.Equal(1, jT808_0X9101.StreamType);
        }

        [Fact]
        public void Test3()
        {
            JT808Package jT808Package = new JT808Package();
            JT808Header header = new JT808Header();
            header.MsgId = 0x9101;
            header.MsgNum = 1;
            header.TerminalPhoneNo = "12345679810";
            jT808Package.Header = header;
            JT808_0x9101 jT808_0X9101 = new JT808_0x9101();
            jT808_0X9101.ServerIp = "127.0.0.1";
            jT808_0X9101.TcpPort = 1888;
            jT808_0X9101.UdpPort = 0;
            jT808_0X9101.ChannelNo = 1;
            jT808_0X9101.DataType = 1;
            jT808_0X9101.StreamType = 1;
            jT808Package.Bodies = jT808_0X9101;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E910100110123456798100001093132372E302E302E31076000000101014C7E", hex);
            //7E910100110123456798100001093132372E302E302E31076000000101014C7E
        }

        [Fact]
        public void Test4()
        {
            var jT808_0X9101 = JT808Serializer.Analyze<JT808_0x9101>("093132372E302E302E3107600000010101".ToHexBytes());
        }

        [Fact]
        public void Test5()
        {
            JT808Package jT808Package = new JT808Package();
            JT808Header header = new JT808Header();
            header.MsgId = 0x9101;
            header.MsgNum = 1;
            header.TerminalPhoneNo = "";
            jT808Package.Header = header;
            JT808_0x9101 jT808_0X9101 = new JT808_0x9101();
            jT808_0X9101.ServerIp = "";
            jT808_0X9101.TcpPort = 1078;
            jT808_0X9101.UdpPort = 0;
            jT808_0X9101.ChannelNo = 3;
            jT808_0X9101.DataType = 1;
            jT808_0X9101.StreamType = 1;
            jT808Package.Bodies = jT808_0X9101;
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
        }
    }
}
