using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 饱和度，0-127
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0073_Formatter))]
    public class JT808_0x8103_0x0073 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0073;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 饱和度，0-127
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
