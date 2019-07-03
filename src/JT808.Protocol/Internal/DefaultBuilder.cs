using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Internal
{
    class DefaultBuilder : IJT808Builder
    {
        public IServiceCollection Services { get; }

        public IJT808Config Config { get; }

        public DefaultBuilder(IServiceCollection services, IJT808Config config)
        {
            Services = services;
            Config = config;
        }
    }
}
