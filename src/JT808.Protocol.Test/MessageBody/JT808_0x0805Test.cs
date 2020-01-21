using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0805Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x0805 jT808_0X0805 = new JT808_0x0805();
            jT808_0X0805.ReplyMsgNum = 12456;
            jT808_0X0805.Result = 0;
            jT808_0X0805.MultimediaIds = new List<uint>()
            {
                12306,
                12580
            };
            string hex = JT808Serializer.Serialize(jT808_0X0805).ToHexString();
            Assert.Equal("30A80000020000301200003124", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "30A80000020000301200003124".ToHexBytes();
            JT808_0x0805 jT808_0X0805 = JT808Serializer.Deserialize<JT808_0x0805>(bytes);
            Assert.Equal(12456, jT808_0X0805.ReplyMsgNum);
            Assert.Equal(0, jT808_0X0805.Result);
            Assert.Equal(2, jT808_0X0805.MultimediaIdCount);
            Assert.Equal(new List<uint>()
            {
                12306,
                12580
            }, jT808_0X0805.MultimediaIds);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "30A80000020000301200003124".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0805>(bytes);
        }
    }
}
