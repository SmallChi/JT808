using System;
using System.Text;

namespace JT808.Protocol.Extensions
{
    public static partial class JT808BinaryExtensions
    {
        public static string ReadBCDLittle(ReadOnlySpan<byte> buf, ref int offset, int len)
        {
            int count = len / 2;
            StringBuilder bcdSb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                bcdSb.Append(buf[offset + i].ToString("X2"));
            }
            offset = offset + count;
            return bcdSb.ToString().TrimStart('0');
        }

        public static int WriteBCDLittle(byte[] bytes, int offset, string data, int len)
        {
            string bcdText = data == null ? "" : data;
            int startIndex = 0;
            int noOfZero = len - bcdText.Length;
            if (noOfZero > 0)
            {
                bcdText = bcdText.Insert(startIndex, new string('0', noOfZero));
            }
            int byteIndex = 0;
            int count = len / 2;
            byte[] tempBytes = new byte[count];
            while (startIndex < bcdText.Length && byteIndex < count)
            {
                tempBytes[byteIndex] = Convert.ToByte(bcdText.Substring(startIndex, 2), 16);
                startIndex += 2;
                byteIndex++;
            }
            Array.Copy(tempBytes, 0, bytes, offset, tempBytes.Length);
            return count;
        }
    }
}
