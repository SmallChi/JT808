using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 最长停车时间，单位为秒（s）
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x005A_Formatter))]
    public class JT808_0x8103_0x005A : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x005A;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 最长停车时间，单位为秒（s）
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
