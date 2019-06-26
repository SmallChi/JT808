using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0901_Formatter : IJT808MessagePackFormatter<JT808_0x0901>
    {
        public JT808_0x0901 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0901 jT808_0X0901 = new JT808_0x0901();
            var compressMessageLength = reader.ReadUInt32();
            var data = reader.ReadArray((int)compressMessageLength);
            jT808_0X0901.UnCompressMessage = config.Compress.Decompress(data.ToArray());
            jT808_0X0901.UnCompressMessageLength = (uint)jT808_0X0901.UnCompressMessage.Length;
            return jT808_0X0901;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0901 value, IJT808Config config)
        {
            var data = config.Compress.Compress(value.UnCompressMessage);
            writer.WriteUInt32((uint)data.Length);
            writer.WriteArray(data);
        }
    }
}
