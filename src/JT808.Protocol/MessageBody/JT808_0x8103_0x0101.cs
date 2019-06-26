using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// CAN 总线通道 1 上传时间间隔(s)，0 表示不上传
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0101_Formatter))]
    public class JT808_0x8103_0x0101 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0101;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// CAN 总线通道 1 上传时间间隔(s)，0 表示不上传
        /// </summary>
        public ushort ParamValue { get; set; }
    }
}
