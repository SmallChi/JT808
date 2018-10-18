using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Buffers.Binary;
using System.Buffers;

namespace JT808.Protocol.Extensions
{
    public  static   class JT808BinaryExtensions
    {
        /// <summary>
        /// 日期限制于2000年
        /// </summary>
        private const int DateLimitYear = 2000;

        static JT808BinaryExtensions()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encoding = Encoding.GetEncoding("GBK");
        }

        private static Encoding encoding;

        public static int ReadBCD32(this byte data, byte dig)
        {
            int result = Convert.ToInt32(data.ToString("X"));
            return result * (int)Math.Pow(100, dig - 1);
        }

        public static long ReadBCD64(this byte data, byte dig)
        {
            long result = Convert.ToInt64(data.ToString("X"));
            return result * (long)Math.Pow(100, dig - 1);
        }

        public static string ReadStringLittle(ReadOnlySpan<byte> read, ref int offset, int len)
        {
            string value = encoding.GetString(read.Slice(offset, len).ToArray()).Trim('\0');
            offset += value.Length;
            return value;
        }

        public static string ReadStringLittle(ReadOnlySpan<byte> read, ref int offset)
        {
            string value = encoding.GetString(read.Slice(offset).ToArray()).Trim('\0');
            offset += value.Length;
            return value;
        }

        //public static void WriteLatLng(byte[] write, int offset,double latlng)
        //{
        //    WriteLittle(write, (int)(Math.Pow(10, 6) * latlng), offset, 4);
        //}

        public static long ReadBCD(ReadOnlySpan<byte> buf,ref int offset, int len)
        {
            long result = 0;
            try
            {
                for (int i = offset; i < offset + len; i++)
                {
                    result += buf[i].ReadBCD64((byte)(offset + len - i));
                }
            }
            catch
            {
            }
            offset = offset + len;
            return result;
        }

        public static DateTime ReadDateTimeLittle(ReadOnlySpan<byte> buf,ref int offset)
        {
            DateTime dateTime= new DateTime(
                (buf[offset]).ReadBCD32(1) + DateLimitYear,
                (buf[offset + 1]).ReadBCD32(1),
                (buf[offset + 2]).ReadBCD32(1),
                (buf[offset + 3]).ReadBCD32(1),
                (buf[offset + 4]).ReadBCD32(1),
                (buf[offset + 5]).ReadBCD32(1));
            offset = offset + 6;
            return dateTime;
        }

        public static DateTime ReadDateLittle(ReadOnlySpan<byte> buf, ref int offset)
        {
            DateTime dateTime = new DateTime(
                    ((buf[offset] << 8) | (buf[offset + 1])),
                    (buf[offset + 2]).ReadBCD32(1),
                    (buf[offset + 3]).ReadBCD32(1));
            offset = offset + 4;
            return dateTime;
        }

        public static int ReadInt32Little(ReadOnlySpan<byte> read, ref int offset)
        {
            int value= (read[offset] << 24) | (read[offset + 1] << 16) | (read[offset + 2] << 8) | read[offset + 3];
            offset = offset + 4;
            return value;
        }
        public static uint ReadUInt32Little(ReadOnlySpan<byte> read, ref int offset)
        {
            uint value =(uint) ((read[offset] << 24) | (read[offset + 1] << 16) | (read[offset + 2] << 8) | read[offset + 3]);
            offset = offset + 4;
            return value;
        }

        public static ushort ReadUInt16Little(ReadOnlySpan<byte> read, ref int offset)
        {
            ushort value = (ushort)((read[offset] << 8) | (read[offset + 1]));
            offset = offset + 2;
            return value;
        }

        public static byte ReadByteLittle(ReadOnlySpan<byte> read, ref int offset)
        {
            byte value = read[offset];
            offset = offset + 1;
            return value;
        }

        public static byte[] ReadBytesLittle(ReadOnlySpan<byte> read, ref int offset,int len)
        {
            ReadOnlySpan<byte> temp = read.Slice(offset, len);
            offset = offset + len;
            return temp.ToArray();
        }

