using JT808.Protocol.Attributes;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Test.MessageBody.JT808Formatters;

namespace JT808.Protocol.Test.JT808_0x0900_BodiesImpl
{
    [JT808Formatter(typeof(JT808_0x0900_0x83Formatter))]
    public class JT808_0x0900_0x83 : JT808_0x0900_BodyBase
    {
        /// <summary>
        /// 透传内容
        /// </summary>
        public string PassthroughContent { get; set; }
    }
}
