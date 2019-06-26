using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0200_0x01_Formatter : IJT808MessagePackFormatter<JT808_0x0200_0x01>
    {
        public JT808_0x0200_0x01 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x01 jT808LocationAttachImpl0X01 = new JT808_0x0200_0x01();
            jT808LocationAttachImpl0X01.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0X01.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0X01.Mileage = reader.ReadInt32();
            return jT808LocationAttachImpl0X01;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x01 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.Mileage);
        }
    }
}