        public static int WriteLittle(ref byte[] write, int offset, DateTime date)
        {
            write[offset] = ((byte)(date.Year - DateLimitYear)).ToBcdByte();
            write[offset + 1] = ((byte)(date.Month)).ToBcdByte();
            write[offset + 2] = ((byte)(date.Day)).ToBcdByte();
            write[offset + 3] = ((byte)(date.Hour)).ToBcdByte();
            write[offset + 4] = ((byte)(date.Minute)).ToBcdByte();
            write[offset + 5] = ((byte)(date.Second)).ToBcdByte();
            return 6;
        }

        public static int WriteDateTime6Little(IMemoryOwner<byte> memoryOwner, int offset, DateTime date)
        {
            memoryOwner.Memory.Span[offset] = ((byte)(date.Year - DateLimitYear)).ToBcdByte();
            memoryOwner.Memory.Span[offset + 1] = ((byte)(date.Month)).ToBcdByte();
            memoryOwner.Memory.Span[offset + 2] = ((byte)(date.Day)).ToBcdByte();
            memoryOwner.Memory.Span[offset + 3] = ((byte)(date.Hour)).ToBcdByte();
            memoryOwner.Memory.Span[offset + 4] = ((byte)(date.Minute)).ToBcdByte();
            memoryOwner.Memory.Span[offset + 5] = ((byte)(date.Second)).ToBcdByte();
            return 6;
        }

        public static int WriteDateLittle(ref byte[] write, int offset, DateTime date)
        {
            write[offset] = (byte)(date.Year >> 8);
            write[offset + 1] = (byte)date.Year;
            write[offset + 2] = ((byte)(date.Month)).ToBcdByte();
            write[offset + 3] = ((byte)(date.Day)).ToBcdByte();
            return 4;
        }

        public static int WriteDateTime4Little(IMemoryOwner<byte> memoryOwner, int offset, DateTime date)
        {
            memoryOwner.Memory.Span[offset] = (byte)(date.Year >> 8);
            memoryOwner.Memory.Span[offset + 1] = (byte)date.Year;
            memoryOwner.Memory.Span[offset + 2] = ((byte)(date.Month)).ToBcdByte();
            memoryOwner.Memory.Span[offset + 3] = ((byte)(date.Day)).ToBcdByte();
            return 4;
        }

        public static int WriteLittle(ref byte[] write, int offset, int data)
        {
            write[offset] = (byte)(data >> 24);
            write[offset + 1] = (byte)(data >> 16);
            write[offset + 2] = (byte)(data >> 8);
            write[offset + 3] = (byte)data;
            return 4;
        }

        public static int WriteInt32Little(IMemoryOwner<byte> memoryOwner, int offset, int data)
        {
            memoryOwner.Memory.Span[offset] = (byte)(data >> 24);
            memoryOwner.Memory.Span[offset + 1] = (byte)(data >> 16);
            memoryOwner.Memory.Span[offset + 2] = (byte)(data >> 8);
            memoryOwner.Memory.Span[offset + 3] = (byte)data;
            return 4;
        }
        public static int WriteUInt32Little(IMemoryOwner<byte> memoryOwner, int offset, uint data)
        {
            memoryOwner.Memory.Span[offset] = (byte)(data >> 24);
            memoryOwner.Memory.Span[offset + 1] = (byte)(data >> 16);
            memoryOwner.Memory.Span[offset + 2] = (byte)(data >> 8);
            memoryOwner.Memory.Span[offset + 3] = (byte)data;
            return 4;
        }

        public static int WriteUInt16Little(ref byte[] write, int offset, ushort data)
        {
            write[offset] = (byte)(data >> 8);
            write[offset + 1] = (byte)data;
            return 2;
        }

        public static int WriteUInt16Little(IMemoryOwner<byte> memoryOwner, int offset, ushort data)
        {
            memoryOwner.Memory.Span[offset] = (byte)(data >> 8);
            memoryOwner.Memory.Span[offset + 1] = (byte)data;
            return 2;
        }

        public static int WriteLittle(ref byte[] write, int offset, byte data)
        {
            write[offset] = data;
            return 1;
        }

        public static int WriteByteLittle(IMemoryOwner<byte> memoryOwner, int offset, byte data)
        {
            memoryOwner.Memory.Span[offset] = data;
            return 1;
        }

