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
    public class JT808_0x0702 : JT808MessagePackFormatter<JT808_0x0702>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x0702
        /// </summary>
        public ushort MsgId  => 0x0702;
        /// <summary>
        /// Description
        /// </summary>
        public string Description => "驾驶员身份信息采集上报";
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
        /// 2011 长度40 位 ，不足补 '\0'；
        /// 2013 长度 20 位，不足补 '\0'。
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
        /// 驾驶员身份证号 长度20 不足补 '\0'
        /// 2011版本
        /// 2019版本
        /// </summary>
        public string DriverIdentityCard { get; set; }
        /// <summary>
        /// 人脸匹配度
        /// 身份证或从业资格证照片与人脸匹配度比例：0~100
        /// 2019版本
        /// </summary>
        public byte? FaceMatchValue { get; set; }
        /// <summary>
        /// 身份证 UID
        /// 长度 20 位，不足补0x00
        /// 2019版本
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0702 value = new JT808_0x0702();
            var firstByte = reader.ReadVirtualByte();
            //因2011第一个字节代表姓名长度 所以该值长度只能为  2，3，4，整个数据长度 62+m+n
            if (firstByte == 0x01)
            {
                value.IC_Card_Status = (JT808ICCardStatus)reader.ReadByte();
                writer.WriteNumber($"[{((byte)value.IC_Card_Status).ReadNumber()}]状态-{value.IC_Card_Status.ToString()}", (byte)value.IC_Card_Status);
                value.IC_Card_PlugDateTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{value.IC_Card_PlugDateTime.ToString("yyMMddHHmmss")}]插拔卡时间", value.IC_Card_PlugDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                if (value.IC_Card_Status == JT808ICCardStatus.ic_card_into)
                {
                    value.IC_Card_ReadResult = (JT808ICCardReadResult)reader.ReadByte();
                    writer.WriteNumber($"[{((byte)value.IC_Card_ReadResult).ReadNumber()}]IC卡读取结果-{value.IC_Card_ReadResult.ToString()}", (byte)value.IC_Card_ReadResult);
                    if (value.IC_Card_ReadResult == JT808ICCardReadResult.ic_card_reading_succeeded)
                    {
                        value.DriverUserNameLength = reader.ReadByte();
                        writer.WriteNumber($"[{value.DriverUserNameLength.ReadNumber()}]驾驶员姓名长度", value.DriverUserNameLength);
                        var driverUserNameBuffer = reader.ReadVirtualArray(value.DriverUserNameLength);
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
                        value.CertificateExpiresDate = reader.ReadDateTime_YYYYMMDD();
                        writer.WriteString($"[{value.CertificateExpiresDate.ToString("yyMMdd")}]插拔卡时间", value.CertificateExpiresDate.ToString("yyyy-MM-dd"));
                        if (reader.Version == JT808Version.JTT2019)
                        {
                            var driverIdentityCardBuffer = reader.ReadVirtualArray(20);
                            value.DriverIdentityCard = reader.ReadString(20);
                            writer.WriteString($"[{driverIdentityCardBuffer.ToArray().ToHexString()}]驾驶员身份证号", value.DriverIdentityCard);
                            //兼容808-2019 补充
                            if (reader.ReadCurrentRemainContentLength() > 0)
                            {
                                value.FaceMatchValue = reader.ReadByte();
                                writer.WriteNumber($"[{value.FaceMatchValue.Value.ReadNumber()}]人脸匹配度", value.FaceMatchValue.Value);
                                var uidBuffer = reader.ReadVirtualArray(20);
                                value.UID = reader.ReadString(20);
                                writer.WriteString($"[{uidBuffer.ToArray().ToHexString()}]身份证UID", value.UID);
                            }
                        }
                    }
                }
            }
            else
            {
                if (firstByte == 0x02 && reader.ReadCurrentRemainContentLength() == 7)
                {
                    //如果字节是0x02且长度只有7，那么该协议就是2013或者2019
                    value.IC_Card_Status = (JT808ICCardStatus)reader.ReadByte();
                    writer.WriteNumber($"[{((byte)value.IC_Card_Status).ReadNumber()}]状态-{value.IC_Card_Status.ToString()}", (byte)value.IC_Card_Status);
                    value.IC_Card_PlugDateTime = reader.ReadDateTime_yyMMddHHmmss();
                    writer.WriteString($"[{value.IC_Card_PlugDateTime.ToString("yyMMddHHmmss")}]插拔卡时间", value.IC_Card_PlugDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    value.DriverUserNameLength = reader.ReadByte();
                    writer.WriteNumber($"[{value.DriverUserNameLength.ReadNumber()}]驾驶员姓名长度", value.DriverUserNameLength);
                    var driverUserNameBuffer = reader.ReadVirtualArray(value.DriverUserNameLength);
                    value.DriverUserName = reader.ReadString(value.DriverUserNameLength);
                    writer.WriteString($"[{driverUserNameBuffer.ToArray().ToHexString()}]驾驶员姓名", value.DriverUserName);
                    var driverIdentityCardBuffer = reader.ReadVirtualArray(20);
                    value.DriverIdentityCard = reader.ReadString(20);
                    writer.WriteString($"[{driverIdentityCardBuffer.ToArray().ToHexString()}]驾驶员身份证号", value.DriverIdentityCard);
                    var qualificationCodeBuffer = reader.ReadVirtualArray(40);
                    value.QualificationCode = reader.ReadString(40);
                    writer.WriteString($"[{qualificationCodeBuffer.ToArray().ToHexString()}]从业资格证编码", value.QualificationCode);
                    value.LicenseIssuingLength = reader.ReadByte();
                    writer.WriteNumber($"[{value.LicenseIssuingLength.ReadNumber()}]发证机构名称长度", value.LicenseIssuingLength);
                    var licenseIssuingBuffer = reader.ReadVirtualArray(value.LicenseIssuingLength);
                    value.LicenseIssuing = reader.ReadString(value.LicenseIssuingLength);
                    writer.WriteString($"[{licenseIssuingBuffer.ToArray().ToHexString()}]发证机构名称", value.LicenseIssuing);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0702 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0702 value = new JT808_0x0702();
            var firstByte = reader.ReadVirtualByte();
            //因2011第一个字节代表姓名长度 所以该值长度只能为  2，3，4，整个数据长度 62+m+n
            if (firstByte == 0x01)
            {
                value.IC_Card_Status = (JT808ICCardStatus)reader.ReadByte();
                value.IC_Card_PlugDateTime = reader.ReadDateTime_yyMMddHHmmss();
                if (value.IC_Card_Status == JT808ICCardStatus.ic_card_into)
                {
                    value.IC_Card_ReadResult = (JT808ICCardReadResult)reader.ReadByte();
                    if (value.IC_Card_ReadResult == JT808ICCardReadResult.ic_card_reading_succeeded)
                    {
                        value.DriverUserNameLength = reader.ReadByte();
                        value.DriverUserName = reader.ReadString(value.DriverUserNameLength);
                        value.QualificationCode = reader.ReadString(20);
                        value.LicenseIssuingLength = reader.ReadByte();
                        value.LicenseIssuing = reader.ReadString(value.LicenseIssuingLength);
                        value.CertificateExpiresDate = reader.ReadDateTime_YYYYMMDD();
                        if (reader.Version == JT808Version.JTT2019)
                        {
                            value.DriverIdentityCard = reader.ReadString(20);
                            //兼容808-2019 补充
                            if (reader.ReadCurrentRemainContentLength() > 0)
                            {
                                value.FaceMatchValue = reader.ReadByte();
                                value.UID = reader.ReadString(20);
                            }
                        }
                    }
                }
            }
            else 
            {
                if (firstByte == 0x02 && reader.ReadCurrentRemainContentLength() == 7)
                {
                    //如果字节是0x02且长度只有7，那么该协议就是2013或者2019
                    value.IC_Card_Status = (JT808ICCardStatus)reader.ReadByte();
                    value.IC_Card_PlugDateTime = reader.ReadDateTime_yyMMddHHmmss();
                }
                else {
                    value.DriverUserNameLength = reader.ReadByte();
                    value.DriverUserName = reader.ReadString(value.DriverUserNameLength);
                    value.DriverIdentityCard = reader.ReadString(20);
                    value.QualificationCode = reader.ReadString(40);
                    value.LicenseIssuingLength = reader.ReadByte();
                    value.LicenseIssuing = reader.ReadString(value.LicenseIssuingLength);
                }            
            }       
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0702 value, IJT808Config config)
        {
            if (writer.Version == JT808Version.JTT2011)
            {
                writer.WriteByte((byte)value.DriverUserName.Length);
                writer.WriteString(value.DriverUserName);
                writer.WriteString(value.DriverIdentityCard.PadLeft(20,'\0').ValiString(nameof(value.DriverIdentityCard), 20));
                writer.WriteString(value.QualificationCode.PadLeft(40, '\0').ValiString(nameof(value.QualificationCode), 40));
                writer.WriteByte((byte)value.LicenseIssuing.Length);
                writer.WriteString(value.LicenseIssuing);
            }
            else {
                writer.WriteByte((byte)value.IC_Card_Status);
                writer.WriteDateTime_yyMMddHHmmss(value.IC_Card_PlugDateTime);
                if (value.IC_Card_Status == JT808ICCardStatus.ic_card_into)
                {
                    writer.WriteByte((byte)value.IC_Card_ReadResult);
                    if (value.IC_Card_ReadResult == JT808ICCardReadResult.ic_card_reading_succeeded)
                    {
                        writer.WriteByte((byte)value.DriverUserName.Length);
                        writer.WriteString(value.DriverUserName);
                        writer.WriteString(value.QualificationCode.PadLeft(20, '\0').ValiString(nameof(value.QualificationCode), 20));
                        writer.WriteByte((byte)value.LicenseIssuing.Length);
                        writer.WriteString(value.LicenseIssuing);
                        writer.WriteDateTime_YYYYMMDD(value.CertificateExpiresDate);
                        if (writer.Version == JT808Version.JTT2019)
                        {
                            writer.WriteString(value.DriverIdentityCard.PadLeft(20, '\0').ValiString(nameof(value.DriverIdentityCard), 20));
                            //兼容808-2019 补充
                            if (value.FaceMatchValue.HasValue)
                            {
                                writer.WriteByte(value.FaceMatchValue.Value);
                            }
                            if (!string.IsNullOrEmpty(value.UID))
                            {
                                writer.WriteString(value.UID.PadLeft(20, '\0').ValiString(nameof(value.UID), 20));
                            }
                        }
                    }
                }
            }
        }
    }
}
