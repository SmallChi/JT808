using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据下行透传
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8900_Formatter))]
    public class JT808_0x8900 : JT808Bodies
    {
        /// <summary>
        /// 透传消息类型
        /// 透传消息类型定义见 表 93
        /// </summary>
        public byte PassthroughType { get; set; }

        /// <summary>
        /// 数据下行透传数据
        /// </summary>
        public byte[] PassthroughData { get; set; }

        /// <summary>
        /// 透传消息内容
        /// </summary>
        public JT808_0x8900_BodyBase JT808_0X8900_BodyBase { get; set; }
    }
}
