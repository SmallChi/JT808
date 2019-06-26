using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 事件报告
    /// 0x0301
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0301_Formatter))]
    public class JT808_0x0301 : JT808Bodies
    {
        /// <summary>
        /// 事件 ID 
        /// </summary>
        public byte EventId { get; set; }
    }
}
