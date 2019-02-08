using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0702Formatter : IJT808Formatter<JT808_0x0702>
    {
        public JT808_0x0702 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                IC_Card_Status = (JT808ICCardStatus)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                IC_Card_PlugDateTime = JT808BinaryExtensions.ReadDateTime6Little(bytes, ref offset)
            };
            if (jT808_0X0702.IC_Card_Status == JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班)
            {
                jT808_0X0702.IC_Card_ReadResult = (JT808ICCardReadResult)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                if (jT808_0X0702.IC_Card_ReadResult == JT808ICCardReadResult.IC卡读卡成功)
                {
                    jT808_0X0702.DriverUserNameLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                    jT808_0X0702.DriverUserName = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X0702.DriverUserNameLength);
                    jT808_0X0702.QualificationCode = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 20);
                    jT808_0X0702.LicenseIssuingLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
                    jT808_0X0702.LicenseIssuing = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X0702.LicenseIssuingLength);
                    jT808_0X0702.CertificateExpiresDate = JT808BinaryExtensions.ReadDateTime4Little(bytes, ref offset);
                }
            }
            readSize = offset;
            return jT808_0X0702;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0702 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.IC_Card_Status);
            offset += JT808BinaryExtensions.WriteDateTime6Little(bytes, offset, value.IC_Card_PlugDateTime);
            if (value.IC_Card_Status == JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班)
            {
                offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.IC_Card_ReadResult);
                if (value.IC_Card_ReadResult == JT808ICCardReadResult.IC卡读卡成功)
                {
                    offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.DriverUserName.Length);
                    offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.DriverUserName);
                    offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.QualificationCode.PadRight(20, '0'));
                    offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.LicenseIssuing.Length);
                    offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.LicenseIssuing);
                    offset += JT808BinaryExtensions.WriteDateTime4Little(bytes, offset, value.CertificateExpiresDate);
                }
            }
            return offset;
        }
    }
}
