using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x0082_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x0082>
    {
        public JT808_0x8103_0x0082 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0082 jT808_0x8103_0x0082 = new JT808_0x8103_0x0082();
            jT808_0x8103_0x0082.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0082.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0082.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x0082;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0082 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
