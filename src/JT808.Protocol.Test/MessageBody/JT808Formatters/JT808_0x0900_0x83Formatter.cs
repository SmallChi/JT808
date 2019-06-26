using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Test.JT808_0x0900_BodiesImpl;
using System;

namespace JT808.Protocol.Test.MessageBody.JT808Formatters
{
    public class JT808_0x0900_0x83Formatter : IJT808MessagePackFormatter<JT808_0x0900_0x83>
    {
        public JT808_0x0900_0x83 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0900_0x83 jT808PassthroughType0x83 = new JT808_0x0900_0x83();
            jT808PassthroughType0x83.PassthroughContent = reader.ReadRemainStringContent();
            return jT808PassthroughType0x83;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0900_0x83 value, IJT808Config config)
        {
            writer.WriteString(value.PassthroughContent);
        }
    }
}
