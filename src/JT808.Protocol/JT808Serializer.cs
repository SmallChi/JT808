using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol
{
    public  class JT808Serializer
    {
        private readonly static JT808Package jT808Package = new JT808Package();

        public JT808Serializer(IJT808Config jT808Config)
        {
            this.jT808Config = jT808Config;
        }

        public JT808Serializer():this(new DefaultGlobalConfig())
        {

        }

        public string SerializerId => jT808Config.ConfigId;

        private readonly IJT808Config jT808Config;

        public byte[] Serialize(JT808Package package, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackWriter jT808MessagePackWriter = new JT808MessagePackWriter(buffer, version);
                jT808Package.Serialize(ref jT808MessagePackWriter, package, jT808Config);
                return jT808MessagePackWriter.FlushAndGetEncodingArray();
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }

        public ReadOnlySpan<byte> SerializeReadOnlySpan(JT808Package package, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                JT808MessagePackWriter jT808MessagePackWriter = new JT808MessagePackWriter(buffer, version);
                jT808Package.Serialize(ref jT808MessagePackWriter, package, jT808Config);
                return jT808MessagePackWriter.FlushAndGetEncodingReadOnlySpan();
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }

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

        private static bool CheckPackageType(Type type)
        {
            return type == typeof(JT808Package) || type == typeof(JT808HeaderPackage);
        }

        /// <summary>
        /// 用于负载或者分布式的时候，在网关只需要解到头部。
        /// 根据头部的消息Id进行分发处理，可以防止小部分性能损耗。
        /// </summary>
        /// <param name="bytes"></param>
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
        public dynamic Deserialize(ReadOnlySpan<byte> bytes, Type type, JT808Version version = JT808Version.JTT2013, int minBufferSize = 4096)
        {
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                var formatter = jT808Config.GetMessagePackFormatterByType(type);
                JT808MessagePackReader jT808MessagePackReader = new JT808MessagePackReader(bytes, version);
                if (CheckPackageType(type))
                    jT808MessagePackReader.Decode(buffer);
                return JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(formatter,ref jT808MessagePackReader, jT808Config);
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }
    }
}
