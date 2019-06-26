using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 休眠时汇报时间间隔，单位为秒（s），>0
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0027_Formatter))]
    public class JT808_0x8103_0x0027 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0027;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 休眠时汇报时间间隔，单位为秒（s），>0
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
