using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0705Test : JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0705 jT808_0X0705 = new JT808_0x0705
            {
                FirstCanReceiveTime = DateTime.Parse("2018-11-15 20:00:05.350"),
                CanItems = new List<JT808Properties.JT808CanProperty>()
            };
            jT808_0X0705.CanItems.Add(new JT808Properties.JT808CanProperty()
            {
                CanId = new byte[] { 0x01, 0x02, 0x03, 0x04 },
                CanData = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x01, 0x02, 0x03, 0x04 },
            });
            jT808_0X0705.CanItems.Add(new JT808Properties.JT808CanProperty()
            {
                CanId = new byte[] { 0x05, 0x06, 0x07, 0x08 },
                CanData = new byte[] { 0x01, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x01 },
            });
            var hex = JT808Serializer.Serialize(jT808_0X0705).ToHexString();
            Assert.Equal("0002200005015E010203040102030401020304050607080103040506070801", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0002200005015E010203040102030401020304050607080103040506070801".ToHexBytes();
            JT808_0x0705 jT808_0X0705 = JT808Serializer.Deserialize<JT808_0x0705>(bytes);

            Assert.Equal(2, jT808_0X0705.CanItemCount);

            Assert.Equal(DateTime.Parse("20:00:05.350"), jT808_0X0705.FirstCanReceiveTime);

            Assert.Equal(new byte[] { 0x01, 0x02, 0x03, 0x04 }, jT808_0X0705.CanItems[0].CanId);
            Assert.Equal(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x01, 0x02, 0x03, 0x04 }, jT808_0X0705.CanItems[0].CanData);

            Assert.Equal(new byte[] { 0x05, 0x06, 0x07, 0x08 }, jT808_0X0705.CanItems[1].CanId);
            Assert.Equal(new byte[] { 0x01, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x01 }, jT808_0X0705.CanItems[1].CanData);
        }
    }
}
