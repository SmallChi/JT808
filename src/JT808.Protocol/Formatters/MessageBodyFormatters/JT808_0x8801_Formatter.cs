using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8801_Formatter : IJT808MessagePackFormatter<JT808_0x8801>
    {
        public JT808_0x8801 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8801 jT808_0X8801 = new JT808_0x8801();
            jT808_0X8801.ChannelId = reader.ReadByte();
            jT808_0X8801.ShootingCommand = reader.ReadUInt16();
            jT808_0X8801.VideoTime = reader.ReadUInt16();
            jT808_0X8801.SaveFlag = reader.ReadByte();
            jT808_0X8801.Resolution = reader.ReadByte();
            jT808_0X8801.VideoQuality = reader.ReadByte();
            jT808_0X8801.Lighting = reader.ReadByte();
            jT808_0X8801.Contrast = reader.ReadByte();
            jT808_0X8801.Saturability = reader.ReadByte();
            jT808_0X8801.Chroma = reader.ReadByte();
            return jT808_0X8801;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8801 value, IJT808Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteUInt16(value.ShootingCommand);
            writer.WriteUInt16(value.VideoTime);
            writer.WriteByte(value.SaveFlag);
            writer.WriteByte(value.Resolution);
            writer.WriteByte(value.VideoQuality);
            writer.WriteByte(value.Lighting);
            writer.WriteByte(value.Contrast);
            writer.WriteByte(value.Saturability);
            writer.WriteByte(value.Chroma);
        }
    }
}
