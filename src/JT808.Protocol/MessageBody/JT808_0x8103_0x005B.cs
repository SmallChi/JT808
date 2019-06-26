using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 超速报警预警差值，单位为 1/10Km/h
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x005B_Formatter))]
    public class JT808_0x8103_0x005B : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x005B;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 超速报警预警差值，单位为 1/10Km/h
        /// </summary>
        public ushort ParamValue { get; set; }
    }
}
