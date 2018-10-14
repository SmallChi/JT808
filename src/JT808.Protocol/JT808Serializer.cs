using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using System;
using System.Buffers;
using System.Text;

namespace JT808.Protocol
{
    /// <summary>
    /// 
    ///  ref https://www.cnblogs.com/TianFang/p/9193881.html
    /// </summary>
    public static class JT808Serializer
    {
        public static byte[] Serialize(JT808Package jT808Package, int minBufferSize = 4096)
        {
            return Serialize<JT808Package>(jT808Package, minBufferSize);
        }

        public static JT808Package Deserialize(ReadOnlySpan<byte> bytes)
        {
            return Deserialize<JT808Package>(bytes);
        }

        public static byte[] Serialize<T>(T obj, int minBufferSize = 4096)
        {
            var formatter = JT808FormatterExtensions.GetFormatter<T>();
            var pool = MemoryPool<byte>.Shared;
            IMemoryOwner<byte> buffer = pool.Rent(minBufferSize);
            try
            {
                var len = formatter.Serialize(buffer, 0, obj);
                return buffer.Memory.Slice(0, len).ToArray();
            }
            finally
            {
                // 源码：System.Memory.MemoryPool 
                // private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
                // 单例内存池 不需要手动释放资源
                // buffer.Dispose() 相当于调用ArrayPool<T>.Shared.Return(array)
                buffer.Dispose();
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
