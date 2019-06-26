using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 波特率，定义如下：
    /// 0x00：4800；0x01：9600；
    /// 0x02：19200；0x03：38400；
    /// 0x04：57600；0x05：115200。
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0091_Formatter))]
    public class JT808_0x8103_0x0091 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0091;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 波特率，定义如下：
        /// 0x00：4800；0x01：9600；
        /// 0x02：19200；0x03：38400；
        /// 0x04：57600；0x05：115200。
        /// </summary>
        public byte ParamValue { get; set; }
    }
}
