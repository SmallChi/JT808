using System;

namespace JT808.Protocol.Extensions
{
    /// <summary>
    /// 
    /// ref:"www.codeproject.com/tips/447938/high-performance-csharp-byte-array-to-hex-string-t"
    /// </summary>
    public static partial class JT808BinaryExtensions
    {
        /// <summary>
        /// 16进制数组转16进制字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] source)
        {
            return Convert.ToHexString(source, 0, source.Length);
        }

        /// <summary>
        /// 16进制字符串转16进制数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToHexBytes(this string hexString)
        {
            hexString = hexString.Replace(" ", "");
            return Convert.FromHexString(hexString);
        }

        /// <summary>
        /// 从内存块中读取16进制字符串
        /// </summary>
        /// <param name="read"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string ReadHexStringLittle(ReadOnlySpan<byte> read, ref int offset, int len)
        {
            string hex = Convert.ToHexString(read.Slice(offset, len));
            offset += len;
            return hex;
        }

        /// <summary>
        /// 将16进制字符串写入对应数组中
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="data"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static int WriteHexStringLittle(byte[] bytes, int offset, string data, int len)
        {
            if (data == null) data = "";
            data = data.Replace(" ", "");
            int startIndex = 0;
            if (data.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                startIndex = 2;
            }
            int length = len;
            if (length == -1)
            {
                length = (data.Length - startIndex) / 2;
            }
            int noOfZero = length * 2 + startIndex - data.Length;
            if (noOfZero > 0)
            {
                data = data.Insert(startIndex, new string('0', noOfZero));
            }
            int byteIndex = 0;
            while (startIndex < data.Length && byteIndex < length)
            {
                bytes[offset + byteIndex] = Convert.ToByte(data.Substring(startIndex, 2), 16);
                startIndex += 2;
                byteIndex++;
            }
            return length;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ReadNumber(this byte value, string format = "X2")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ReadNumber(this int value, string format = "X8")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this uint value, string format = "X8")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this long value, string format = "X16")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this ulong value, string format="X16")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this short value, string format = "X4")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this ushort value, string format = "X4")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this ushort value)
        {
            return System.Convert.ToString(value, 2).PadLeft(16, '0').AsSpan();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this short value)
        {
            return System.Convert.ToString(value, 2).PadLeft(16, '0').AsSpan();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this uint value)
        {
            return System.Convert.ToString(value, 2).PadLeft(32, '0').AsSpan();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this int value)
        {
            return System.Convert.ToString(value, 2).PadLeft(32, '0').AsSpan();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this byte value)
        {
            return System.Convert.ToString(value, 2).PadLeft(8, '0').AsSpan();
        }
    }
}
