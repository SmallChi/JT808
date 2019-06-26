using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x005E_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x005E>
    {
        public JT808_0x8103_0x005E Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x005E jT808_0x8103_0x005E = new JT808_0x8103_0x005E();
            jT808_0x8103_0x005E.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005E.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005E.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x005E;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x005E value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
