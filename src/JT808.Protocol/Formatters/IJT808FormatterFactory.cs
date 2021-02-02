using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Formatters
{
    /// <summary>
    /// 序列化工厂
    /// </summary>
    public interface IJT808FormatterFactory: IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<Guid,object> FormatterDict { get;}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TIJT808Formatter"></typeparam>
        /// <returns></returns>
        IJT808FormatterFactory SetMap<TIJT808Formatter>()
                    where TIJT808Formatter : IJT808Formatter;
    }
}
