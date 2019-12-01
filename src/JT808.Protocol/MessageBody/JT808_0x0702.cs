using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 驾驶员身份信息采集上报
    /// </summary>
    public class JT808_0x0702 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0702>, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x0702;
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
        /// 发证机构名称长度
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
        public JT808_0x0702 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702();
            jT808_0X0702.IC_Card_Status = (JT808ICCardStatus)reader.ReadByte();
            jT808_0X0702.IC_Card_PlugDateTime = reader.ReadDateTime6();
            if (jT808_0X0702.IC_Card_Status == JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班)
            {
                jT808_0X0702.IC_Card_ReadResult = (JT808ICCardReadResult)reader.ReadByte();
                if (jT808_0X0702.IC_Card_ReadResult == JT808ICCardReadResult.IC卡读卡成功)
                {
                    jT808_0X0702.DriverUserNameLength = reader.ReadByte();
                    jT808_0X0702.DriverUserName = reader.ReadString(jT808_0X0702.DriverUserNameLength);
                    jT808_0X0702.QualificationCode = reader.ReadString(20);
                    jT808_0X0702.LicenseIssuingLength = reader.ReadByte();
                    jT808_0X0702.LicenseIssuing = reader.ReadString(jT808_0X0702.LicenseIssuingLength);
                    jT808_0X0702.CertificateExpiresDate = reader.ReadDateTime4();
                    if(reader.Version== JT808Version.JTT2019)
                    {
                        jT808_0X0702.DriverIdentityCard = reader.ReadString(20);
                    }
                }
            }
            return jT808_0X0702;
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
