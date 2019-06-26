using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 疲劳驾驶预警差值，单位为秒（s），>0
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x005C_Formatter))]
    public class JT808_0x8103_0x005C : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x005C;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 疲劳驾驶预警差值，单位为秒（s），>0
        /// </summary>
        public ushort ParamValue { get; set; }
    }
}
