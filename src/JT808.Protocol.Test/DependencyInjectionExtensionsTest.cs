using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test
{
    public class DependencyInjectionExtensionsTest
    {
        [Fact]
        public void Test1()
        {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            //1
            serviceDescriptors.AddJT808Configure<DT1Config>()
                              .AddJT808Configure<DT2Config>();
            IServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
            //使用实例的方式获取
            IJT808Config DT1JT808Config = serviceProvider.GetRequiredService<DT1Config>();
            IJT808Config DT2JT808Config = serviceProvider.GetRequiredService<DT2Config>();
            Assert.Equal("DT1", DT1JT808Config.ConfigId);
            Assert.Equal("DT2", DT2JT808Config.ConfigId);
        }

        [Fact]
        public void Test2()
        {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure(new DT1Config())
                              .AddJT808Configure(new DT2Config());
            IServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
            //使用实例的方式获取
            IJT808Config DT1JT808Config = serviceProvider.GetRequiredService<DT1Config>();
            IJT808Config DT2JT808Config = serviceProvider.GetRequiredService<DT2Config>();
            Assert.Equal("DT1", DT1JT808Config.ConfigId);
            Assert.Equal("DT2", DT2JT808Config.ConfigId);
        }
        public class DT1Config : GlobalConfigBase
        {
            public override string ConfigId { get; protected set; } = "DT1";
        }
        public class DT2Config : GlobalConfigBase
        {
            public override string ConfigId { get; protected set; } = "DT2";
        }
    }
}
