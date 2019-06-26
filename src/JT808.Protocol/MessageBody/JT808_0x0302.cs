using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 提问应答
    /// 0x0302
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0302_Formatter))]
    public class JT808_0x0302 : JT808Bodies
    {
        /// <summary>
        /// 应答流水号
        /// 对应的提问下发消息的流水号
        /// </summary>
        public ushort ReplySNo { get; set; }
        /// <summary>
        /// 答案 ID 
        /// 提问下发中附带的答案 ID
        /// </summary>
        public byte AnswerId { get; set; }
    }
}
