using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x25_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x25>
    {
        public JT808_0x0200_0x25 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x25 jT808LocationAttachImpl0x13 = new JT808_0x0200_0x25();
            jT808LocationAttachImpl0x13.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x13.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x13.CarSignalStatus = reader.ReadInt32();
            return jT808LocationAttachImpl0x13;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x25 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.CarSignalStatus);
        }
    }
}
