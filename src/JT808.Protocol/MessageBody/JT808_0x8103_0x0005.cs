using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// UDP 消息重传次数
    /// 0x8103_0x0005
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0005_Formatter))]
    public class JT808_0x8103_0x0005 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0005;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// UDP 消息重传次数
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
