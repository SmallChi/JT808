using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x12_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x12>
    {
        public JT808_0x0200_0x12 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x12 jT808LocationAttachImpl0x12 = new JT808_0x0200_0x12();
            jT808LocationAttachImpl0x12.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x12.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x12.JT808PositionType = (JT808PositionType)reader.ReadByte();
            jT808LocationAttachImpl0x12.AreaId = reader.ReadInt32();
            jT808LocationAttachImpl0x12.Direction = (JT808DirectionType)reader.ReadByte();
            return jT808LocationAttachImpl0x12;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x12 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte((byte)value.JT808PositionType);
            writer.WriteInt32(value.AreaId);
            writer.WriteByte((byte)value.Direction);
        }
    }
}
