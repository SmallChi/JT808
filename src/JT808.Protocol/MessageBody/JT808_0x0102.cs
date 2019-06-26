using JT808.Protocol.Formatters.MessageBodyFormatters;
using JT808.Protocol.Attributes;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端鉴权
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0102_Formatter))]
    public class JT808_0x0102 : JT808Bodies
    {
        /// <summary>
        /// 鉴权码
        /// </summary>
        public string Code { get; set; }
    }
}
