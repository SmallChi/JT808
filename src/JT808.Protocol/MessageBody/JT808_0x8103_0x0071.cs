using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 亮度，0-255
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0071_Formatter))]
    public class JT808_0x8103_0x0071 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0071;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 亮度，0-255
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
