using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 事件设置
    /// 0x8301
    /// 2019版本已作删除
    /// </summary>
    public class JT808_0x8301 : JT808MessagePackFormatter<JT808_0x8301>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8301
        /// </summary>
        public ushort MsgId  => 0x8301;
        /// <summary>
        /// 事件设置
        /// </summary>
        public string Description => "事件设置";
        /// <summary>
        /// 设置类型
        /// <see cref="JT808.Protocol.Enums.JT808EventSettingType"/>
        /// </summary>
        public byte SettingType { get; set; }
        /// <summary>
        /// 设置总数
        /// </summary>
        public byte SettingCount { get; set; }
        /// <summary>
        /// 事件项
        /// </summary>
        public List<JT808EventProperty> EventItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8301 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8301 jT808_0X8301 = new JT808_0x8301();
            jT808_0X8301.SettingType = reader.ReadByte();
            jT808_0X8301.SettingCount = reader.ReadByte();
            jT808_0X8301.EventItems = new List<JT808EventProperty>();
            for (var i = 0; i < jT808_0X8301.SettingCount; i++)
            {
                JT808EventProperty jT808EventProperty = new JT808EventProperty();
                jT808EventProperty.EventId = reader.ReadByte();
                jT808EventProperty.EventContentLength = reader.ReadByte();
                jT808EventProperty.EventContent = reader.ReadString(jT808EventProperty.EventContentLength);
                jT808_0X8301.EventItems.Add(jT808EventProperty);
            }
            return jT808_0X8301;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8301 value, IJT808Config config)
        {
            writer.WriteByte(value.SettingType);
            if (value.EventItems != null && value.EventItems.Count > 0)
            {
                writer.WriteByte((byte)value.EventItems.Count);
                foreach (var item in value.EventItems)
                {
                    writer.WriteByte(item.EventId);
                    // 先计算内容长度（汉字为两个字节）
                    writer.Skip(1, out int eventPosition);
                    writer.WriteString(item.EventContent);
                    byte eventLength = (byte)(writer.GetCurrentPosition() - eventPosition - 1);
                    writer.WriteByteReturn(eventLength, eventPosition);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8301 value = new JT808_0x8301();
            value.SettingType = reader.ReadByte();
            writer.WriteNumber($"[{value.SettingType.ReadNumber()}]设置类型", value.SettingType);
            value.SettingCount = reader.ReadByte();
            writer.WriteNumber($"[{value.SettingCount.ReadNumber()}]设置总数", value.SettingCount);
            writer.WriteStartArray("事件项");
            for (var i = 0; i < value.SettingCount; i++)
            {
                writer.WriteStartObject();
                JT808EventProperty jT808EventProperty = new JT808EventProperty();
                jT808EventProperty.EventId = reader.ReadByte();
                writer.WriteNumber($"[{jT808EventProperty.EventId.ReadNumber()}]事件ID ", jT808EventProperty.EventId);
                jT808EventProperty.EventContentLength = reader.ReadByte();
                writer.WriteNumber($"[{jT808EventProperty.EventContentLength.ReadNumber()}]事件内容长度", jT808EventProperty.EventContentLength);
                var eventContenBuffer = reader.ReadVirtualArray(jT808EventProperty.EventContentLength).ToArray();
                jT808EventProperty.EventContent = reader.ReadString(jT808EventProperty.EventContentLength);
                writer.WriteString($"[{eventContenBuffer.ToHexString()}]事件内容", jT808EventProperty.EventContent);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

    }
}
