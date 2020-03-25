using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Concurrent;

namespace JT808.Protocol
{
    public static class JT808ConfigExtensions
    {
        private readonly static ConcurrentDictionary<string, JT808Serializer> jT808SerializerDict = new ConcurrentDictionary<string, JT808Serializer>(StringComparer.OrdinalIgnoreCase);
        private readonly static ConcurrentDictionary<string, JT808CarDVRSerializer> jT808CarDVRSerializer = new ConcurrentDictionary<string, JT808CarDVRSerializer>(StringComparer.OrdinalIgnoreCase);
        public static object GetMessagePackFormatterByType(this IJT808Config jT808Config,Type type)
        {
            if (!jT808Config.FormatterFactory.FormatterDict.TryGetValue(type.GUID, out var formatter))
            {
                throw new JT808Exception(JT808ErrorCode.NotGlobalRegisterFormatterAssembly, type.FullName);
            }
            return formatter;
        }
        public static object GetAnalyzeByType(this IJT808Config jT808Config, Type type)
        {
            if (!jT808Config.FormatterFactory.FormatterDict.TryGetValue(type.GUID, out var analyze))
            {
                throw new JT808Exception(JT808ErrorCode.NotGlobalRegisterFormatterAssembly, type.FullName);
            }
            return analyze;
        }
        public static IJT808MessagePackFormatter<T> GetMessagePackFormatter<T>(this IJT808Config jT808Config)
        {
            return (IJT808MessagePackFormatter<T>)GetMessagePackFormatterByType(jT808Config,typeof(T));
        }
        public static IJT808Analyze GetAnalyze<T>(this IJT808Config jT808Config)
        {
            return (IJT808Analyze)GetAnalyzeByType(jT808Config, typeof(T));
        }
        public static JT808CarDVRSerializer GetCarDVRSerializer(this IJT808Config jT808Config)
        {
            if(!jT808CarDVRSerializer.TryGetValue(jT808Config.ConfigId,out var serializer))
            {
                serializer = new JT808CarDVRSerializer(jT808Config);
                jT808CarDVRSerializer.TryAdd(jT808Config.ConfigId, serializer);
            }
            return serializer;
        }       
        public static JT808Serializer GetSerializer(this IJT808Config jT808Config)
        {
            if(!jT808SerializerDict.TryGetValue(jT808Config.ConfigId,out var serializer))
            {
                serializer = new JT808Serializer(jT808Config);
                jT808SerializerDict.TryAdd(jT808Config.ConfigId, serializer);
            }
            return serializer;
        }
    }
}
