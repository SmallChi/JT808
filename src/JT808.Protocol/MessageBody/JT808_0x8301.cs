using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Metadata;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 事件设置
    /// 0x8301
    /// </summary>
    [Obsolete("2019版本已作删除")]
    public class JT808_0x8301 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8301>,IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8301;
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

        public JT808_0x8301 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
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

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8301 value, IJT808Config config)
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
    }
}
