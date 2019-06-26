using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8500_Formatter : IJT808MessagePackFormatter<JT808_0x8500>
    {
        public JT808_0x8500 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8500 jT808_0X8500 = new JT808_0x8500();
            jT808_0X8500.ControlFlag = reader.ReadByte();
            return jT808_0X8500;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8500 value, IJT808Config config)
        {
            writer.WriteByte(value.ControlFlag);
        }
    }
}
