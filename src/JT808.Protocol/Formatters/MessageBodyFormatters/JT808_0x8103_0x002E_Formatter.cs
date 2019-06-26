using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x002E_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x002E>
    {
        public JT808_0x8103_0x002E Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x002E jT808_0x8103_0x002E = new JT808_0x8103_0x002E();
            jT808_0x8103_0x002E.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x002E.ParamLength = reader.ReadByte();
            jT808_0x8103_0x002E.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x002E;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x002E value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
