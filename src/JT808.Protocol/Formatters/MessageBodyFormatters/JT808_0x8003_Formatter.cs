using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8003_Formatter : IJT808MessagePackFormatter<JT808_0x8003>
    {
        public JT808_0x8003 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8003 jT808_0X8003 = new JT808_0x8003();
            jT808_0X8003.OriginalMsgNum = reader.ReadUInt16();
            jT808_0X8003.AgainPackageCount = reader.ReadByte();
            jT808_0X8003.AgainPackageData = reader.ReadArray(jT808_0X8003.AgainPackageCount * 2).ToArray();
            return jT808_0X8003;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8003 value, IJT808Config config)
        {
            writer.WriteUInt16(value.OriginalMsgNum);
            writer.WriteByte((byte)(value.AgainPackageData.Length / 2));
            writer.WriteArray(value.AgainPackageData);
        }
    }
}
