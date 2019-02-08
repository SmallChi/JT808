using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using System;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 驾驶员身份信息采集上报
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0702Formatter))]
    public class JT808_0x0702 : JT808Bodies
    {
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
    }
}
