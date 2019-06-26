using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据上行透传
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0900_Formatter))]
    public class JT808_0x0900 : JT808Bodies
    {
        /// <summary>
        /// 透传消息类型
        /// </summary>
        public byte PassthroughType { get; set; }

        /// <summary>
        /// 透传数据
        /// </summary>
        public byte[] PassthroughData { get; set; }

        /// <summary>
        /// 透传消息内容
        /// </summary>
        public JT808_0x0900_BodyBase JT808_0x0900_BodyBase { get; set; }
    }
}
