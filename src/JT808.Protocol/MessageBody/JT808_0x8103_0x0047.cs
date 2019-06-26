using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 当月最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0047_Formatter))]
    public class JT808_0x8103_0x0047 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0047;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 当月最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
