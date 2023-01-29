using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 下发终端升级包
    /// </summary>
    public class JT808_0x8108 : JT808MessagePackFormatter<JT808_0x8108>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8108
        /// </summary>
        public ushort MsgId  => 0x8108;
        /// <summary>
        /// 下发终端升级包
        /// </summary>
        public string Description => "下发终端升级包";
        /// <summary>
        /// 升级类型
        /// </summary>
        public JT808UpgradeType UpgradeType { get; set; }
        /// <summary>
        /// 制造商 ID
        /// 2013版本 5 个字节，终端制造商编码
        /// 2019版本 11 个字节，终端制造商编码
        /// </summary>
        public string MakerId { get; set; }
        /// <summary>
        /// 版本号长度
        /// </summary>
        public byte VersionNumLength { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string VersionNum { get; set; }
        /// <summary>
        /// 升级数据包长度
        /// </summary>
        public int UpgradePackageLength { get; set; }
        /// <summary>
        /// 升级数据包
        /// </summary>
        public byte[] UpgradePackage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8108 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8108 jT808_0X8108 = new JT808_0x8108();
            jT808_0X8108.UpgradeType = (JT808UpgradeType)reader.ReadByte();
            if (reader.Version == JT808Version.JTT2019)
            {
                jT808_0X8108.MakerId = reader.ReadString(11);
            }
            else
            {
                jT808_0X8108.MakerId = reader.ReadString(5);
            }
            jT808_0X8108.VersionNumLength = reader.ReadByte();
            jT808_0X8108.VersionNum = reader.ReadString(jT808_0X8108.VersionNumLength);
            jT808_0X8108.UpgradePackageLength = reader.ReadInt32();
            jT808_0X8108.UpgradePackage = reader.ReadArray(jT808_0X8108.UpgradePackageLength).ToArray();
            return jT808_0X8108;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8108 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.UpgradeType);
            if (writer.Version == JT808Version.JTT2019)
            {
                writer.WriteString(value.MakerId.PadLeft(11, '\0'));
            }
            else
            {
                writer.WriteString(value.MakerId.PadRight(5, '\0'));
            }
            writer.WriteByte((byte)value.VersionNum.Length);
            writer.WriteString(value.VersionNum);
            writer.WriteInt32(value.UpgradePackage.Length);
            writer.WriteArray(value.UpgradePackage);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8108 value = new JT808_0x8108();
            value.UpgradeType = (JT808UpgradeType)reader.ReadByte();
            writer.WriteNumber($"[{ ((byte)value.UpgradeType).ReadNumber()}]升级类型-{value.UpgradeType.ToString()}", (byte)value.UpgradeType);
            if (reader.Version == JT808Version.JTT2019)
            {
                var makerIdBuffer = reader.ReadVirtualArray(11).ToArray();
                value.MakerId = reader.ReadString(11);
                writer.WriteString($"[{makerIdBuffer.ToHexString()}]制造商ID", value.MakerId);
            }
            else
            {
                var makerIdBuffer = reader.ReadVirtualArray(5).ToArray();
                value.MakerId = reader.ReadString(5);
                writer.WriteString($"[{makerIdBuffer.ToHexString()}]制造商ID", value.MakerId);
            }
            value.VersionNumLength = reader.ReadByte();
            writer.WriteNumber($"[{value.VersionNumLength.ReadNumber()}]版本号长度",value.VersionNumLength);
            var versionNumBuffer = reader.ReadVirtualArray(value.VersionNumLength).ToArray();
            value.VersionNum = reader.ReadString(value.VersionNumLength);
            writer.WriteString($"[{versionNumBuffer.ToHexString()}]版本号", value.VersionNum);
            value.UpgradePackageLength = reader.ReadInt32();
            writer.WriteNumber($"[{value.UpgradePackageLength.ReadNumber()}]升级数据包长度", value.UpgradePackageLength);
            value.UpgradePackage = reader.ReadArray(value.UpgradePackageLength).ToArray();
            writer.WriteString($"升级数据包", value.UpgradePackage.ToHexString());
        }
    }
}
