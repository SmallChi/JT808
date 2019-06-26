using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 侧翻报警参数设置：
    /// 侧翻角度，单位 1 度，默认为 30 度
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x005E_Formatter))]
    public class JT808_0x8103_0x005E : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x005E;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 侧翻报警参数设置：
        /// 侧翻角度，单位 1 度，默认为 30 度
        /// </summary>
        public ushort ParamValue { get; set; }
    }
}
