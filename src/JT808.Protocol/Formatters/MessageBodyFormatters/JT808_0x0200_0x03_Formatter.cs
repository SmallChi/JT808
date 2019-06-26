using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x03_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x03>
    {
        public JT808_0x0200_0x03 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x03 jT808LocationAttachImpl0x03 = new JT808_0x0200_0x03();
            jT808LocationAttachImpl0x03.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x03.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x03.Speed = reader.ReadUInt16();
            return jT808LocationAttachImpl0x03;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x03 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.Speed);
        }
    }
}
