using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8300_Formatter : IJT808MessagePackFormatter<JT808_0x8300>
    {
        public JT808_0x8300 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8300 jT808_0X8300 = new JT808_0x8300();
            jT808_0X8300.TextFlag = reader.ReadByte();
            jT808_0X8300.TextInfo = reader.ReadRemainStringContent();
            return jT808_0X8300;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8300 value, IJT808Config config)
        {
            writer.WriteByte(value.TextFlag);
            writer.WriteString(value.TextInfo);
        }
    }
}
