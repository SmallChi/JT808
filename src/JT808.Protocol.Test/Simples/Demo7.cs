using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.Simples
{
    public class Demo7
    {
        /// <summary>
        /// 单个
        /// </summary>
        [Fact]
        public void Test1()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1.AddJT808Configure(new DefaultConfig());
            var serviceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var config = serviceProvider1.GetRequiredService<IJT808Config>();
            var defaultConfig = (DefaultConfig)config;
            Assert.Equal("test", defaultConfig.ConfigId);
            Assert.Equal("smallchi", defaultConfig.Test());
        }

        /// <summary>
        /// 多个
        /// </summary>
        [Fact]
        public void Test2()
        {
            IServiceCollection serviceDescriptors2 = new ServiceCollection();
            serviceDescriptors2.AddJT808Configure(new Config1());
            serviceDescriptors2.AddJT808Configure(new Config2());
            serviceDescriptors2.AddSingleton(factory =>
            {
                Func<string, IJT808Config> accesor = key =>
                {
                    if (key.Equals("Config1"))
                    {
                        return factory.GetService<Config1>();
                    }
                    else if (key.Equals("Config2"))
                    {
                        return factory.GetService<Config2>();
                    }
                    else
                    {
                        throw new ArgumentException($"Not Support key : {key}");
                    }
                };
                return accesor;
            });
            var ServiceProvider2 = serviceDescriptors2.BuildServiceProvider();
            var config1 = ServiceProvider2.GetRequiredService<Func<string, IJT808Config>>()("Config1");
            Assert.Equal("Config1", config1.ConfigId);
            Assert.Equal("Config1", config1.GetSerializer().SerializerId);
            var config2 = ServiceProvider2.GetRequiredService<Func<string, IJT808Config>>()("Config2");
            Assert.Equal("Config2", config2.ConfigId);
            Assert.Equal("Config2", config2.GetSerializer().SerializerId);
        }

        public class DefaultConfig : GlobalConfigBase
        {
            public override string ConfigId => "test";

            public string Test()
            {
                return "smallchi";
            }
        }

        public class Config1 : GlobalConfigBase
        {
            public override string ConfigId => "Config1";
        }

        public class Config2 : GlobalConfigBase
        {
            public override string ConfigId => "Config2";
        }
    }
}
