using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0702Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                IC_Card_Status = JT808ICCardStatus.从业资格证IC卡拔出_驾驶员下班,
                IC_Card_PlugDateTime = DateTime.Parse("2018-08-16 09:16:16")
            };
            var hex = JT808Serializer.Serialize(jT808_0X0702).ToHexString();
            Assert.Equal("02 18 08 16 09 16 16".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test1_2()
        {
            byte[] bytes = "02 18 08 16 09 16 16".ToHexBytes();
            JT808_0x0702 jT808_0X0702 = JT808Serializer.Deserialize<JT808_0x0702>(bytes);
            Assert.Equal(JT808ICCardStatus.从业资格证IC卡拔出_驾驶员下班, jT808_0X0702.IC_Card_Status);
            Assert.Equal(DateTime.Parse("2018-08-16 09:16:16"), jT808_0X0702.IC_Card_PlugDateTime);
        }

        [Fact]
        public void Test2()
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                IC_Card_Status = JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班,
                IC_Card_PlugDateTime = DateTime.Parse("2018-08-16 09:16:16"),
                IC_Card_ReadResult = JT808ICCardReadResult.读卡失败_原因为卡片密钥认证未通过
            };
            var hex = JT808Serializer.Serialize(jT808_0X0702).ToHexString();
            Assert.Equal("01 18 08 16 09 16 16 01".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test2_2()
        {
            byte[] bytes = "01 18 08 16 09 16 16 01".ToHexBytes();
            JT808_0x0702 jT808_0X0702 = JT808Serializer.Deserialize<JT808_0x0702>(bytes);
            Assert.Equal(JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班, jT808_0X0702.IC_Card_Status);
            Assert.Equal(DateTime.Parse("2018-08-16 09:16:16"), jT808_0X0702.IC_Card_PlugDateTime);
            Assert.Equal(JT808ICCardReadResult.读卡失败_原因为卡片密钥认证未通过, jT808_0X0702.IC_Card_ReadResult);
        }

        [Fact]
        public void Test3()
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                IC_Card_Status = JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班,
                IC_Card_PlugDateTime = DateTime.Parse("2018-08-16 09:16:16"),
                IC_Card_ReadResult = JT808ICCardReadResult.IC卡读卡成功,
                DriverUserName = "koike",
                QualificationCode = "qwe123456aaa",
                LicenseIssuing = "qwertx",
                CertificateExpiresDate = DateTime.Parse("2018-08-16")
            };
            var hex = JT808Serializer.Serialize(jT808_0X0702).ToHexString();
            Assert.Equal("01 18 08 16 09 16 16 00 05 6B 6F 69 6B 65 71 77 65 31 32 33 34 35 36 61 61 61 30 30 30 30 30 30 30 30 06 71 77 65 72 74 78 07 E2 08 16".Replace(" ", ""), hex);
            //"01 18 08 16 09 16 16 00 05 6B 6F 69 6B 65 71 77 65 31 32 33 34 35 36 61 61 61 30 30 30 30 30 30 30 30 06 71 77 65 72 74 78 07 E2 08 16"
        }

        [Fact]
        public void Test3_1()
        {
            byte[] bytes = "01 18 08 16 09 16 16 00 05 6B 6F 69 6B 65 71 77 65 31 32 33 34 35 36 61 61 61 30 30 30 30 30 30 30 30 06 71 77 65 72 74 78 07 E2 08 16".ToHexBytes();
            JT808_0x0702 jT808_0X0702 = JT808Serializer.Deserialize<JT808_0x0702>(bytes);
            Assert.Equal(JT808ICCardStatus.从业资格证IC卡插入_驾驶员上班, jT808_0X0702.IC_Card_Status);
            Assert.Equal(DateTime.Parse("2018-08-16 09:16:16"), jT808_0X0702.IC_Card_PlugDateTime);
            Assert.Equal(JT808ICCardReadResult.IC卡读卡成功, jT808_0X0702.IC_Card_ReadResult);
            Assert.Equal("koike", jT808_0X0702.DriverUserName);
            Assert.Equal("qwe123456aaa00000000", jT808_0X0702.QualificationCode);
            Assert.Equal("qwertx", jT808_0X0702.LicenseIssuing);
            Assert.Equal(DateTime.Parse("2018-08-16"), jT808_0X0702.CertificateExpiresDate);
        }
    }
}
