using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;
using JT808.Protocol.Metadata;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x8301Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8301 jT808_0X8301 = new JT808_0x8301
            {
                SettingType = JT808EventSettingType.delete_terminal_all_existing_events.ToByteValue(),
                EventItems = new List<JT808EventProperty>()
            };
            jT808_0X8301.EventItems.Add(new JT808EventProperty
            {
                 EventId=1,
                 EventContent="123456"
            });
            jT808_0X8301.EventItems.Add(new JT808EventProperty
            {
                EventId = 2,
                EventContent = "789456"
            });

            var hex = JT808Serializer.Serialize(jT808_0X8301).ToHexString();
            Assert.Equal("000201063132333435360206373839343536", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "000201063132333435360206373839343536".ToHexBytes();
            JT808_0x8301 jT808_0X8301 = JT808Serializer.Deserialize<JT808_0x8301>(bytes);
            Assert.Equal(JT808EventSettingType.delete_terminal_all_existing_events.ToByteValue(), jT808_0X8301.SettingType);
            Assert.Equal(2, jT808_0X8301.SettingCount);
  
            Assert.Equal(1, jT808_0X8301.EventItems[0].EventId);
            Assert.Equal(6, jT808_0X8301.EventItems[0].EventContentLength);
            Assert.Equal("123456", jT808_0X8301.EventItems[0].EventContent);
            Assert.Equal(2, jT808_0X8301.EventItems[1].EventId);
            Assert.Equal(6, jT808_0X8301.EventItems[1].EventContentLength);
            Assert.Equal("789456", jT808_0X8301.EventItems[1].EventContent);
        }

        [Fact]
        public void Test1_2()
        {
            byte[] bytes = "000201063132333435360206373839343536".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8301>(bytes);
        }
    }
}
