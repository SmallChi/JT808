using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x31_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x31>
    {
        public JT808_0x0200_0x31 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x31 jT808LocationAttachImpl0x31 = new JT808_0x0200_0x31();
            jT808LocationAttachImpl0x31.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x31.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x31.GNSSCount = reader.ReadByte();
            return jT808LocationAttachImpl0x31;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x31 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte(value.GNSSCount);
        }
    }
}
