using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8108_Formatter : IJT808MessagePackFormatter<JT808_0x8108>
    {
        public JT808_0x8108 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8108 jT808_0X8108 = new JT808_0x8108();
            jT808_0X8108.UpgradeType = (JT808UpgradeType)reader.ReadByte();
            jT808_0X8108.MakerId = reader.ReadString(5);
            jT808_0X8108.VersionNumLength = reader.ReadByte();
            jT808_0X8108.VersionNum = reader.ReadString(jT808_0X8108.VersionNumLength);
            jT808_0X8108.UpgradePackageLength = reader.ReadInt32();
            jT808_0X8108.UpgradePackage = reader.ReadArray(jT808_0X8108.UpgradePackageLength).ToArray();
            return jT808_0X8108;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8108 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.UpgradeType);
            writer.WriteString(value.MakerId.PadRight(5, '0'));
            writer.WriteByte((byte)value.VersionNum.Length);
            writer.WriteString(value.VersionNum);
            writer.WriteInt32(value.UpgradePackage.Length);
            writer.WriteArray(value.UpgradePackage);
        }
    }
}
