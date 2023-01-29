using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Formatters
{
    /// <summary>
    /// 序列化工厂
    /// Serialization factory
    /// </summary>
    public interface IJT808FormatterFactory: IJT808ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<Guid, IJT808MessagePackFormatter> FormatterDict { get;}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT808MessagePackFormatter"></typeparam>
        /// <returns></returns>
        IJT808FormatterFactory SetMap<TJT808MessagePackFormatter>() where TJT808MessagePackFormatter : IJT808MessagePackFormatter;
    }
}
