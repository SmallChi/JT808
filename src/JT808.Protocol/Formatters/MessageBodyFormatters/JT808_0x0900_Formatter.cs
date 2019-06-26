using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0900_Formatter : IJT808MessagePackFormatter<JT808_0x0900>
    {
        public JT808_0x0900 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0900 jT808_0X0900 = new JT808_0x0900();
            jT808_0X0900.PassthroughType = reader.ReadByte();
            jT808_0X0900.PassthroughData = reader.ReadContent().ToArray(); ;
            return jT808_0X0900;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0900 value, IJT808Config config)
        {
            writer.WriteByte(value.PassthroughType);
            object obj = config.GetMessagePackFormatterByType(value.JT808_0x0900_BodyBase.GetType());
            JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(obj, ref writer, value.JT808_0x0900_BodyBase, config);
        }
    }
}
