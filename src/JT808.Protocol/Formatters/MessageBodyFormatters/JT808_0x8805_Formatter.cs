using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8805_Formatter : IJT808MessagePackFormatter<JT808_0x8805>
    {
        public JT808_0x8805 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8805 jT808_0X8805 = new JT808_0x8805();
            jT808_0X8805.MultimediaId = reader.ReadUInt32();
            jT808_0X8805.MultimediaDeleted = reader.ReadByte();
            return jT808_0X8805;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8805 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte(value.MultimediaDeleted);
        }
    }
}
