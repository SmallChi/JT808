using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x002F_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x002F>
    {
        public JT808_0x8103_0x002F Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x002F jT808_0x8103_0x002F = new JT808_0x8103_0x002F();
            jT808_0x8103_0x002F.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x002F.ParamLength = reader.ReadByte();
            jT808_0x8103_0x002F.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x002F;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x002F value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
