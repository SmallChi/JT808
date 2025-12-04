using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

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
        public static IJT808Builder AddJT808Configure(this IServiceCollection services, IJT808Config jT808Config)
        {
            services.AddSingleton(jT808Config.GetType(), jT808Config);
            return new DefaultBuilder(services, jT808Config);
        }
        /// <summary>
        /// 注册808配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="jT808Config"></param>
        /// <returns></returns>
        public static IJT808Builder AddJT808Configure(this IJT808Builder builder, IJT808Config jT808Config)
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
        public static IJT808Builder AddJT808Configure<TJT808Config>(this IServiceCollection services, Action<TJT808Config> options = null) where TJT808Config : IJT808Config, new()
        {
            var config = new TJT808Config();
            options?.Invoke(config);
            services.AddSingleton(typeof(TJT808Config), config);
            return new DefaultBuilder(services, config);
        }
        /// <summary>
        /// 注册808配置
        /// </summary>
        /// <typeparam name="TJT808Config"></typeparam>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IJT808Builder AddJT808Configure<TJT808Config>(this IJT808Builder builder, Action<TJT808Config> options = null) where TJT808Config : IJT808Config, new()
        {
            var config = new TJT808Config();
            options?.Invoke(config);
            builder.Services.AddSingleton(typeof(TJT808Config), config);
            return builder;
        }
        /// <summary>
        /// 注册808配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IJT808Builder AddJT808Configure(this IServiceCollection services, Action<IJT808Config> options = null)
        {
            DefaultGlobalConfig config = new DefaultGlobalConfig();
            options?.Invoke(config);
            services.AddSingleton<IJT808Config>(config);
            return new DefaultBuilder(services, config);
        }
    }
}
