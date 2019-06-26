using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 电子围栏半径（非法位移阈值），单位为米
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0031_Formatter))]
    public class JT808_0x8103_0x0031 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0031;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 电子围栏半径（非法位移阈值），单位为米
        /// </summary>
        public ushort ParamValue { get; set; }
    }
}
