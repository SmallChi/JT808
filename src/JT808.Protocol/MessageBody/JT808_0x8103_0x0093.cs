using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据采集频率，单位为秒，默认为 1。
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0093_Formatter))]
    public class JT808_0x8103_0x0093 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0093;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 模块详细定位数据采集频率，单位为秒，默认为 1。
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
