using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.DependencyInjection
{
    /// <summary>
    /// 默认JT808构造器
    /// </summary>
    class DefaultDIBuilder : IJT808DIBuilder
    {
        /// <summary>
        /// JT808配置
        /// </summary>
        public IJT808Config Config { get; }
        /// <summary>
        /// DI ServiceCollection
        /// </summary>
        public IServiceCollection Services { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public DefaultDIBuilder(IServiceCollection services,IJT808Config config)
        {
            Config = config;
            Services = services;
        }
    }
}
