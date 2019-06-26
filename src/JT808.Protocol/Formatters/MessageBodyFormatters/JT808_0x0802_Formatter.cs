using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using JT808.Protocol.Metadata;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0802_Formatter : IJT808MessagePackFormatter<JT808_0x0802>
    {
        public JT808_0x0802 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0802 JT808_0x0802 = new JT808_0x0802();
            JT808_0x0802.MsgNum = reader.ReadUInt16();
            JT808_0x0802.MultimediaItemCount = reader.ReadUInt16();
            JT808_0x0802.MultimediaSearchItems = new List<JT808MultimediaSearchProperty>();
            for (var i = 0; i < JT808_0x0802.MultimediaItemCount; i++)
            {
                JT808MultimediaSearchProperty jT808MultimediaSearchProperty = new JT808MultimediaSearchProperty();
                jT808MultimediaSearchProperty.MultimediaId = reader.ReadUInt32();
                jT808MultimediaSearchProperty.MultimediaType = reader.ReadByte();
                jT808MultimediaSearchProperty.ChannelId = reader.ReadByte();
                jT808MultimediaSearchProperty.EventItemCoding = reader.ReadByte();
                JT808MessagePackReader positionReader = new JT808MessagePackReader(reader.ReadArray(28));
                jT808MultimediaSearchProperty.Position = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref positionReader, config);
                JT808_0x0802.MultimediaSearchItems.Add(jT808MultimediaSearchProperty);
            }
            return JT808_0x0802;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0802 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteUInt16((ushort)value.MultimediaSearchItems.Count);
            foreach (var item in value.MultimediaSearchItems)
            {
                writer.WriteUInt32(item.MultimediaId);
                writer.WriteByte(item.MultimediaType);
                writer.WriteByte(item.ChannelId);
                writer.WriteByte(item.EventItemCoding);
                config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer,item.Position, config);
            }
        }
    }
}
