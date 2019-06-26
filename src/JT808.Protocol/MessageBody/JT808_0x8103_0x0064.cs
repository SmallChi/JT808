using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 定时拍照控制，见 表 13
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0064_Formatter))]
    public class JT808_0x8103_0x0064 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0064;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 定时拍照控制，见 表 13
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
