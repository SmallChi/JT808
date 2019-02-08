using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.JT808Formatters;
using System;
using System.Reflection;

namespace JT808.Protocol.Extensions
{
    public static class JT808FormatterExtensions
    {
        public static IJT808Formatter<T> GetFormatter<T>()
        {
            IJT808Formatter<T> formatter;
            var attr = typeof(T).GetTypeInfo().GetCustomAttribute<JT808FormatterAttribute>();
            if (attr == null)
            {
                return default;
            }
            if (attr.Arguments == null)
            {
                formatter = (IJT808Formatter<T>)Activator.CreateInstance(attr.FormatterType);
            }
            else
            {
                formatter = (IJT808Formatter<T>)Activator.CreateInstance(attr.FormatterType, attr.Arguments);
            }
            return formatter;
        }

        public static object GetFormatter(Type formatterType)
        {
            object formatter;
            var attr = formatterType.GetTypeInfo().GetCustomAttribute<JT808FormatterAttribute>();
            if (attr == null)
            {
                throw new JT808Exception(JT808ErrorCode.GetFormatterAttributeError, formatterType.FullName);
            }
            if (attr.Arguments == null)
            {
                formatter = Activator.CreateInstance(attr.FormatterType);
            }
            else
            {
                formatter = Activator.CreateInstance(attr.FormatterType, attr.Arguments);
            }
            return formatter;
        }
    }
}
