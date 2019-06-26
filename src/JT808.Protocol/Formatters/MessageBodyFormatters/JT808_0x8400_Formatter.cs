using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8400_Formatter : IJT808MessagePackFormatter<JT808_0x8400>
    {
        public JT808_0x8400 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8400 jT808_0X8400 = new JT808_0x8400();
            jT808_0X8400.CallBack = (JT808CallBackType)reader.ReadByte();
            // 最长为 20 字节
            jT808_0X8400.PhoneNumber = reader.ReadRemainStringContent();
            return jT808_0X8400;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8400 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.CallBack);
            writer.WriteString(value.PhoneNumber);
        }
    }
}
