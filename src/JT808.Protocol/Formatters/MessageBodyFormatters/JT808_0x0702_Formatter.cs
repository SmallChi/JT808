using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0702_Formatter : IJT808MessagePackFormatter<JT808_0x0702>
    {
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
                    jT808_0X0702.DriverUserName = reader.ReadString( jT808_0X0702.DriverUserNameLength);
                    jT808_0X0702.QualificationCode = reader.ReadString(20);
                    jT808_0X0702.LicenseIssuingLength = reader.ReadByte();
                    jT808_0X0702.LicenseIssuing = reader.ReadString(jT808_0X0702.LicenseIssuingLength);
                    jT808_0X0702.CertificateExpiresDate = reader.ReadDateTime4();
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
                    writer.WriteString( value.QualificationCode.PadRight(20, '0'));
                    writer.WriteByte((byte)value.LicenseIssuing.Length);
                    writer.WriteString(value.LicenseIssuing);
                    writer.WriteDateTime4(value.CertificateExpiresDate);
                }
            }
        }
    }
}
