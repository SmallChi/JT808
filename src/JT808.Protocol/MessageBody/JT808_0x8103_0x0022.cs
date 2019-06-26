using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 驾驶员未登录汇报时间间隔，单位为秒（s），>0
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0022_Formatter))]
    public class JT808_0x8103_0x0022 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0022;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 驾驶员未登录汇报时间间隔，单位为秒（s），>0
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
