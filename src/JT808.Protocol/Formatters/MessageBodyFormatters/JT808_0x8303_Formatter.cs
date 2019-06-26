using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using JT808.Protocol.Metadata;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8303_Formatter : IJT808MessagePackFormatter<JT808_0x8303>
    {
        public JT808_0x8303 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8303 jT808_0X8303 = new JT808_0x8303();
            jT808_0X8303.SettingType = reader.ReadByte();
            jT808_0X8303.InformationItemCount = reader.ReadByte();
            jT808_0X8303.InformationItems = new List<JT808InformationItemProperty>();
            for (var i = 0; i < jT808_0X8303.InformationItemCount; i++)
            {
                JT808InformationItemProperty jT808InformationItemProperty = new JT808InformationItemProperty();
                jT808InformationItemProperty.InformationType = reader.ReadByte();
                jT808InformationItemProperty.InformationLength = reader.ReadUInt16();
                jT808InformationItemProperty.InformationName = reader.ReadString(jT808InformationItemProperty.InformationLength);
                jT808_0X8303.InformationItems.Add(jT808InformationItemProperty);
            }
            return jT808_0X8303;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8303 value, IJT808Config config)
        {
            writer.WriteByte(value.SettingType);
            writer.WriteByte((byte)value.InformationItems.Count);
            foreach (var item in value.InformationItems)
            {
                writer.WriteByte(item.InformationType);
                // 先计算内容长度（汉字为两个字节）
                writer.Skip(2, out int position);
                writer.WriteString(item.InformationName);
                ushort length = (ushort)(writer.GetCurrentPosition() - position - 2);
                writer.WriteUInt16Return(length, position);
            }
        }
    }
}
