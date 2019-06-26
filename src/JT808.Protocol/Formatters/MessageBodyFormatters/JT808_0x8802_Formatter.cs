using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8802_Formatter : IJT808MessagePackFormatter<JT808_0x8802>
    {
        public JT808_0x8802 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8802 jT808_0X8802 = new JT808_0x8802();
            jT808_0X8802.MultimediaType = reader.ReadByte();
            jT808_0X8802.ChannelId = reader.ReadByte();
            jT808_0X8802.EventItemCoding = reader.ReadByte();
            jT808_0X8802.StartTime = reader.ReadDateTime6();
            jT808_0X8802.EndTime = reader.ReadDateTime6();
            return jT808_0X8802;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8802 value, IJT808Config config)
        {
            writer.WriteByte(value.MultimediaType);
            writer.WriteByte(value.ChannelId);
            writer.WriteByte(value.EventItemCoding);
            writer.WriteDateTime6(value.StartTime);
            writer.WriteDateTime6(value.EndTime);
        }
    }
}
