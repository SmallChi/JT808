using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    public static class DependencyInjectionExtensions
    {
        public static IJT808Builder AddJT808Configure(this IServiceCollection services, IJT808Config jT808Config)
        {
            services.AddSingleton(jT808Config.GetType(), jT808Config);
            return new DefaultBuilder(services, jT808Config);
        }

        public static IJT808Builder AddJT808Configure(this IJT808Builder builder, IJT808Config jT808Config)
        {
            builder.Services.AddSingleton(jT808Config.GetType(), jT808Config);
            return builder;
        }

        public static IJT808Builder AddJT808Configure<TJT808Config>(this IServiceCollection services)where TJT808Config : IJT808Config,new()
        {
            var config = new TJT808Config();
            services.AddSingleton(typeof(TJT808Config), config);
            return new DefaultBuilder(services, config);
        }

        public static IJT808Builder AddJT808Configure<TJT808Config>(this IJT808Builder builder) where TJT808Config : IJT808Config, new()
        {
            var config = new TJT808Config();
            builder.Services.AddSingleton(typeof(TJT808Config), config);
            return builder;
        }

        public static IJT808Builder AddJT808Configure(this IServiceCollection services)
        {
            DefaultGlobalConfig config = new DefaultGlobalConfig();
            services.AddSingleton<IJT808Config>(config);
            return new DefaultBuilder(services, config);
        }
    }
}
