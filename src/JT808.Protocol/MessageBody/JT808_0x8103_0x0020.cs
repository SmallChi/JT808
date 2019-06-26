using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置汇报策略，0：定时汇报；1：定距汇报；2：定时和定距汇报
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0020_Formatter))]
    public class JT808_0x8103_0x0020 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0020;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 位置汇报策略，0：定时汇报；1：定距汇报；2：定时和定距汇报
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
