using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JT808.Protocol.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddJT808Configure(this IServiceCollection services, IJT808Config jT808Config)
        {
            services.AddSingleton(jT808Config.GetType(), jT808Config);
            services.AddSingleton(jT808Config);
            return services;
        }

        public static IServiceCollection AddJT808Configure(this IServiceCollection services)
        {
            services.AddSingleton<IJT808Config>(new DefaultGlobalConfig());
            return services;
        }

        class DefaultGlobalConfig : GlobalConfigBase
        {
            public override string ConfigId => "default";
        }
    }
}
