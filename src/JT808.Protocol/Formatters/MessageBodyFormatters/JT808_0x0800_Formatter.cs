using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0800_Formatter : IJT808MessagePackFormatter<JT808_0x0800>
    {
        public JT808_0x0800 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0800 jT808_0X0800 = new JT808_0x0800();
            jT808_0X0800.MultimediaId = reader.ReadUInt32();
            jT808_0X0800.MultimediaType = reader.ReadByte();
            jT808_0X0800.MultimediaCodingFormat = reader.ReadByte();
            jT808_0X0800.EventItemCoding = reader.ReadByte();
            jT808_0X0800.ChannelId = reader.ReadByte();
            return jT808_0X0800;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0800 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte(value.MultimediaType);
            writer.WriteByte(value.MultimediaCodingFormat);
            writer.WriteByte(value.EventItemCoding);
            writer.WriteByte(value.ChannelId);
        }
    }
}
