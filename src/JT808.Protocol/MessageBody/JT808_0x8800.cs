using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 多媒体数据上传应答
    /// 0x8800
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8800_Formatter))]
    public class JT808_0x8800 : JT808Bodies
    {
        /// <summary>
        /// 多媒体ID
        /// </summary>
        public uint MultimediaId { get; set; }
        /// <summary>
        /// 重传包总数
        /// </summary>
        public byte RetransmitPackageCount { get; set; }
        /// <summary>
        /// 重传包 ID 列表
        /// 重传包序号顺序排列，如“包 ID1 包 ID2......包 IDn”。
        /// </summary>
        public byte[] RetransmitPackageIds { get; set; }
    }
}
