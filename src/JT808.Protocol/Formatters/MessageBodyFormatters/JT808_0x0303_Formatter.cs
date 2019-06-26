using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0303_Formatter : IJT808MessagePackFormatter<JT808_0x0303>
    {
        public JT808_0x0303 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0303 jT808_0X0303 = new JT808_0x0303();
            jT808_0X0303.InformationType = reader.ReadByte();
            jT808_0X0303.Flag = reader.ReadByte();
            return jT808_0X0303;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0303 value, IJT808Config config)
        {
            writer.WriteByte(value.InformationType);
            writer.WriteByte(value.Flag);
        }
    }
}
