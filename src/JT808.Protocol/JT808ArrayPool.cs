using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    public static class JT808ArrayPool
    {
        private readonly static ArrayPool<byte> ArrayPool;

        static JT808ArrayPool()
        {
            ArrayPool = ArrayPool<byte>.Create();
        }

        public static byte[] Rent(int minimumLength)
        {
            return ArrayPool.Rent(minimumLength);
        }

        public static void Return(byte[] array, bool clearArray = false)
        {
             ArrayPool.Return(array, clearArray);
        }
    }
}
