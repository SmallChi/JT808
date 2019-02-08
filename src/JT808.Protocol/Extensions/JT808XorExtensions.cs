using System;

namespace JT808.Protocol.Extensions
{
    public static partial class JT808BinaryExtensions
    {
        /// <summary>
        /// 异或
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte ToXor(this byte[] buf, int offset, int len)
        {
            byte result = buf[offset];
            for (int i = offset + 1; i < len; i++)
            {
                result = (byte)(result ^ buf[i]);
            }
            return result;
        }

        /// <summary>
        /// 异或
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte ToXor(this ReadOnlySpan<byte> buf, int offset, int len)
        {
            byte result = buf[offset];
            for (int i = offset + 1; i < len; i++)
            {
                result = (byte)(result ^ buf[i]);
            }
            return result;
        }

        /// <summary>
        /// 异或
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte ToXor(this Span<byte> buf, int offset, int len)
        {
            byte result = buf[offset];
            for (int i = offset + 1; i < len; i++)
            {
                result = (byte)(result ^ buf[i]);
            }
            return result;
        }
    }
}
