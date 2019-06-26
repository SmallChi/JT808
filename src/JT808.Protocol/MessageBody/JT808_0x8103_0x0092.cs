using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据输出频率，定义如下：
    /// 0x00：500ms；0x01：1000ms（默认值）；
    /// 0x02：2000ms；0x03：3000ms；
    /// 0x04：4000ms。
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0092_Formatter))]
    public class JT808_0x8103_0x0092 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0092;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 模块详细定位数据输出频率，定义如下：
        /// 0x00：500ms；0x01：1000ms（默认值）；
        /// 0x02：2000ms；0x03：3000ms；
        /// 0x04：4000ms。
        /// </summary>
        public byte ParamValue { get; set; }
    }
}
