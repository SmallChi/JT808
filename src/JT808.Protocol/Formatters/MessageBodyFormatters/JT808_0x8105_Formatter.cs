using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8105_Formatter : IJT808MessagePackFormatter<JT808_0x8105>
    {
        public JT808_0x8105 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8105 jT808_0x8105 = new JT808_0x8105
            {
                CommandWord = reader.ReadByte()
            };
            if (jT808_0x8105.CommandWord == 1 || jT808_0x8105.CommandWord == 2)
            {
                jT808_0x8105.CommandValue = new CommandParams();
                jT808_0x8105.CommandValue.SetCommandParams(reader.ReadRemainStringContent());
            }
            return jT808_0x8105;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8105 value, IJT808Config config)
        {
            writer.WriteByte(value.CommandWord);
            if (value.CommandWord == 1 || value.CommandWord == 2)
            {
                writer.WriteString(value.CommandValue.ToString());
            }
        }
    }
}
