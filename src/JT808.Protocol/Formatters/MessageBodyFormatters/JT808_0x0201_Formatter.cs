using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0201_Formatter : IJT808MessagePackFormatter<JT808_0x0201>
    {
        public JT808_0x0201 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0201 jT808_0X0201 = new JT808_0x0201();
            jT808_0X0201.MsgNum = reader.ReadUInt16();
            jT808_0X0201.Position = config.GetMessagePackFormatter<JT808_0x0200>().Deserialize(ref reader, config);
            return jT808_0X0201;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0201 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            config.GetMessagePackFormatter<JT808_0x0200>().Serialize(ref writer,value.Position, config);
        }
    }
}
