using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// CAN 总线通道 2 采集时间间隔(ms)，0 表示不采集
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0102_Formatter))]
    public class JT808_0x8103_0x0102 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0102;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// CAN 总线通道 2 采集时间间隔(ms)，0 表示不采集
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
