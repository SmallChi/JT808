using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x005B_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x005B>
    {
        public JT808_0x8103_0x005B Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x005B jT808_0x8103_0x005B = new JT808_0x8103_0x005B();
            jT808_0x8103_0x005B.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005B.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005B.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x005B;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x005B value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
