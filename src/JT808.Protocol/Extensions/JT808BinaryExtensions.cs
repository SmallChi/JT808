using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions
{
    public static partial class JT808BinaryExtensions
    {
        public static int ReadInt32Little(ReadOnlySpan<byte> read, ref int offset)
        {
            int value = ((read[offset] << 24) | (read[offset + 1] << 16) | (read[offset + 2] << 8) | read[offset + 3]);
            offset = offset + 4;
            return value;
        }

        public static uint ReadUInt32Little(ReadOnlySpan<byte> read, ref int offset)
        {
            uint value = (uint)((read[offset] << 24) | (read[offset + 1] << 16) | (read[offset + 2] << 8) | read[offset + 3]);
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

        public static byte[] ReadBytesLittle(ReadOnlySpan<byte> read, ref int offset, int len)
        {
            ReadOnlySpan<byte> temp = read.Slice(offset, len);
            offset = offset + len;
            return temp.ToArray();
        }

        public static byte[] ReadBytesLittle(ReadOnlySpan<byte> read, ref int offset)
        {
            ReadOnlySpan<byte> temp = read.Slice(offset);
            offset = offset + temp.Length;
            return temp.ToArray();
        }

        public static int WriteUInt32Little(byte[] write, int offset, uint data)
        {
            write[offset] = (byte)(data >> 24);
            write[offset + 1] = (byte)(data >> 16);
            write[offset + 2] = (byte)(data >> 8);
            write[offset + 3] = (byte)data;
            return 4;
        }

        public static int WriteInt32Little(byte[] write, int offset, int data)
        {
            write[offset] = (byte)(data >> 24);
            write[offset + 1] = (byte)(data >> 16);
            write[offset + 2] = (byte)(data >> 8);
            write[offset + 3] = (byte)data;
            return 4;
        }

        public static int WriteUInt16Little(byte[] write, int offset, ushort data)
        {
            write[offset] = (byte)(data >> 8);
            write[offset + 1] = (byte)data;
            return 2;
        }

        public static int WriteByteLittle(byte[] write, int offset, byte data)
        {
            write[offset] = data;
            return 1;
        }

        public static int WriteBytesLittle(byte[] write, int offset, byte[] data)
        {
            Array.Copy(data, 0, write, offset, data.Length);
            return data.Length;
        }

        public static IEnumerable<byte> ToBytes(this string data, Encoding coding)
        {
            return coding.GetBytes(data);
        }

        public static IEnumerable<byte> ToBytes(this string data)
        {
            return ToBytes(data, JT808GlobalConfig.Instance.Encoding);
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
        /// 经纬度
        /// </summary>
        /// <param name="latlng"></param>
        /// <returns></returns>
        public static double ToLatLng(this int latlng)
        {
            return Math.Round(latlng / Math.Pow(10, 6), 6);
        }
    }
}
