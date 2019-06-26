using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8803_Formatter : IJT808MessagePackFormatter<JT808_0x8803>
    {
        public JT808_0x8803 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8803 jT808_0X8803 = new JT808_0x8803();
            jT808_0X8803.MultimediaType = reader.ReadByte();
            jT808_0X8803.ChannelId = reader.ReadByte();
            jT808_0X8803.EventItemCoding = reader.ReadByte();
            jT808_0X8803.StartTime = reader.ReadDateTime6();
            jT808_0X8803.EndTime = reader.ReadDateTime6();
            jT808_0X8803.MultimediaDeleted = reader.ReadByte();
            return jT808_0X8803;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8803 value, IJT808Config config)
        {
            writer.WriteByte(value.MultimediaType);
            writer.WriteByte(value.ChannelId);
            writer.WriteByte(value.EventItemCoding);
            writer.WriteDateTime6(value.StartTime);
            writer.WriteDateTime6(value.EndTime);
            writer.WriteByte(value.MultimediaDeleted);
        }
    }
}
