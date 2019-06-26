using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0108_Formatter : IJT808MessagePackFormatter<JT808_0x0108>
    {
        public JT808_0x0108 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0108 jT808_0X0108 = new JT808_0x0108();
            jT808_0X0108.UpgradeType = (JT808UpgradeType)reader.ReadByte();
            jT808_0X0108.UpgradeResult = (JT808UpgradeResult)reader.ReadByte();
            return jT808_0X0108;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0108 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.UpgradeType);
            writer.WriteByte((byte)value.UpgradeResult);
        }
    }
}
