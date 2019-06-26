using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x0018_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x0018>
    {
        public JT808_0x8103_0x0018 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0018 jT808_0x8103_0x0018 = new JT808_0x8103_0x0018();
            jT808_0x8103_0x0018.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0018.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0018.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0018;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0018 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
