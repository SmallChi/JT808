using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using JT808.Protocol.Extensions;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 驾驶员身份信息采集上报
    /// </summary>
    public class JT808_0x0702 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0702>, IJT808Analyze, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x0702;
        public override string Description => "驾驶员身份信息采集上报";
        /// <summary>
        /// 状态
        /// 0x01：从业资格证 IC 卡插入（驾驶员上班）；
        /// 0x02：从业资格证 IC 卡拔出（驾驶员下班）。
        /// </summary>
        public JT808ICCardStatus IC_Card_Status { get; set; }
        /// <summary>
        /// 插卡/拔卡时间，YY-MM-DD-hh-mm-ss；
        /// 以下字段在状态为 0x01 时才有效并做填充。
        /// BCD[6]
        /// </summary>
        public DateTime IC_Card_PlugDateTime { get; set; }
        /// <summary>
        /// IC 卡读取结果
        /// 0x00：IC 卡读卡成功；
        /// 0x01：读卡失败，原因为卡片密钥认证未通过；
        /// 0x02：读卡失败，原因为卡片已被锁定；
        /// 0x03：读卡失败，原因为卡片被拔出；
        /// 0x04：读卡失败，原因为数据校验错误。
        /// 以下字段在 IC 卡读取结果等于 0x00 时才有效。
        /// </summary>
        public JT808ICCardReadResult IC_Card_ReadResult { get; set; }
        /// <summary>
        /// 驾驶员姓名长度
        /// </summary>
        public byte DriverUserNameLength { get; set; }
        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string DriverUserName { get; set; }
        /// <summary>
        /// 从业资格证编码
        /// 长度 20 位，不足补 0x00。
        /// </summary>
        public string QualificationCode { get; set; }
        /// <summary>
        /// 发证机构名称长度
        /// </summary>
        public byte LicenseIssuingLength { get; set; }
        /// <summary>
        /// 发证机构名称
        /// </summary>
        public string LicenseIssuing { get; set; }
        /// <summary>
        /// 证件有效期 BCD[4]
        /// </summary>
        public DateTime CertificateExpiresDate { get; set; }
        /// <summary>
        /// 驾驶员身份证号 长度20 不足补0
        /// 2019版本
        /// </summary>
        public string DriverIdentityCard { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0702 value = new JT808_0x0702();
            value.IC_Card_Status = (JT808ICCardStatus)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.IC_Card_Status).ReadNumber()}]状态-{value.IC_Card_Status.ToString()}", (byte)value.IC_Card_Status);
            value.IC_Card_PlugDateTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.IC_Card_PlugDateTime.ToString("yyMMddHHmmss")}]插拔卡时间", value.IC_Card_PlugDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            if (value.IC_Card_Status == JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班)
            {
                value.IC_Card_ReadResult = (JT808ICCardReadResult)reader.ReadByte();
                writer.WriteNumber($"[{((byte)value.IC_Card_ReadResult).ReadNumber()}]IC卡读取结果-{value.IC_Card_ReadResult.ToString()}", (byte)value.IC_Card_ReadResult);
                if (value.IC_Card_ReadResult == JT808ICCardReadResult.IC卡读卡成功)
                {
                    value.DriverUserNameLength = reader.ReadByte();
                    writer.WriteNumber($"[{value.DriverUserNameLength.ReadNumber()}]驾驶员姓名长度", value.DriverUserNameLength);
                    var driverUserNameBuffer= reader.ReadVirtualArray(value.DriverUserNameLength);
                    value.DriverUserName = reader.ReadString(value.DriverUserNameLength);
                    writer.WriteString($"[{driverUserNameBuffer.ToArray().ToHexString()}]驾驶员姓名", value.DriverUserName);
                    var qualificationCodeBuffer = reader.ReadVirtualArray(20);
                    value.QualificationCode = reader.ReadString(20);
                    writer.WriteString($"[{qualificationCodeBuffer.ToArray().ToHexString()}]从业资格证编码", value.QualificationCode);
                    value.LicenseIssuingLength = reader.ReadByte();
                    writer.WriteNumber($"[{value.LicenseIssuingLength.ReadNumber()}]发证机构名称长度", value.LicenseIssuingLength);
                    var licenseIssuingLengtheBuffer = reader.ReadVirtualArray(value.LicenseIssuingLength);
                    value.LicenseIssuing = reader.ReadString(value.LicenseIssuingLength);
                    writer.WriteString($"[{licenseIssuingLengtheBuffer.ToArray().ToHexString()}]发证机构名称", value.LicenseIssuing);
                    value.CertificateExpiresDate = reader.ReadDateTime4();
                    writer.WriteString($"[{value.CertificateExpiresDate.ToString("yyMMdd")}]插拔卡时间", value.CertificateExpiresDate.ToString("yyyy-MM-dd"));
                    if (reader.Version == JT808Version.JTT2019)
                    {
                        var driverIdentityCardBuffer = reader.ReadVirtualArray(20);
                        value.DriverIdentityCard = reader.ReadString(20);
                        writer.WriteString($"[{driverIdentityCardBuffer.ToArray().ToHexString()}]驾驶员身份证号", value.DriverIdentityCard);
                    }
                }
            }
        }

        public JT808_0x0702 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0702 value = new JT808_0x0702();
            value.IC_Card_Status = (JT808ICCardStatus)reader.ReadByte();
            value.IC_Card_PlugDateTime = reader.ReadDateTime6();
            if (value.IC_Card_Status == JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班)
            {
                value.IC_Card_ReadResult = (JT808ICCardReadResult)reader.ReadByte();
                if (value.IC_Card_ReadResult == JT808ICCardReadResult.IC卡读卡成功)
                {
                    value.DriverUserNameLength = reader.ReadByte();
                    value.DriverUserName = reader.ReadString(value.DriverUserNameLength);
                    value.QualificationCode = reader.ReadString(20);
                    value.LicenseIssuingLength = reader.ReadByte();
                    value.LicenseIssuing = reader.ReadString(value.LicenseIssuingLength);
                    value.CertificateExpiresDate = reader.ReadDateTime4();
                    if(reader.Version== JT808Version.JTT2019)
                    {
                        value.DriverIdentityCard = reader.ReadString(20);
                    }
                }
            }
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0702 value, IJT808Config config)
        {
            writer.WriteByte((byte)value.IC_Card_Status);
            writer.WriteDateTime6(value.IC_Card_PlugDateTime);
            if (value.IC_Card_Status == JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班)
            {
                writer.WriteByte((byte)value.IC_Card_ReadResult);
                if (value.IC_Card_ReadResult == JT808ICCardReadResult.IC卡读卡成功)
                {
                    writer.WriteByte((byte)value.DriverUserName.Length);
                    writer.WriteString(value.DriverUserName);
                    writer.WriteString(value.QualificationCode.PadRight(20, '0'));
                    writer.WriteByte((byte)value.LicenseIssuing.Length);
                    writer.WriteString(value.LicenseIssuing);
                    writer.WriteDateTime4(value.CertificateExpiresDate);
                    if (writer.Version == JT808Version.JTT2019)
                    {
                        writer.WriteString(value.DriverIdentityCard.PadRight(20,'0'));
                    }
                }
            }
        }
    }
}
