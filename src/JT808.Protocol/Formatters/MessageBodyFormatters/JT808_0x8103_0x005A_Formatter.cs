using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x005A_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x005A>
    {
        public JT808_0x8103_0x005A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x005A jT808_0x8103_0x005A = new JT808_0x8103_0x005A();
            jT808_0x8103_0x005A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005A.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005A.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x005A;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x005A value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
