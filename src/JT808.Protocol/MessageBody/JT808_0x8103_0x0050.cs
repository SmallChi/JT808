using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 报警屏蔽字，与位置信息汇报消息中的报警标志相对应，相应位为 1则相应报警被屏蔽
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0050_Formatter))]
    public class JT808_0x8103_0x0050 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0050;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 报警屏蔽字，与位置信息汇报消息中的报警标志相对应，相应位为 1则相应报警被屏蔽
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
