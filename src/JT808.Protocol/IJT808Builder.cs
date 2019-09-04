using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    public interface IJT808Builder
    {
        IJT808Config Config { get; }
        IServiceCollection Services { get; }
    }
}
