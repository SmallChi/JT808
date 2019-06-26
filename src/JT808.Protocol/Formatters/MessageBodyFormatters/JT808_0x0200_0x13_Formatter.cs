using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x13_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x13>
    {
        public JT808_0x0200_0x13 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x13 jT808LocationAttachImpl0x13 = new JT808_0x0200_0x13();
            jT808LocationAttachImpl0x13.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x13.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x13.DrivenRouteId = reader.ReadInt32();
            jT808LocationAttachImpl0x13.Time = reader.ReadUInt16();
            jT808LocationAttachImpl0x13.DrivenRoute = (JT808DrivenRouteType)reader.ReadByte();
            return jT808LocationAttachImpl0x13;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x13 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.DrivenRouteId);
            writer.WriteUInt16(value.Time);
            writer.WriteByte((byte)value.DrivenRoute);
        }
    }
}
