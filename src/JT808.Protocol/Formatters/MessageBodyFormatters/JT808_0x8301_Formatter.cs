using JT808.Protocol.Extensions;
using JT808.Protocol.Metadata;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8301_Formatter : IJT808MessagePackFormatter<JT808_0x8301>
    {
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
