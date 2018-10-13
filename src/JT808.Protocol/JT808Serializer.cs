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
        public static byte[] Serialize(JT808Package jT808Package)
        {
            var formatter = JT808FormatterExtensions.GetFormatter<JT808Package>();
            var pool = MemoryPool<byte>.Shared;
            IMemoryOwner<byte> buffer = pool.Rent(4096);
            try
            {
                var len = formatter.Serialize(buffer, 0, jT808Package);
                return buffer.Memory.Slice(0, len).ToArray();
            }
            catch (JT808Exception ex)
            {
                throw new JT808Exception("Serialize", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Serialize", ex);
            }
            finally
            {
                // 源码：System.Memory.MemoryPool 
                // private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
                // 单例内存池 不需要手动释放资源
                 buffer.Dispose();
            }
        }

        public static JT808Package Deserialize(ReadOnlySpan<byte> bytes)
        {
            try
            {
                var formatter = JT808FormatterExtensions.GetFormatter<JT808Package>();
                int readSize;
                return formatter.Deserialize(bytes,out readSize);
            }
            catch (Exception ex)
            {
                throw new Exception("Deserialize", ex);
            }
        }

        public static byte[] Serialize<T>(T obj)
        {
            var formatter = JT808FormatterExtensions.GetFormatter<T>();
            var pool = MemoryPool<byte>.Shared;
            IMemoryOwner<byte> buffer = pool.Rent(10240);
            try
            {
                var len = formatter.Serialize(buffer, 0, obj);
                return buffer.Memory.Slice(0, len).ToArray();
            }
            catch (JT808Exception ex)
            {
                throw new JT808Exception("Serialize", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Serialize", ex);
            }
            finally
            {
                // 源码：System.Memory.MemoryPool 
                // private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
                // 单例内存池 不需要手动释放资源
                buffer.Dispose();
            }
        }

        public static T Deserialize<T>(ReadOnlySpan<byte> bytes)
        {
            try
            {
                var formatter = JT808FormatterExtensions.GetFormatter<T>();
                int readSize;
                return formatter.Deserialize(bytes,out readSize);
            }
            catch (Exception ex)
            {
                throw new Exception("Deserialize", ex);
            }
        }
    }
}
