using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0500_Formatter : IJT808MessagePackFormatter<JT808_0x0500>
    {
        public JT808_0x0500 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0500 jT808_0X0500 = new JT808_0x0500();
            jT808_0X0500.MsgNum = reader.ReadUInt16();
            jT808_0X0500.JT808_0x0200 = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref reader, config);
            return jT808_0X0500;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0500 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer, value.JT808_0x0200, config);
        }
    }
}
