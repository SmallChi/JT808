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
    public class JT808_0x9206Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9206Test()
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
            JT808_0x9206 jT808_0x9206 = new JT808_0x9206()
            {
                AlarmFlag=1,
                MediaType=2,
                BeginTime= Convert.ToDateTime("2019-07-16 10:10:10"),
                EndTime= Convert.ToDateTime("2019-07-16 10:10:11"),
                ChannelNo=3,   
                StreamType =5,
                FileUploadPath ="D://1112",
                FileUploadPathLength=8,
                MemoryPositon=4,
                Password="123456",
                PasswordLength=6,
                Port=808,
                ServerIp="127.0.0.1",
                ServerIpLength=9,
                TaskExcuteCondition=7, 
                UserName="tk",
                UserNameLength=2
            };
            var hex = JT808Serializer.Serialize(jT808_0x9206).ToHexString();
            Assert.Equal("093132372E302E302E31032802746B0631323334353608443A2F2F3131313203190716101010190716101011000000000000000102050407", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT808_0x9206 = JT808Serializer.Deserialize<JT808_0x9206>("093132372E302E302E31032802746B0631323334353608443A2F2F3131313203190716101010190716101011000000000000000102050407".ToHexBytes());
            Assert.Equal(1u, jT808_0x9206.AlarmFlag);
            Assert.Equal(2, jT808_0x9206.MediaType);
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:10:10"), jT808_0x9206.BeginTime);
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:10:11"), jT808_0x9206.EndTime);
            Assert.Equal(3, jT808_0x9206.ChannelNo);
            Assert.Equal(5, jT808_0x9206.StreamType);
            Assert.Equal("D://1112", jT808_0x9206.FileUploadPath);
            Assert.Equal(8, jT808_0x9206.FileUploadPathLength);
            Assert.Equal(4, jT808_0x9206.MemoryPositon);
            Assert.Equal("123456", jT808_0x9206.Password);
            Assert.Equal(6, jT808_0x9206.PasswordLength);
            Assert.Equal(808, jT808_0x9206.Port);
            Assert.Equal("127.0.0.1", jT808_0x9206.ServerIp);
            Assert.Equal(9, jT808_0x9206.ServerIpLength);
            Assert.Equal(7, jT808_0x9206.TaskExcuteCondition);
            Assert.Equal("tk", jT808_0x9206.UserName);
            Assert.Equal(2, jT808_0x9206.UserNameLength);
        }
        [Fact]
        public void Test3()
        {
            var jT808_0x9206 = JT808Serializer.Analyze<JT808_0x9206>("093132372E302E302E31032802746B0631323334353608443A2F2F3131313203190716101010190716101011000000000000000102050407".ToHexBytes());
        }
    }
}