using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    public static class JT808Constants
    {
        static JT808Constants()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding = Encoding.GetEncoding("GBK");
        }
        /// <summary>
        /// 日期限制于2000年
        /// </summary>
        public const int DateLimitYear = 2000;
        public static readonly DateTime UTCBaseTime = new DateTime(1970, 1, 1);
        public static Encoding Encoding { get;}
    }
}
