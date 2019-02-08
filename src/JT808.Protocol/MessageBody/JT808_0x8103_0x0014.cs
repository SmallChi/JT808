using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 备份服务器 APN，无线通信拨号访问点
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0014Formatter))]
    public class JT808_0x8103_0x0014 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0014;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 备份服务器 APN，无线通信拨号访问点
        /// </summary>
        public string ParamValue { get; set; }
    }
}
