using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8106_Formatter : IJT808MessagePackFormatter<JT808_0x8106>
    {
        public JT808_0x8106 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8106 jT808_0X8106 = new JT808_0x8106();
            jT808_0X8106.ParameterCount = reader.ReadByte();
            jT808_0X8106.Parameters = new uint[jT808_0X8106.ParameterCount];
            for (int i = 0; i < jT808_0X8106.ParameterCount; i++)
            {
                jT808_0X8106.Parameters.SetValue(reader.ReadUInt32(), i);
            }
            return jT808_0X8106;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8106 value, IJT808Config config)
        {
            writer.WriteByte(value.ParameterCount);
            for (int i = 0; i < value.ParameterCount; i++)
            {
                writer.WriteUInt32(value.Parameters[i]);
            }
        }
    }
}
