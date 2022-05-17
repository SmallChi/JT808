using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0702Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                IC_Card_Status = JT808ICCardStatus.ic_card_pull_out,
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
            Assert.Equal(JT808ICCardStatus.ic_card_pull_out, jT808_0X0702.IC_Card_Status);
            Assert.Equal(DateTime.Parse("2018-08-16 09:16:16"), jT808_0X0702.IC_Card_PlugDateTime);
        }

        [Fact]
        public void Test2()
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                IC_Card_Status = JT808ICCardStatus.ic_card_into,
                IC_Card_PlugDateTime = DateTime.Parse("2018-08-16 09:16:16"),
                IC_Card_ReadResult = JT808ICCardReadResult.read_card_failure_auth
            };
            var hex = JT808Serializer.Serialize(jT808_0X0702).ToHexString();
            Assert.Equal("01 18 08 16 09 16 16 01".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test2_2()
        {
            byte[] bytes = "01 18 08 16 09 16 16 01".ToHexBytes();
            JT808_0x0702 jT808_0X0702 = JT808Serializer.Deserialize<JT808_0x0702>(bytes);
            Assert.Equal(JT808ICCardStatus.ic_card_into, jT808_0X0702.IC_Card_Status);
            Assert.Equal(DateTime.Parse("2018-08-16 09:16:16"), jT808_0X0702.IC_Card_PlugDateTime);
            Assert.Equal(JT808ICCardReadResult.read_card_failure_auth, jT808_0X0702.IC_Card_ReadResult);
        }

        [Fact]
        public void Test3()
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                IC_Card_Status = JT808ICCardStatus.ic_card_into,
                IC_Card_PlugDateTime = DateTime.Parse("2018-08-16 09:16:16"),
                IC_Card_ReadResult = JT808ICCardReadResult.ic_card_reading_succeeded,
                DriverUserName = "koike",
                QualificationCode = "qwe123456aaa",
                LicenseIssuing = "qwertx",
                CertificateExpiresDate = DateTime.Parse("2018-08-16")
            };
            var hex = JT808Serializer.Serialize(jT808_0X0702).ToHexString();
            Assert.Equal("0118081609161600056B6F696B6500000000000000007177653132333435366161610671776572747820180816".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test3_1()
        {
            byte[] bytes = "0118081609161600056B6F696B6571776531323334353661616100000000000000000671776572747820180816".ToHexBytes();
            JT808_0x0702 jT808_0X0702 = JT808Serializer.Deserialize<JT808_0x0702>(bytes);
            Assert.Equal(JT808ICCardStatus.ic_card_into, jT808_0X0702.IC_Card_Status);
            Assert.Equal(DateTime.Parse("2018-08-16 09:16:16"), jT808_0X0702.IC_Card_PlugDateTime);
            Assert.Equal(JT808ICCardReadResult.ic_card_reading_succeeded, jT808_0X0702.IC_Card_ReadResult);
            Assert.Equal("koike", jT808_0X0702.DriverUserName);
            Assert.Equal("qwe123456aaa", jT808_0X0702.QualificationCode);
            Assert.Equal("qwertx", jT808_0X0702.LicenseIssuing);
            Assert.Equal(DateTime.Parse("2018-08-16"), jT808_0X0702.CertificateExpiresDate);
        }

        [Fact]
        public void Test_2019_1()
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                IC_Card_Status = JT808ICCardStatus.ic_card_into,
                IC_Card_PlugDateTime = DateTime.Parse("2019-12-01 11:11:11"),
                IC_Card_ReadResult = JT808ICCardReadResult.ic_card_reading_succeeded,
                DriverUserName = "koike",
                QualificationCode = "qwe123456aaa",
                LicenseIssuing = "qwertx",
                CertificateExpiresDate = DateTime.Parse("2019-12-01"),
                DriverIdentityCard="12345678901234567"
            };
            var hex = JT808Serializer.Serialize(jT808_0X0702, JT808Version.JTT2019).ToHexString();
            Assert.Equal("0119120111111100056B6F696B65000000000000000071776531323334353661616106717765727478201912010000003132333435363738393031323334353637", hex);
            
        }

        [Fact]
        public void Test_2019_2()
        {
            byte[] bytes = "0119120111111100056B6F696B65717765313233343536616161000000000000000006717765727478201912013132333435363738393031323334353637000000".ToHexBytes();
            JT808_0x0702 jT808_0X0702 = JT808Serializer.Deserialize<JT808_0x0702>(bytes, JT808Version.JTT2019);
            Assert.Equal(JT808ICCardStatus.ic_card_into, jT808_0X0702.IC_Card_Status);
            Assert.Equal(DateTime.Parse("2019-12-01 11:11:11"), jT808_0X0702.IC_Card_PlugDateTime);
            Assert.Equal(JT808ICCardReadResult.ic_card_reading_succeeded, jT808_0X0702.IC_Card_ReadResult);
            Assert.Equal("koike", jT808_0X0702.DriverUserName);
            Assert.Equal("qwe123456aaa", jT808_0X0702.QualificationCode);
            Assert.Equal("qwertx", jT808_0X0702.LicenseIssuing);
            Assert.Equal(DateTime.Parse("2019-12-01"), jT808_0X0702.CertificateExpiresDate);
            Assert.Equal("12345678901234567", jT808_0X0702.DriverIdentityCard);
        }

        [Fact]
        public void Test_2019_3()
        {
            byte[] bytes = "0119120111111100056B6F696B65717765313233343536616161303030303030303006717765727478201912013132333435363738393031323334353637303030".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0702>(bytes, JT808Version.JTT2019);
        }

        [Fact]
        public void Test_2019_4()
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                IC_Card_Status = JT808ICCardStatus.ic_card_into,
                IC_Card_PlugDateTime = DateTime.Parse("2021-05-28 18:11:11"),
                IC_Card_ReadResult = JT808ICCardReadResult.ic_card_reading_succeeded,
                DriverUserName = "koike",
                QualificationCode = "qwe123456aaa",
                LicenseIssuing = "qwertx",
                CertificateExpiresDate = DateTime.Parse("2021-05-28"),
                DriverIdentityCard = "12345678901234567",
                FaceMatchValue=99,
                UID= "12345678901234567"
            };
            var hex = JT808Serializer.Serialize(jT808_0X0702, JT808Version.JTT2019).ToHexString();
            Assert.Equal("0121052818111100056B6F696B65000000000000000071776531323334353661616106717765727478202105280000003132333435363738393031323334353637630000003132333435363738393031323334353637", hex);
        }

        [Fact]
        public void Test_2019_5()
        {
            byte[] bytes = "0121052818111100056B6F696B65000000000000000071776531323334353661616106717765727478202105280000003132333435363738393031323334353637630000003132333435363738393031323334353637".ToHexBytes();
            JT808_0x0702 jT808_0X0702 = JT808Serializer.Deserialize<JT808_0x0702>(bytes, JT808Version.JTT2019);
            Assert.Equal(JT808ICCardStatus.ic_card_into, jT808_0X0702.IC_Card_Status);
            Assert.Equal(DateTime.Parse("2021-05-28 18:11:11"), jT808_0X0702.IC_Card_PlugDateTime);
            Assert.Equal(JT808ICCardReadResult.ic_card_reading_succeeded, jT808_0X0702.IC_Card_ReadResult);
            Assert.Equal("koike", jT808_0X0702.DriverUserName);
            Assert.Equal("qwe123456aaa", jT808_0X0702.QualificationCode);
            Assert.Equal("qwertx", jT808_0X0702.LicenseIssuing);
            Assert.Equal(DateTime.Parse("2021-05-28"), jT808_0X0702.CertificateExpiresDate);
            Assert.Equal("12345678901234567", jT808_0X0702.DriverIdentityCard);
            Assert.Equal(99, jT808_0X0702.FaceMatchValue.Value);
            Assert.Equal("12345678901234567", jT808_0X0702.UID);
        }

        [Fact]
        public void Test_2019_6()
        {
            byte[] bytes = "0121052818111100056B6F696B65000000000000000071776531323334353661616106717765727478202105280000003132333435363738393031323334353637630000003132333435363738393031323334353637".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0702>(bytes, JT808Version.JTT2019);
        }

        [Fact]
        public void Test2011_1()
        {
            JT808_0x0702 jT808_0X0702 = new JT808_0x0702
            {
                DriverUserName = "tk",
                DriverIdentityCard="123456789123456789",
                QualificationCode = "qwe123456aaa",
                LicenseIssuing = "qwertx"
            };
            var hex = JT808Serializer.Serialize(jT808_0X0702, JT808Version.JTT2011).ToHexString();
            Assert.Equal("02746B00003132333435363738393132333435363738390000000000000000000000000000000000000000000000000000000071776531323334353661616106717765727478".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test2011_2()
        {
            byte[] bytes = "02746B00003132333435363738393132333435363738390000000000000000000000000000000000000000000000000000000071776531323334353661616106717765727478".ToHexBytes();
            JT808_0x0702 jT808_0X0702 = JT808Serializer.Deserialize<JT808_0x0702>(bytes);
            Assert.Equal("tk", jT808_0X0702.DriverUserName);
            Assert.Equal("qwe123456aaa", jT808_0X0702.QualificationCode);
            Assert.Equal("qwertx", jT808_0X0702.LicenseIssuing);
            Assert.Equal("123456789123456789", jT808_0X0702.DriverIdentityCard);
        }

        [Fact]
        public void Test2011_3()
        {
            byte[] bytes = "02746B00003132333435363738393132333435363738390000000000000000000000000000000000000000000000000000000071776531323334353661616106717765727478".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0702>(bytes, JT808Version.JTT2011);
        }
    }
}
