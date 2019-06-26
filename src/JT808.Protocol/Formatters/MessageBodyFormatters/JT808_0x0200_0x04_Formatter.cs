using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x04_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x04>
    {
        public JT808_0x0200_0x04 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x04 jT808LocationAttachImpl0x04 = new JT808_0x0200_0x04();
            jT808LocationAttachImpl0x04.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x04.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x04.EventId = reader.ReadUInt16();
            return jT808LocationAttachImpl0x04;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x04 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.EventId);
        }
    }
}
