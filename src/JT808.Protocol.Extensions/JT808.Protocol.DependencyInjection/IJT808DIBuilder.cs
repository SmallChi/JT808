using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JT808.Protocol.DependencyInjection
{
    /// <summary>
    /// JT808 DI Builder
    /// </summary>
    public interface IJT808DIBuilder: IJT808Builder
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        IServiceCollection Services { get; }
    }
}
