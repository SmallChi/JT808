using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Internal;
using JT808.Protocol.MessagePack;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol
{
    /// <summary>
    /// JT19056序列化器
    /// </summary>
    public  class JT808CarDVRSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly static JT808CarDVRUpPackage JT808CarDVRUpPackage = new JT808CarDVRUpPackage();
        /// <summary>
        /// 
        /// </summary>

        public readonly static JT808CarDVRDownPackage JT808CarDVRDownPackage = new JT808CarDVRDownPackage();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jT808Config"></param>
        public JT808CarDVRSerializer(IJT808Config jT808Config)
        {
            this.jT808Config = jT808Config;
        }

        /// <summary>
        /// 
        /// </summary>
        public JT808CarDVRSerializer():this(new DefaultGlobalConfig())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string SerializerId => jT808Config.ConfigId;
        /// <summary>
        /// 
        /// </summary>

        private readonly IJT808Config jT808Config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] Serialize(JT808CarDVRUpPackage package, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
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
        /// <param name="package"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> SerializeReadOnlySpan(JT808CarDVRUpPackage package, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackWriter jT808MessagePackWriter = new JT808MessagePackWriter(buffer, version);
                package.Serialize(ref jT808MessagePackWriter, package, jT808Config);
                return jT808MessagePackWriter.FlushAndGetEncodingReadOnlySpan();
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] Serialize(JT808CarDVRDownPackage package, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
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
        /// <param name="package"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> SerializeReadOnlySpan(JT808CarDVRDownPackage package, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackWriter jT808MessagePackWriter = new JT808MessagePackWriter(buffer, version);
                package.Serialize(ref jT808MessagePackWriter, package, jT808Config);
                return jT808MessagePackWriter.FlushAndGetEncodingReadOnlySpan();
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
        /// <returns></returns>
        public JT808CarDVRUpPackage UpDeserialize(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013)
        {
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            return JT808CarDVRUpPackage.Deserialize(ref jT808MessagePackReader, jT808Config);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public JT808CarDVRDownPackage DownDeserialize(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013)
        {
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            return JT808CarDVRDownPackage.Deserialize(ref jT808MessagePackReader, jT808Config);
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
        /// <param name="obj"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> SerializeReadOnlySpan<T>(T obj, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                var formatter = jT808Config.GetMessagePackFormatter<T>();
                JT808MessagePackWriter jT808MessagePackWriter = new JT808MessagePackWriter(buffer, version);
                formatter.Serialize(ref jT808MessagePackWriter, obj, jT808Config);
                return jT808MessagePackWriter.FlushAndGetEncodingReadOnlySpan();
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
        /// <returns></returns>
        public T Deserialize<T>(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013)
        {
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            var formatter = jT808Config.GetMessagePackFormatter<T>();
            return formatter.Deserialize(ref jT808MessagePackReader, jT808Config);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public dynamic Deserialize(ReadOnlySpan<byte> bytes, Type type, JT808Version version = JT808Version.JTT2013)
        {
            var formatter = jT808Config.GetMessagePackFormatterByType(type);
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            return formatter.Deserialize(ref jT808MessagePackReader, jT808Config);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string UpAnalyze(ReadOnlySpan<byte> bytes,  JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default)
        {
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            using(MemoryStream memoryStream = new MemoryStream())
            using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                utf8JsonWriter.WriteStartObject();
                JT808CarDVRUpPackage.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                return value; 
            }   
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string DownAnalyze(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default)
        {
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            using (MemoryStream memoryStream = new MemoryStream())
            using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                utf8JsonWriter.WriteStartObject();
                JT808CarDVRDownPackage.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                return value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string Analyze<T>(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default)
        {
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            var analyze = jT808Config.GetAnalyze<T>();
            using (MemoryStream memoryStream = new MemoryStream())
            using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                utf8JsonWriter.WriteStartObject();
                analyze.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                return value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public byte[] UpAnalyzeJsonBuffer(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default)
        {
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            using (MemoryStream memoryStream = new MemoryStream())
            using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                utf8JsonWriter.WriteStartObject();
                JT808CarDVRUpPackage.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                return memoryStream.ToArray();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public byte[] DownAnalyzeJsonBuffer(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default)
        {
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            using (MemoryStream memoryStream = new MemoryStream())
            using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                utf8JsonWriter.WriteStartObject();
                JT808CarDVRDownPackage.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                return memoryStream.ToArray();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer<T>(ReadOnlySpan<byte> bytes, JT808Version version = JT808Version.JTT2013, JsonWriterOptions options = default)
        {
            JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
            var analyze = jT808Config.GetAnalyze<T>();
            using (MemoryStream memoryStream = new MemoryStream())
            using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                utf8JsonWriter.WriteStartObject();
                analyze.Analyze(ref jT808MessagePackReader, utf8JsonWriter, jT808Config);
                utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                return memoryStream.ToArray();
            }
        }
    }
}
