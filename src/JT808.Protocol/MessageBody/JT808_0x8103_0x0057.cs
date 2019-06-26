using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 连续驾驶时间门限，单位为秒（s）
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0057_Formatter))]
    public class JT808_0x8103_0x0057 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0057;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 连续驾驶时间门限，单位为秒（s）
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
