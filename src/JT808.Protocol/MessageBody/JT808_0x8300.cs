using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 文本信息下发
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8300_Formatter))]
    public class JT808_0x8300 : JT808Bodies
    {
        /// <summary>
        /// 文本信息标志位含义见 表 38
        /// </summary>
        public byte TextFlag { get; set; }

        /// <summary>
        /// 文本信息
        /// 最长为 1024 字节，经GBK编码
        /// </summary>
        public string TextInfo { get; set; }
    }
}
