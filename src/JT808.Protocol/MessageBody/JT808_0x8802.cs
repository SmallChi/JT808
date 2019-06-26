using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using System;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 存储多媒体数据检索
    /// 0x8802
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8802_Formatter))]
    public class JT808_0x8802 : JT808Bodies
    {
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
        /// 0：平台下发指令；1：定时动作；2：抢劫报警触发；3：碰撞侧翻报警触发；其他保留
        /// </summary>
        public byte EventItemCoding { get; set; }
        /// <summary>
        /// 起始时间
        /// YY-MM-DD-hh-mm-ss
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// YY-MM-DD-hh-mm-ss
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
