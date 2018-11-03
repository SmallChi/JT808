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
            int readSize;
            return formatter.Deserialize(bytes,out readSize);
        }
    }
}
