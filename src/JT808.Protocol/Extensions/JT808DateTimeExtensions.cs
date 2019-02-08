using System;

namespace JT808.Protocol.Extensions
{
    public static partial class JT808BinaryExtensions
    {
        /// <summary>
        /// 日期限制于2000年
        /// </summary>
        private const int DateLimitYear = 2000;

        private static readonly DateTime UTCBaseTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="format">D2： 10  X2：16</param>
        /// <returns></returns>
        public static DateTime ReadDateTime6Little(ReadOnlySpan<byte> buf, ref int offset, string format = "X2")
        {
            DateTime d = UTCBaseTime;
            try
            {
                int year = Convert.ToInt32(buf[offset].ToString(format)) + DateLimitYear;
                int month = Convert.ToInt32(buf[offset + 1].ToString(format));
                int day = Convert.ToInt32(buf[offset + 2].ToString(format));
                int hour = Convert.ToInt32(buf[offset + 3].ToString(format));
                int minute = Convert.ToInt32(buf[offset + 4].ToString(format));
                int second = Convert.ToInt32(buf[offset + 5].ToString(format));
                d = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                d = UTCBaseTime;
            }
            offset = offset + 6;
            return d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="format">D2： 10  X2：16</param>
        /// <returns></returns>
        public static DateTime ReadDateTime4Little(ReadOnlySpan<byte> buf, ref int offset, string format = "X2")
        {
            DateTime d = UTCBaseTime;
            try
            {
                d = new DateTime(
                (Convert.ToInt32(buf[offset].ToString(format)) << 8) + Convert.ToByte(buf[offset + 1]),
                Convert.ToInt32(buf[offset + 2].ToString(format)),
                Convert.ToInt32(buf[offset + 3].ToString(format)));
            }
            catch (Exception)
            {
                d = UTCBaseTime;
            }
            offset = offset + 4;
            return d;
        }

        public static DateTime ReadUTCDateTimeLittle(ReadOnlySpan<byte> buf, ref int offset)
        {
            ulong result = 0;
            for (int i = 0; i < 8; i++)
            {
                ulong currentData = (ulong)buf[offset + i] << (8 * (8 - i - 1));
                result += currentData;
            }
            offset += 8;
            return UTCBaseTime.AddSeconds(result).AddHours(8);
        }
        /// <summary>
        /// hh-mm-ss-msms
        /// hh-mm-ss-fff
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ReadDateTime5Little(ReadOnlySpan<byte> buf, ref int offset, string format = "X2")
        {

            DateTime dateTime = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                Convert.ToInt32(buf[offset].ToString(format)),
                Convert.ToInt32(buf[offset + 1].ToString(format)),
                Convert.ToInt32(buf[offset + 2].ToString(format)),
                ((buf[offset + 3] << 8) + buf[offset + 4]));
            offset = offset + 5;
            return dateTime;
        }

        public static int WriteUTCDateTimeLittle(byte[] bytes, int offset, DateTime date)
        {
            ulong totalSecends = (ulong)(date.AddHours(-8) - UTCBaseTime).TotalSeconds;
            //高位在前
            for (int i = 7; i >= 0; i--)
            {
                bytes[offset + i] = (byte)(totalSecends & 0xFF);  //取低8位
                totalSecends = totalSecends >> 8;
            }
            return 8;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoryOwner"></param>
        /// <param name="offset"></param>
        /// <param name="date"></param>
        /// <param name="fromBase">BCD：10  HEX：16</param>
        /// <returns></returns>
        public static int WriteDateTime6Little(byte[] bytes, int offset, DateTime date, int fromBase = 16)
        {
            bytes[offset] = Convert.ToByte(date.ToString("yy"), fromBase);
            bytes[offset + 1] = Convert.ToByte(date.ToString("MM"), fromBase);
            bytes[offset + 2] = Convert.ToByte(date.ToString("dd"), fromBase);
            bytes[offset + 3] = Convert.ToByte(date.ToString("HH"), fromBase);
            bytes[offset + 4] = Convert.ToByte(date.ToString("mm"), fromBase);
            bytes[offset + 5] = Convert.ToByte(date.ToString("ss"), fromBase);
            return 6;
        }

        /// <summary>
        /// YYYYMMDD
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="date"></param>
        /// <param name="fromBase">BCD：10  HEX：16</param>
        /// <returns></returns>
        public static int WriteDateTime4Little(byte[] bytes, int offset, DateTime date, int fromBase = 16)
        {
            bytes[offset] = (byte)(date.Year >> 8);
            bytes[offset + 1] = (byte)(date.Year);
            bytes[offset + 2] = Convert.ToByte(date.ToString("MM"), fromBase);
            bytes[offset + 3] = Convert.ToByte(date.ToString("dd"), fromBase);
            return 4;
        }
        /// <summary>
        /// hh-mm-ss-msms
        /// hh-mm-ss-fff
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="date"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
        public static int WriteDateTime5Little(byte[] bytes, int offset, DateTime date, int fromBase = 16)
        {
            bytes[offset] = Convert.ToByte(date.ToString("HH"), fromBase);
            bytes[offset + 1] = Convert.ToByte(date.ToString("mm"), fromBase);
            bytes[offset + 2] = Convert.ToByte(date.ToString("ss"), fromBase);
            bytes[offset + 3] = (byte)(date.Millisecond >> 8);
            bytes[offset + 4] = (byte)(date.Millisecond);
            return 5;
        }
    }
}
