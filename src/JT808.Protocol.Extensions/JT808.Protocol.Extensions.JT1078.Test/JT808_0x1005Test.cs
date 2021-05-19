using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public  class JT808_0x1005Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x1005Test()
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
            JT808_0x1005 jT808_0x1005 = new JT808_0x1005()
            {
                BeginTime=Convert.ToDateTime("2019-07-16 10:20:01"),
                EndTime= Convert.ToDateTime("2019-07-16 10:25:02"),
                GettingOffNumber=1,
                GettingOnNumber=1
            };
            var hex = JT808Serializer.Serialize(jT808_0x1005).ToHexString();
            Assert.Equal("19071610200119071610250200010001", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT808_0x1005 = JT808Serializer.Deserialize<JT808_0x1005>("19071610200119071610250200010001".ToHexBytes());
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:20:01"),jT808_0x1005.BeginTime);
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:25:02"),jT808_0x1005.EndTime);
            Assert.Equal(1, jT808_0x1005.GettingOffNumber);
            Assert.Equal(1, jT808_0x1005.GettingOnNumber);
        }
        [Fact]
        public void Test3()
        {
            var json = JT808Serializer.Analyze<JT808_0x1005>("19071610200119071610250200010001".ToHexBytes());
        }
    }
}
