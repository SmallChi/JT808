using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据压缩上报
    /// 0x0901
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0901_Formatter))]
    public class JT808_0x0901 : JT808Bodies
    {
        /// <summary>
        /// 未压缩消息长度 
        /// </summary>
        public uint UnCompressMessageLength { get; set; }
        /// <summary>
        /// 未压缩消息体
        /// 压缩消息体为需要压缩的消息经过 GZIP 压缩算法后的消息
        /// 可实现 <see cref="JT808.Protocol.IJT808ICompress"/>自定义压缩算法
        /// </summary>
        public byte[] UnCompressMessage { get; set; }
    }
}
