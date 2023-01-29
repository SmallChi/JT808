using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 存储多媒体数据上传命令
    /// 0x8803
    /// </summary>
    public class JT808_0x8803 : JT808MessagePackFormatter<JT808_0x8803>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x8803
        /// </summary>
        public ushort MsgId => 0x8803;
        /// <summary>
        /// 存储多媒体数据上传命令
        /// </summary>
        public string Description => "存储多媒体数据上传命令";
        /// <summary>
        /// 多媒体类型 
        /// <see cref="JT808.Protocol.Enums.JT808MultimediaType"/>
        /// 0：图像；1：音频；2：视频
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
        /// <summary>
        /// 删除标志
        /// <see cref="JT808.Protocol.Enums.JT808MultimediaDeleted"/>
        /// </summary>
        public byte MultimediaDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8803 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8803 jT808_0X8803 = new JT808_0x8803();
            jT808_0X8803.MultimediaType = reader.ReadByte();
            jT808_0X8803.ChannelId = reader.ReadByte();
            jT808_0X8803.EventItemCoding = reader.ReadByte();
            jT808_0X8803.StartTime = reader.ReadDateTime_yyMMddHHmmss();
            jT808_0X8803.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            jT808_0X8803.MultimediaDeleted = reader.ReadByte();
            return jT808_0X8803;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8803 value, IJT808Config config)
        {
            writer.WriteByte(value.MultimediaType);
            writer.WriteByte(value.ChannelId);
            writer.WriteByte(value.EventItemCoding);
            writer.WriteDateTime_yyMMddHHmmss(value.StartTime);
            writer.WriteDateTime_yyMMddHHmmss(value.EndTime);
            writer.WriteByte(value.MultimediaDeleted);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8803 value = new JT808_0x8803();
            value.MultimediaType = reader.ReadByte();
            value.ChannelId = reader.ReadByte();
            value.EventItemCoding = reader.ReadByte();
            value.StartTime = reader.ReadDateTime_yyMMddHHmmss();
            value.EndTime = reader.ReadDateTime_yyMMddHHmmss();
            value.MultimediaDeleted = reader.ReadByte();
            JT808MultimediaType multimediaType = (JT808MultimediaType)value.MultimediaType;
            JT808EventItemCoding eventItemCoding = (JT808EventItemCoding)value.EventItemCoding;
            JT808MultimediaDeleted multimediaDeleted = (JT808MultimediaDeleted)value.MultimediaDeleted;
            writer.WriteNumber($"[{ value.MultimediaType.ReadNumber()}]多媒体类型-{multimediaType.ToString()}", value.MultimediaType);
            writer.WriteNumber($"[{ value.ChannelId.ReadNumber()}]通道ID", value.ChannelId);
            writer.WriteNumber($"[{ value.EventItemCoding.ReadNumber()}]事件项编码-{eventItemCoding.ToString()}", value.EventItemCoding);
            writer.WriteString($"[{ value.StartTime.ToString("yyMMddHHmmss")}]起始时间", value.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteString($"[{ value.EndTime.ToString("yyMMddHHmmss")}]结束时间", value.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteNumber($"[{ value.MultimediaDeleted.ReadNumber()}]删除标志-{multimediaDeleted.ToString()}", value.MultimediaDeleted);
        }
    }
}
