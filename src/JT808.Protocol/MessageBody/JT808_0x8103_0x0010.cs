using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 主服务器 APN，无线通信拨号访问点。若网络制式为 CDMA，则该处为PPP 拨号号码
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0010_Formatter))]
    public class JT808_0x8103_0x0010 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0010;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 主服务器 APN，无线通信拨号访问点。若网络制式为 CDMA，则该处为PPP 拨号号码
        /// </summary>
        public string ParamValue { get; set; }
    }
}
