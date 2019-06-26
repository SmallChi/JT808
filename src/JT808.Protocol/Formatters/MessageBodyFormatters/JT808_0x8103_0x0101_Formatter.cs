using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x0101_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x0101>
    {
        public JT808_0x8103_0x0101 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0101 jT808_0x8103_0x0101 = new JT808_0x8103_0x0101();
            jT808_0x8103_0x0101.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0101.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0101.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x0101;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0101 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}