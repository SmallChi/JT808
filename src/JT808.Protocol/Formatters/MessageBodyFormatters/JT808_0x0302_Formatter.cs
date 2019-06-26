using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0302_Formatter : IJT808MessagePackFormatter<JT808_0x0302>
    {
        public JT808_0x0302 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0302 jT808_0X0302 = new JT808_0x0302();
            jT808_0X0302.ReplySNo = reader.ReadUInt16();
            jT808_0X0302.AnswerId = reader.ReadByte();
            return jT808_0X0302;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0302 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplySNo);
            writer.WriteByte(value.AnswerId);
        }
    }
}
