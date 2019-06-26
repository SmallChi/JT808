using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端心跳发送间隔，单位为秒（s）
    /// 0x8103_0x0001
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0001_Formatter))]
    public class JT808_0x8103_0x0001 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0001;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 终端心跳发送间隔，单位为秒（s）
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
