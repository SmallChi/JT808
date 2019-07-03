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
            services.AddSingleton(jT808Config);
            return new DefaultBuilder(services, jT808Config);
        }

        public static IJT808Builder AddJT808Configure(this IServiceCollection services)
        {
            DefaultGlobalConfig config = new DefaultGlobalConfig();
            services.AddSingleton<IJT808Config>(config);
            return new DefaultBuilder(services, config);
        }
    }
}
