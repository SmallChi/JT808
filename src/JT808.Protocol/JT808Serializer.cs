using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol
{
    /// <summary>
    /// 
    ///  ref https://www.cnblogs.com/TianFang/p/9193881.html
    /// </summary>
    public static class JT808Serializer
    {
        public static byte[] Serialize(JT808Package jT808Package, int minBufferSize = 1024)
        {
            return Serialize<JT808Package>(jT808Package, minBufferSize);
        }

        public static JT808Package Deserialize(ReadOnlySpan<byte> bytes)
        {
            return Deserialize<JT808Package>(bytes);
        }

        public static byte[] Serialize<T>(T obj, int minBufferSize = 1024)
        {
            var formatter = JT808FormatterExtensions.GetFormatter<T>();
            byte[] buffer = JT808ArrayPool.Rent(minBufferSize);
            try
            {
                var len = formatter.Serialize(ref buffer, 0, obj);
                return buffer.AsSpan(0, len).ToArray();
            }
            finally
            {
                JT808ArrayPool.Return(buffer);
            }
        }

        public static T Deserialize<T>(ReadOnlySpan<byte> bytes)
        {
            var formatter = JT808FormatterExtensions.GetFormatter<T>();
            return formatter.Deserialize(bytes, out int readSize);
        }

        /// <summary>
        /// 用于负载或者分布式的时候，在网关只需要解到头部。
        /// 根据头部的消息Id进行分发处理，可以防止小部分性能损耗。
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static JT808HeaderPackage HeaderDeserialize(ReadOnlySpan<byte> bytes)
        {
            var formatter = JT808FormatterExtensions.GetFormatter<JT808HeaderPackage>();
            return formatter.Deserialize(bytes, out int readSize);
        }

        public static dynamic Deserialize(ReadOnlySpan<byte> bytes, Type type)
        {
            var formatter = JT808FormatterExtensions.GetFormatter(type);
            return JT808FormatterResolverExtensions.JT808DynamicDeserialize(formatter, bytes, out int readSize);
        }
    }
}