        public static int WriteBytesLittle(IMemoryOwner<byte> memoryOwner, int offset, byte[] data)
        {
            CopyTo(data, memoryOwner.Memory.Span, offset);
            return data.Length;
        }

        public static int WriteLittle(ref byte[] write, int offset, byte[] data)
        {
            Buffer.BlockCopy(data, 0, write, offset, data.Length);
            return data.Length;
        }

        public static int WriteLittle(ref byte[] write, int offset, string data)
        {
            byte[] codeBytes = encoding.GetBytes(data);
            Buffer.BlockCopy(codeBytes, 0, write, offset, codeBytes.Length);
            return codeBytes.Length;
        }

        public static int WriteStringLittle(IMemoryOwner<byte> memoryOwner, int offset, string data)
        {
            byte[] codeBytes = encoding.GetBytes(data);
            CopyTo(codeBytes, memoryOwner.Memory.Span, offset);
            return codeBytes.Length;
        }

        public static int WriteBCDLittle(ref byte[] write, int offset, string data, int digit, int len)
        {
            ReadOnlySpan<char> bcd = data.PadLeft(len, '0').AsSpan();
            for (int i = 0; i < digit; i++)
            {
                write[offset + i] = Convert.ToByte(bcd.Slice(i * 2, 2).ToString(), 16);
            }
            return digit;
        }

        public static int WriteBCDLittle(IMemoryOwner<byte> memoryOwner, int offset, string data, int digit, int len)
        {
            ReadOnlySpan<char> bcd = data.PadLeft(len, '0').AsSpan();
            for (int i = 0; i < digit; i++)
            {
                memoryOwner.Memory.Span[offset + i] = Convert.ToByte(bcd.Slice(i * 2, 2).ToString(), 16);
            }
            return digit;
        }

        public static IEnumerable<byte> ToBytes(this string data, Encoding coding)
        {
            return coding.GetBytes(data);
        }

        public static IEnumerable<byte> ToBytes(this string data)
        {
            return ToBytes(data, encoding);
        }

        public static IEnumerable<byte> ToBytes(this int data, int len)
        {
            List<byte> bytes = new List<byte>();
            int n = 1;
            for (int i = 0; i < len; i++)
            {
                bytes.Add((byte)(data >> 8 * (len - n)));
                n++;
            }
            return bytes;
        }

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

        public static byte ToBcdByte(this byte buf)
        {
            return (byte)Convert.ToInt32(buf.ToString(), 16);
        }

        /// <summary>
        /// 经纬度
        /// </summary>
        /// <param name="latlng"></param>
        /// <returns></returns>
        public static double ToLatLng(this int latlng)
        {
            return Math.Round(latlng / Math.Pow(10, 6), 6);
        }

        public static void CopyTo(Span<byte> source, Span<byte> destination, int offset)
        {
            for (int i = 0; i < source.Length; i++)
            {
                destination[offset + i] = source[i];
            }
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator">默认 " "</param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes, string separator = " ")
        {
            return string.Join(separator, bytes.Select(s => s.ToString("X2")));
        }

        /// <summary>
        /// 16进制字符串转16进制数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static byte[] ToHexBytes(this string hexString, string separator = " ")
        {
            return hexString.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToByte(s, 16)).ToArray();
        }

        /// <summary>
        /// 16进制字符串转16进制数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToStr2HexBytes(this string hexString)
        {
            //byte[] buf = new byte[hexString.Length / 2];
            //for (int i = 0; i < hexString.Length; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        buf[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16) ;
            //    }

            //}
            byte[] buf = new byte[hexString.Length / 2];
            ReadOnlySpan<char> readOnlySpan = hexString.AsSpan();
            for (int i = 0; i < hexString.Length; i++)
            {
                if (i % 2 == 0)
                {
                    buf[i / 2] = Convert.ToByte(readOnlySpan.Slice(i, 2).ToString(), 16);
                }
            }
            return buf;
            //List<byte> bytes = new List<byte>();
            //while (hexString.Length>0)
            //{
            //    bytes.Add(Convert.ToByte(hexString.AsSpan(0, 2).ToString(), 16));
            //    hexString = hexString.Remove(0,2);
            //}
            //return Regex.Replace(hexString, @"(\w{2})", "$1 ").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToByte(s, 16)).ToArray();
        }
    }
}
