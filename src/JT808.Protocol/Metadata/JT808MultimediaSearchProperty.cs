using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// 存储多媒体数据检索应答
    /// </summary>
    public class JT808MultimediaSearchProperty
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
        /// 通道 ID
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 事件项编码
        /// <see cref="JT808.Protocol.Enums.JT808EventItemCoding"/>
        /// </summary>
        public byte EventItemCoding { get; set; }
        /// <summary>
        /// 位置信息汇报(0x0200)消息体
        /// 表示拍摄或录制的起始时刻的位置基本信息数据
        /// </summary>
        public JT808_0x0200 Position { get; set; }
    }
}
