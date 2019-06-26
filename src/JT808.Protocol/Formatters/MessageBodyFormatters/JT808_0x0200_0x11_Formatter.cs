using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x11_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x11>
    {
        public JT808_0x0200_0x11 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x11 jT808LocationAttachImpl0x11 = new JT808_0x0200_0x11();
            jT808LocationAttachImpl0x11.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x11.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x11.JT808PositionType = (JT808PositionType)reader.ReadByte();
            if(jT808LocationAttachImpl0x11.JT808PositionType != JT808PositionType.无特定位置)
            {
                jT808LocationAttachImpl0x11.AreaId = reader.ReadInt32();
            }
            return jT808LocationAttachImpl0x11;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x11 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte((byte)value.JT808PositionType);
            if (value.JT808PositionType != JT808PositionType.无特定位置)
            {
                writer.WriteInt32(value.AreaId);
            }
        }
    }
}
