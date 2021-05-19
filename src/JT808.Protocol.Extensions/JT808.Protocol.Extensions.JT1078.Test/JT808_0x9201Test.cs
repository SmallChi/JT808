using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public class JT808_0x9201Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9201Test()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1
                            .AddJT808Configure()
                            .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<IJT808Config>();
            JT808Serializer = defaultConfig.GetSerializer();

            Newtonsoft.Json.JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                return new Newtonsoft.Json.JsonSerializerSettings
                {
                    DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss"
                };
            });
        }

        [Fact]
        public void Test1()
        {
            JT808_0x9201 jT808_0x9201 = new JT808_0x9201()
            {
                ChannelNo = 1,
                MediaType = 2,
                BeginTime = Convert.ToDateTime("2019-07-16 10:10:10"),
                EndTime = Convert.ToDateTime("2019-07-16 10:10:10"),
                PlaySpeed=3,
                MemoryType=5,
                PlaybackWay=6,
                ServerIp="127.0.0.1",
                ServerIpLength=9,
                StreamType=7,
                TcpPort=80,
                UdpPort=8080
            };
            var hex = JT808Serializer.Serialize(jT808_0x9201).ToHexString();
            Assert.Equal("093132372E302E302E3100501F90010207050603190716101010190716101010", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT808_0x9201 = JT808Serializer.Deserialize<JT808_0x9201>("093132372E302E302E3100501F90010207050603190716101010190716101010".ToHexBytes());
            Assert.Equal(1, jT808_0x9201.ChannelNo);
            Assert.Equal(2, jT808_0x9201.MediaType);
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:10:10"), jT808_0x9201.BeginTime);
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:10:10"), jT808_0x9201.EndTime);
            Assert.Equal(3, jT808_0x9201.PlaySpeed);
            Assert.Equal(5, jT808_0x9201.MemoryType);
            Assert.Equal(6, jT808_0x9201.PlaybackWay);
            Assert.Equal("127.0.0.1", jT808_0x9201.ServerIp);
            Assert.Equal(9, jT808_0x9201.ServerIpLength);
            Assert.Equal(7, jT808_0x9201.StreamType);
            Assert.Equal(80, jT808_0x9201.TcpPort);
            Assert.Equal(8080, jT808_0x9201.UdpPort);
        }
        [Fact]
        public void Test3()
        {
            var jT808_0x9201 = JT808Serializer.Analyze<JT808_0x9201>("093132372E302E302E3100501F90010207050603190716101010190716101010".ToHexBytes());
        }
    }
}