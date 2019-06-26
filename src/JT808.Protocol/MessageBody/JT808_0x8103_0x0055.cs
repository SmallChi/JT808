using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 最高速度，单位为公里每小时（km/h）
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0055_Formatter))]
    public class JT808_0x8103_0x0055 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0055;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 最高速度，单位为公里每小时（km/h）
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
