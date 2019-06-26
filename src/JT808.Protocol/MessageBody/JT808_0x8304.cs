using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 信息服务
    /// 0x8304
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8304_Formatter))]
    public class JT808_0x8304 : JT808Bodies
    {
        /// <summary>
        /// 信息类型
        /// </summary>
        public byte InformationType { get; set; }
        /// <summary>
        /// 信息长度
        /// </summary>
        public ushort InformationLength { get; set; }
        /// <summary>
        /// 信息内容
        /// 经 GBK 编码
        /// </summary>
        public string InformationContent { get; set; }

    }
}
