using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 关键标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警为关键报警
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0054_Formatter))]
    public class JT808_0x8103_0x0054 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0054;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 关键标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警为关键报警
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
