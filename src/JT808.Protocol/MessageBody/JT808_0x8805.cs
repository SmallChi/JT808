using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 单条存储多媒体数据检索上传命令
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8805Formatter))]
    public class JT808_0x8805 : JT808Bodies
    {
        /// <summary>
        /// 多媒体ID
        /// </summary>
        public uint MultimediaId { get; set; }
        /// <summary>
        /// 删除标志
        /// <see cref="JT808.Protocol.Enums.JT808MultimediaDeleted"/>
        /// </summary>
        public byte MultimediaDeleted { get; set; }
    }
}
