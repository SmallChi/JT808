using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端电话接听策略，0：自动接听；1：ACC ON 时自动接听，OFF 时手动接听
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0045_Formatter))]
    public class JT808_0x8103_0x0045 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0045;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 终端电话接听策略，0：自动接听；1：ACC ON 时自动接听，OFF 时手动接听
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
