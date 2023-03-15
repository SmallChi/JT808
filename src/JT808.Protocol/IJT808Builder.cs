using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808构造器
    /// </summary>
    public interface IJT808Builder
    {
        /// <summary>
        /// JT808配置
        /// </summary>
        IJT808Config Config { get; }
    }
}
