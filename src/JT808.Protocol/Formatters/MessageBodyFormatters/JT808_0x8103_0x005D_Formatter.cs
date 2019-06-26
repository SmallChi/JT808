using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x005D_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x005D>
    {
        public JT808_0x8103_0x005D Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x005D jT808_0x8103_0x005D = new JT808_0x8103_0x005D();
            jT808_0x8103_0x005D.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005D.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005D.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x005D;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x005D value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
