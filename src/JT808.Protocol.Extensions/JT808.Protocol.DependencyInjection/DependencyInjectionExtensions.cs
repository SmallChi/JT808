using JT808.Protocol.DependencyInjection;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808 DI扩展
    /// JT808 DependencyInjectionExtensions
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 注册808配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jT808Config"></param>
        /// <returns></returns>
        public static IJT808DIBuilder AddJT808Configure(this IServiceCollection services, IJT808Config jT808Config)
        {
            services.AddSingleton(jT808Config.GetType(), jT808Config);
            return new DefaultDIBuilder(services,jT808Config);
        }
        /// <summary>
        /// 注册808配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="jT808Config"></param>
        /// <returns></returns>
        public static IJT808DIBuilder AddJT808Configure(this IJT808DIBuilder builder, IJT808Config jT808Config)
        {
            builder.Services.AddSingleton(jT808Config.GetType(), jT808Config);
            return builder;
        }
        /// <summary>
        /// 注册808配置
        /// </summary>
        /// <typeparam name="TJT808Config"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IJT808DIBuilder AddJT808Configure<TJT808Config>(this IServiceCollection services)where TJT808Config : IJT808Config,new()
        {
            var config = new TJT808Config();
            services.AddSingleton(typeof(TJT808Config), config);
            return new DefaultDIBuilder(services, config);
        }
        /// <summary>
        /// 注册808配置
        /// </summary>
        /// <typeparam name="TJT808Config"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IJT808DIBuilder AddJT808Configure<TJT808Config>(this IJT808DIBuilder builder) where TJT808Config : IJT808Config, new()
        {
            var config = new TJT808Config();
            builder.Services.AddSingleton(typeof(TJT808Config), config);
            return builder;
        }
        /// <summary>
        /// 注册808配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IJT808DIBuilder AddJT808Configure(this IServiceCollection services)
        {
            DefaultGlobalConfig config = new DefaultGlobalConfig();
            services.AddSingleton<IJT808Config>(config);
            return new DefaultDIBuilder(services,config);
        }
    }
}
