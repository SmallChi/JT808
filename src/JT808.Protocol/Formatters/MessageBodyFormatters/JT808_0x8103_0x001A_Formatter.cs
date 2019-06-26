using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x001A_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x001A>
    {
        public JT808_0x8103_0x001A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x001A jT808_0x8103_0x001A = new JT808_0x8103_0x001A();
            jT808_0x8103_0x001A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x001A.ParamLength = reader.ReadByte();
            jT808_0x8103_0x001A.ParamValue = reader.ReadString( jT808_0x8103_0x001A.ParamLength);
            return jT808_0x8103_0x001A;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x001A value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}