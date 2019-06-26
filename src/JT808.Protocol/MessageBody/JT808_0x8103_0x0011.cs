using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 主服务器无线通信拨号用户名
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0011_Formatter))]
    public class JT808_0x8103_0x0011 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0011;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 主服务器无线通信拨号用户名
        /// </summary>
        public string ParamValue { get; set; }
    }
}
