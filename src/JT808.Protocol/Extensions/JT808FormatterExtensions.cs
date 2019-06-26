using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace JT808.Protocol.Extensions
{
    [Obsolete("使用JT808ConfigExtensions")]
    public static class JT808FormatterExtensions
    {
        public static IJT808Formatter<T> GetFormatter<T>(IJT808Config jT808Config)
        {
            return (IJT808Formatter<T>)GetFormatter(typeof(T), jT808Config); 
        }

        public static IJT808MessagePackFormatter<T> GetMessagePackFormatter<T>(IJT808Config jT808Config)
        {
            return (IJT808MessagePackFormatter<T>)GetFormatter(typeof(T), jT808Config);
        }

        public static object GetFormatter(Type type,IJT808Config  jT808Config)
        {
            if (!jT808Config.FormatterFactory.FormatterDict.TryGetValue(type.GUID, out var formatter))
            {
                throw new JT808Exception(JT808ErrorCode.NotGlobalRegisterFormatterAssembly, type.FullName);
            }
            return formatter;
        }
    }
}
