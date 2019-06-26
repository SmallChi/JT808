using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 提问下发
    /// 0x8302
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8302_Formatter))]
    public class JT808_0x8302 : JT808Bodies
    {
        /// <summary>
        /// 标志
        /// 提问下发标志位定义
        /// </summary>
        public byte Flag { get; set; }
        /// <summary>
        /// 问题内容长度
        /// </summary>
        public byte IssueContentLength { get; set; }
        /// <summary>
        /// 问题
        /// 问题文本，经 GBK 编码，长度为 N
        /// </summary>
        public string Issue { get; set; }
        /// <summary>
        /// 答案 ID
        /// </summary>
        public byte AnswerId { get; set; }
        /// <summary>
        /// 答案内容长度
        /// 答案内容字段字节长度
        /// </summary>
        public ushort AnswerContentLength { get; set; }
        /// <summary>
        /// 答案内容 
        /// 答案内容，经 GBK 编码
        /// </summary>
        public string AnswerContent { get; set; }
    }
}
