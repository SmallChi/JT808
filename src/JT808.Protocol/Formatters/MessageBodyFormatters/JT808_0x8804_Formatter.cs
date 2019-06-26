using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8804_Formatter : IJT808MessagePackFormatter<JT808_0x8804>
    {
        public JT808_0x8804 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8804 jT808_0X8804 = new JT808_0x8804();
            jT808_0X8804.RecordCmd = (JT808RecordCmd)reader.ReadByte();
            jT808_0X8804.RecordTime = reader.ReadUInt16();
            jT808_0X8804.RecordSave = (JT808RecordSave)reader.ReadByte();
            jT808_0X8804.AudioSampleRate = reader.ReadByte();
            return jT808_0X8804;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8804 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.RecordCmd);
            writer.WriteUInt16(value.RecordTime);
            writer.WriteByte((byte)value.RecordSave);
            writer.WriteByte(value.AudioSampleRate);
        }
    }
}
