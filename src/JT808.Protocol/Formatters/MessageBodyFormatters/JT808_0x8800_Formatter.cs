using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8800_Formatter : IJT808MessagePackFormatter<JT808_0x8800>
    {
        public JT808_0x8800 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8800 jT808_0X8800 = new JT808_0x8800();
            jT808_0X8800.MultimediaId = reader.ReadUInt32();
            jT808_0X8800.RetransmitPackageCount = reader.ReadByte();
            jT808_0X8800.RetransmitPackageIds = reader.ReadArray(jT808_0X8800.RetransmitPackageCount * 2).ToArray();
            return jT808_0X8800;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8800 value, IJT808Config config)
        {
            writer.WriteUInt32(value.MultimediaId);
            writer.WriteByte((byte)(value.RetransmitPackageIds.Length / 2));
            writer.WriteArray(value.RetransmitPackageIds);
        }
    }
}
