using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 多媒体事件信息上传
    /// 0x0800
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0800Formatter))]
    public class JT808_0x0800 : JT808Bodies
    {
        /// <summary>
        /// 多媒体数据 ID
        /// </summary>
        public uint MultimediaId { get; set; }
        /// <summary>
        /// 多媒体类型
        /// 0：图像；1：音频；2：视频；
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
        /// 0：平台下发指令；
        /// 1：定时动作；
        /// 2：抢劫报警触发；
        /// 3：碰撞侧翻报警触发；
        /// 4：门开拍照；
        /// 5：门关拍照；
        /// 6：车门由开变关，时速从＜20 公里到超过 20 公里；
        /// 7：定距拍照；
        /// 其他保留
        /// </summary>
        public byte EventItemCoding { get; set; }
        /// <summary>
        /// 通道 ID
        /// </summary>
        public byte ChannelId { get; set; }
    }
}
