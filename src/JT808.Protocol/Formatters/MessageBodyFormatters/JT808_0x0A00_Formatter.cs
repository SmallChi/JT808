using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0A00_Formatter : IJT808MessagePackFormatter<JT808_0x0A00>
    {
        public JT808_0x0A00 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0A00 jT808_0X0A00 = new JT808_0x0A00
            {
                E = reader.ReadUInt32(),
                N = reader.ReadArray(128).ToArray()
            };
            if (jT808_0X0A00.N.Length != 128)
            {
                throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808_0X0A00.N)}->128");
            }
            return jT808_0X0A00;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0A00 value, IJT808Config config)
        {
            writer.WriteUInt32(value.E);
            if (value.N.Length != 128)
            {
                throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(value.N)}->128");
            }
            writer.WriteArray(value.N);
        }
    }
}
