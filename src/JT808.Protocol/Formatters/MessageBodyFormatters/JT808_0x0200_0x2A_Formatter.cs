using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x2A_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x2A>
    {
        public JT808_0x0200_0x2A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x2A jT808LocationAttachImpl0X2A = new JT808_0x0200_0x2A();
            jT808LocationAttachImpl0X2A.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0X2A.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0X2A.IOStatus = reader.ReadUInt16();
            return jT808LocationAttachImpl0X2A;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x2A value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.IOStatus);
        }
    }
}
