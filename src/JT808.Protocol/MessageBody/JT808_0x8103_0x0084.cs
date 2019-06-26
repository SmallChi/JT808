using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车牌颜色，按照 JT/T415-2006 的 5.4.12
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0084_Formatter))]
    public class JT808_0x8103_0x0084 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0084;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 车牌颜色，按照 JT/T415-2006 的 5.4.12
        /// </summary>
        public byte ParamValue { get; set; }
    }
}
