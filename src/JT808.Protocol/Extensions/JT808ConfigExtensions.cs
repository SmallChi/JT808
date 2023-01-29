using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Concurrent;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808配置扩展
    /// </summary>
    public static class JT808ConfigExtensions
    {
        private readonly static ConcurrentDictionary<string, JT808Serializer> jT808SerializerDict = new ConcurrentDictionary<string, JT808Serializer>(StringComparer.OrdinalIgnoreCase);
        private readonly static ConcurrentDictionary<string, JT808CarDVRSerializer> jT808CarDVRSerializer = new ConcurrentDictionary<string, JT808CarDVRSerializer>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 通过类型获取对应的消息序列化器
        /// </summary>
        /// <param name="jT808Config"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IJT808MessagePackFormatter GetMessagePackFormatterByType(this IJT808Config jT808Config,Type type)
        {
            if (!jT808Config.FormatterFactory.FormatterDict.TryGetValue(type.GUID, out var formatter))
            {
                throw new JT808Exception(JT808ErrorCode.NotGlobalRegisterFormatterAssembly, type.FullName);
            }
            return formatter;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T Cast<T>(this object instance) where T : class
        {
            if (instance == null) { return default(T); }
            return instance as T;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="instance"></param>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static TValue DeserializeExt<TValue>(this object instance, ref JT808MessagePackReader reader, IJT808Config config) 
            where TValue : class
        {
            if (instance == null) { return default(TValue); }
            var formatter= instance as IJT808MessagePackFormatter;
            if (formatter != null)
            {
                return formatter.Deserialize(ref reader, config) as TValue;
            }
            return default(TValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static void SerializeExt(this object instance, ref JT808MessagePackWriter writer, object value , IJT808Config config)
        {
            if (instance == null) { return; }
            var formatter = instance as IJT808MessagePackFormatter;
            if (formatter != null)
            {
                 formatter.Serialize(ref writer, value, config);
            }
        }

        /// <summary>
        /// 通过类型获取对应的消息分析器
        /// </summary>
        /// <param name="jT808Config"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetAnalyzeByType(this IJT808Config jT808Config, Type type)
        {
            if (!jT808Config.FormatterFactory.FormatterDict.TryGetValue(type.GUID, out var analyze))
            {
                throw new JT808Exception(JT808ErrorCode.NotGlobalRegisterFormatterAssembly, type.FullName);
            }
            return analyze;
        }
        /// <summary>
        /// 获取对应的消息序列化器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jT808Config"></param>
        /// <returns></returns>
        public static IJT808MessagePackFormatter<T> GetMessagePackFormatter<T>(this IJT808Config jT808Config)
        {
            return (IJT808MessagePackFormatter<T>)GetMessagePackFormatterByType(jT808Config,typeof(T));
        }
        /// <summary>
        /// 获取对应的消息分析器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jT808Config"></param>
        /// <returns></returns>
        public static IJT808Analyze GetAnalyze<T>(this IJT808Config jT808Config)
        {
            return (IJT808Analyze)GetAnalyzeByType(jT808Config, typeof(T));
        }
        /// <summary>
        /// 获取JT19056序列化器
        /// </summary>
        /// <param name="jT808Config"></param>
        /// <returns></returns>
        public static JT808CarDVRSerializer GetCarDVRSerializer(this IJT808Config jT808Config)
        {
            if(!jT808CarDVRSerializer.TryGetValue(jT808Config.ConfigId,out var serializer))
            {
                serializer = new JT808CarDVRSerializer(jT808Config);
                jT808CarDVRSerializer.TryAdd(jT808Config.ConfigId, serializer);
            }
            return serializer;
        }
        /// <summary>
        /// 获取JT808序列化器
        /// </summary>
        /// <param name="jT808Config"></param>
        /// <returns></returns>
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
