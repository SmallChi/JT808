using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0801_Formatter : IJT808MessagePackFormatter<JT808_0x0801>
    {
        public JT808_0x0801 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0801 jT808_0X0801 = new JT808_0x0801();
            jT808_0X0801.MultimediaId = reader.ReadUInt32();
            jT808_0X0801.MultimediaType = reader.ReadByte();
            jT808_0X0801.MultimediaCodingFormat = reader.ReadByte();
            jT808_0X0801.EventItemCoding = reader.ReadByte();
            jT808_0X0801.ChannelId = reader.ReadByte();
            JT808MessagePackReader positionReader = new JT808MessagePackReader(reader.ReadArray(28));
            jT808_0X0801.Position = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref positionReader, config);
            jT808_0X0801.MultimediaDataPackage = reader.ReadContent().ToArray();
            return jT808_0X0801;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0801 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte(value.MultimediaType);
            writer.WriteByte(value.MultimediaCodingFormat);
            writer.WriteByte(value.EventItemCoding);
            writer.WriteByte(value.ChannelId);
            config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, value.Position, config);
            writer.WriteArray(value.MultimediaDataPackage);
        }
    }
}
