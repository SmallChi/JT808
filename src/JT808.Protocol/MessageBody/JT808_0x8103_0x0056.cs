using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 超速持续时间，单位为秒（s）
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0056_Formatter))]
    public class JT808_0x8103_0x0056 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0056;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 超速持续时间，单位为秒（s）
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
