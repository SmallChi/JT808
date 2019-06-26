using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆里程表读数，1/10km
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0080_Formatter))]
    public class JT808_0x8103_0x0080 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0080;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 车辆里程表读数，1/10km
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
