using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0301_Formatter : IJT808MessagePackFormatter<JT808_0x0301>
    {
        public JT808_0x0301 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0301 jT808_0X0301 = new JT808_0x0301();
            jT808_0X0301.EventId = reader.ReadByte();
            return jT808_0X0301;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0301 value, IJT808Config config)
        {
            writer.WriteByte(value.EventId);
        }
    }
}
