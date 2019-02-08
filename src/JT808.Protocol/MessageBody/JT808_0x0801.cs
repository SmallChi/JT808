using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 多媒体数据上传
    /// 0x0801
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0801Formatter))]
    public class JT808_0x0801 : JT808Bodies
    {
        /// <summary>
        /// 多媒体 ID
        /// </summary>
        public uint MultimediaId { get; set; }
        /// <summary>
        /// 多媒体类型
        /// <see cref="JT808.Protocol.Enums.JT808MultimediaType"/>
        /// </summary>
        public byte MultimediaType { get; set; }
        /// <summary>
        /// 多媒体格式编码 
        /// 0：JPEG；1：TIF；2：MP3；3：WAV；4：WMV；其他保留
        /// <see cref="JT808.Protocol.Enums.JT808MultimediaCodingFormat"/>
        /// </summary>
        public byte MultimediaCodingFormat { get; set; }
        /// <summary>
        /// 事件项编码
        /// <see cref="JT808.Protocol.Enums.JT808EventItemCoding"/>
        /// </summary>
        public byte EventItemCoding { get; set; }
        /// <summary>
        /// 通道 ID
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 位置信息汇报(0x0200)消息体
        /// 表示拍摄或录制的起始时刻的位置基本信息数据
        /// </summary>
        public JT808_0x0200 Position { get; set; }
        /// <summary>
        /// 多媒体数据包
        /// </summary>
        public byte[] MultimediaDataPackage { get; set; }
    }
}
