using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 缺省时间汇报间隔，单位为秒（s），>0
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0029_Formatter))]
    public class JT808_0x8103_0x0029 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0029;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 缺省时间汇报间隔，单位为秒（s），>0
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
