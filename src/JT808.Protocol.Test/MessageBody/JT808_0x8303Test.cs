using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x8303Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8303 jT808_0X8303 = new JT808_0x8303
            {
                SettingType = JT808InformationSettingType.delete_all_items.ToByteValue(),
                InformationItems = new List<JT808InformationItemProperty>()
            };
            jT808_0X8303.InformationItems.Add(new JT808InformationItemProperty
            {
                  InformationType=11,
                  InformationName="smallchi1"
            });
            jT808_0X8303.InformationItems.Add(new JT808InformationItemProperty
            {
                InformationType = 22,
                InformationName = "smallchi2"
            });
            var hex = JT808Serializer.Serialize(jT808_0X8303).ToHexString();
            Assert.Equal("00020B0009736D616C6C63686931160009736D616C6C63686932", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "00020B0009736D616C6C63686931160009736D616C6C63686932".ToHexBytes();
            JT808_0x8303 jT808_0X8303 = JT808Serializer.Deserialize<JT808_0x8303>(bytes);
            Assert.Equal(JT808InformationSettingType.delete_all_items.ToByteValue(), jT808_0X8303.SettingType);

            Assert.Equal(11, jT808_0X8303.InformationItems[0].InformationType);
            Assert.Equal("smallchi1", jT808_0X8303.InformationItems[0].InformationName);
            Assert.Equal(9, jT808_0X8303.InformationItems[0].InformationLength);

            Assert.Equal(22, jT808_0X8303.InformationItems[1].InformationType);
            Assert.Equal("smallchi2", jT808_0X8303.InformationItems[1].InformationName);
            Assert.Equal(9, jT808_0X8303.InformationItems[1].InformationLength);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "00020B0009736D616C6C63686931160009736D616C6C63686932".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8303>(bytes);
        }
    }
}
