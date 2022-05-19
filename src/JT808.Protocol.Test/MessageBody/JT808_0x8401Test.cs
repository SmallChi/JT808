using JT808.Protocol.Extensions;
using JT808.Protocol.Metadata;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8401Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8401 jT808_0X8401 = new JT808_0x8401
            {
                SettingTelephoneBook = Enums.JT808SettingTelephoneBook.append_phone_book
            };
            List<JT808ContactProperty> jT808ContactProperties = new List<JT808ContactProperty>();
            jT808ContactProperties.Add(new JT808ContactProperty
            {
                 TelephoneBookContactType= Enums.JT808TelephoneBookContactType.callin,
                 Contact="smallchi",
                 PhoneNumber="13456smallch"
            });
            jT808ContactProperties.Add(new JT808ContactProperty
            {
                TelephoneBookContactType = Enums.JT808TelephoneBookContactType.call_in_out,
                Contact = "koike",
                PhoneNumber = "koike123456"
            });
            jT808_0X8401.JT808ContactProperties = jT808ContactProperties;
            var hex = JT808Serializer.Serialize(jT808_0X8401).ToHexString();
            Assert.Equal("0202010C3133343536736D616C6C636808736D616C6C636869030B6B6F696B65313233343536056B6F696B65", hex);
            //"02 02 01 0C 31 33 34 35 36 73 6D 61 6C 6C 63 68 08 73 6D 61 6C 6C 63 68 69 03 0B 6B 6F 69 6B 65 31 32 33 34 35 36 05 6B 6F 69 6B 65"
        }

        [Fact]
        public void Test2()
        {
            var bytes = "02 02 01 0C 31 33 34 35 36 73 6D 61 6C 6C 63 68 08 73 6D 61 6C 6C 63 68 69 03 0B 6B 6F 69 6B 65 31 32 33 34 35 36 05 6B 6F 69 6B 65".ToHexBytes();
            JT808_0x8401 jT808_0X8401 = JT808Serializer.Deserialize<JT808_0x8401>(bytes);
            Assert.Equal(Enums.JT808SettingTelephoneBook.append_phone_book, jT808_0X8401.SettingTelephoneBook);
            Assert.Equal(2, jT808_0X8401.ContactCount);

            Assert.Equal("13456smallch", jT808_0X8401.JT808ContactProperties[0].PhoneNumber);
            Assert.Equal(Enums.JT808TelephoneBookContactType.callin, jT808_0X8401.JT808ContactProperties[0].TelephoneBookContactType);
            Assert.Equal("smallchi", jT808_0X8401.JT808ContactProperties[0].Contact);

            Assert.Equal("koike123456", jT808_0X8401.JT808ContactProperties[1].PhoneNumber);
            Assert.Equal(Enums.JT808TelephoneBookContactType.call_in_out, jT808_0X8401.JT808ContactProperties[1].TelephoneBookContactType);
            Assert.Equal("koike", jT808_0X8401.JT808ContactProperties[1].Contact);

        }

        [Fact]
        public void Test3()
        {
            var bytes = "02 02 01 0C 31 33 34 35 36 73 6D 61 6C 6C 63 68 08 73 6D 61 6C 6C 63 68 69 03 0B 6B 6F 69 6B 65 31 32 33 34 35 36 05 6B 6F 69 6B 65".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8401>(bytes);
        }
    }
}
