using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 监管平台特权短信号码
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0049_Formatter))]
    public class JT808_0x8103_0x0049 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0049;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 监管平台特权短信号码
        /// </summary>
        public string ParamValue { get; set; }
    }
}
