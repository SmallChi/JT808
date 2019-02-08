using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using System;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 电子运单上报
    /// 0x0701
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0701Formatter))]
    public class JT808_0x0701 : JT808Bodies
    {
        /// <summary>
        /// 电子运单长度
        /// </summary>
        public uint ElectronicWaybillLength { get; set; }

        /// <summary>
        /// 电子运单内容
        /// 注意:需要具体的实现
        /// </summary>
        public JT808_0x0701Body ElectronicContent { get; set; }

        /// <summary>
        /// 电子运单内容基类
        /// </summary>
        public abstract class JT808_0x0701Body
        {
            internal static Type BodyImpl { get; set; }
        }
    }
}
