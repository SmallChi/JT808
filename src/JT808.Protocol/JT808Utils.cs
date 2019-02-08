using System;

namespace JT808.Protocol
{
    public static class JT808Utils
    {
        /// <summary>
        /// 转义
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static ReadOnlySpan<byte> JT808DeEscape(ReadOnlySpan<byte> buf)
        {
            var bytes = JT808ArrayPool.Rent(buf.Length);
            try
            {
                int i = 0;
                int offset = 0;
                int len = 0 + buf.Length;
                while (i < len)
                {
                    if (buf[i] == 0x7d)
                    {
                        if (len > i + 1)
                        {
                            if (buf[i + 1] == 0x01)
                            {
                                bytes[offset++] = 0x7d;
                                i++;
                            }
                            else if (buf[i + 1] == 0x02)
                            {
                                bytes[offset++] = 0x7e;
                                i++;
                            }
                            else
                            {
                                bytes[offset++] = buf[i];
                            }
                        }
                    }
                    else
                    {
                        bytes[offset++] = buf[i];
                    }
                    i++;
                }
                return bytes.AsSpan(0, offset).ToArray();
            }
            finally
            {
                JT808ArrayPool.Return(bytes);
            }
        }

        /// <summary>
        /// 转码
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static int JT808Escape(ref byte[] buf, int offset)
        {
            var tmpBuffer = buf.AsSpan(0, offset).ToArray();
            int tmpOffset = 0;
            buf[tmpOffset++] = tmpBuffer[0];
            for (int i = 1; i < offset - 1; i++)
            {
                if (tmpBuffer[i] == 0x7e)
                {
                    buf[tmpOffset++] = 0x7d;
                    buf[tmpOffset++] = 0x02;
                }
                else if (tmpBuffer[i] == 0x7d)
                {
                    buf[tmpOffset++] = 0x7d;
                    buf[tmpOffset++] = 0x01;
                }
                else
                {
                    buf[tmpOffset++] = tmpBuffer[i];
                }
            }
            buf[tmpOffset++] = tmpBuffer[tmpBuffer.Length - 1];
            return tmpOffset;
        }
    }
}
