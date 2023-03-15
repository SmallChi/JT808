using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Internal;
using JT808.Protocol.MessagePack;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

[assembly: InternalsVisibleTo("JT808.Protocol.DependencyInjection")]
namespace JT808.Protocol
{
    /// <summary>
    /// JT808序列化器
    /// </summary>
    public  class JT808Serializer
    {
        private readonly static JT808Package jT808Package = new JT808Package();

        private readonly static Type JT808_Header_Package_Type = typeof(JT808HeaderPackage);

        private readonly static Type JT808_Package_Type = typeof(JT808Package);

        /// <summary>
        /// 默认实例
        /// default instance
        /// </summary>
        public readonly static JT808Serializer Instance;

        static JT808Serializer()
        {
            Instance= new JT808Serializer();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jT808Config"></param>
        public JT808Serializer(IJT808Config jT808Config)
        {
            this.jT808Config = jT808Config;
        }

        /// <summary>
        /// 
        /// </summary>
        public JT808Serializer():this(new DefaultGlobalConfig())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string SerializerId => jT808Config.ConfigId;

        private readonly IJT808Config jT808Config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] Serialize(JT808Package package, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackWriter jT808MessagePackWriter = new JT808MessagePackWriter(buffer, version);
                package.Serialize(ref jT808MessagePackWriter, package, jT808Config);
                return jT808MessagePackWriter.FlushAndGetEncodingArray();
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public JT808Package Deserialize(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                jT808MessagePackReader.Decode(buffer);
                return jT808Package.Deserialize(ref jT808MessagePackReader, jT808Config);
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte [] Serialize<T>(T obj, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                var formatter = jT808Config.GetMessagePackFormatter<T>();
                JT808MessagePackWriter jT808MessagePackWriter = new JT808MessagePackWriter(buffer, version);
                formatter.Serialize(ref jT808MessagePackWriter, obj, jT808Config);
                return jT808MessagePackWriter.FlushAndGetEncodingArray();
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public T Deserialize<T>(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                if(CheckPackageType(typeof(T)))
                    jT808MessagePackReader.Decode(buffer);
                var formatter = jT808Config.GetMessagePackFormatter<T>();
                return formatter.Deserialize(ref jT808MessagePackReader, jT808Config);
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool CheckPackageType(Type type)
        {
            return type == JT808_Package_Type || type == JT808_Header_Package_Type;
        }

        /// <summary>
        /// 用于负载或者分布式的时候，在网关只需要解到头部。
        /// 根据头部的消息Id进行分发处理，可以防止小部分性能损耗。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public JT808HeaderPackage HeaderDeserialize(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                jT808MessagePackReader.Decode(buffer);
                return new JT808HeaderPackage(ref jT808MessagePackReader,jT808Config);
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public object Deserialize(ReadOnlySpan<byte> bytes, Type type, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                var formatter = jT808Config.GetMessagePackFormatterByType(type);
                JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                if (CheckPackageType(type))
                    jT808MessagePackReader.Decode(buffer);
                return formatter.Deserialize(ref jT808MessagePackReader, jT808Config);
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public string Analyze(ReadOnlySpan<byte> bytes,  JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                jT808MessagePackReader.Decode(buffer);
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                jT808Package.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                utf8JsonWriter.Flush();
                string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                return value;
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }       
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public string Analyze<T>(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                if (CheckPackageType(typeof(T)))
                    jT808MessagePackReader.Decode(buffer);
                var analyze = jT808Config.GetAnalyze<T>();
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteStartObject();
                analyze.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                return value;
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 用于分包组合
        /// </summary>
        /// <param name="msgid">对应消息id</param>
        /// <param name="bytes">组合的数据体</param>
        /// <param name="version">对应版本号</param>
        /// <param name="options">序列化选项</param>
        /// <param name="minBufferSize">默认65535</param>
        /// <returns></returns>
        public string Analyze(ushort msgid,ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default, int minBufferSize = 65535)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                if(jT808Config.MsgIdFactory.TryGetValue(msgid,out object msgHandle))
                {
                    if (msgHandle is IJT808MessagePackFormatter instance)
                    {
                        using MemoryStream memoryStream = new MemoryStream();
                        using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                        JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                        utf8JsonWriter.WriteStartObject();
                        instance.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                        utf8JsonWriter.WriteEndObject();
                        utf8JsonWriter.Flush();
                        string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                        return value;
                    }
                    return $"未找到对应的0x{msgid:X2}消息数据体类型";
                }
                return $"未找到对应的0x{msgid:X2}消息数据体类型";
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 用于分包组合
        /// </summary>
        /// <param name="msgid">对应消息id</param>
        /// <param name="bytes">组合的数据体</param>
        /// <param name="version">对应版本号</param>
        /// <param name="options">序列化选项</param>
        /// <param name="minBufferSize">默认65535</param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer(ushort msgid, ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default, int minBufferSize = 65535)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                if (jT808Config.MsgIdFactory.TryGetValue(msgid, out object msgHandle))
                {
                    if (msgHandle is IJT808MessagePackFormatter instance)
                    {
                        using MemoryStream memoryStream = new MemoryStream();
                        using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                        JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                        utf8JsonWriter.WriteStartObject();
                        instance.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                        utf8JsonWriter.WriteEndObject();
                        utf8JsonWriter.Flush();
                        return memoryStream.ToArray();
                    }
                    return Encoding.UTF8.GetBytes($"未找到对应的0x{msgid:X2}消息数据体类型");
                }
                return Encoding.UTF8.GetBytes($"未找到对应的0x{msgid:X2}消息数据体类型");
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                jT808MessagePackReader.Decode(buffer);
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                jT808Package.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                utf8JsonWriter.Flush();
                return memoryStream.ToArray();
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer<T>(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                if (CheckPackageType(typeof(T)))
                    jT808MessagePackReader.Decode(buffer);
                var analyze = jT808Config.GetAnalyze<T>();
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteStartObject();
                analyze.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                return memoryStream.ToArray();
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 外部注册
        /// </summary>
        /// <param name="externalAssemblies"></param>
        /// <returns></returns>
        public JT808Serializer Register(params Assembly[] externalAssemblies)
        {
            if(externalAssemblies!=null && externalAssemblies.Length > 0)
            {
                foreach(var asm in externalAssemblies)
                {
                    jT808Config.Register(asm);
                }
            }
            return this;
        }
    }
}
