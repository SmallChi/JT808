using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0805Test : JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0805 jT808_0X0805 = new JT808_0x0805();
            jT808_0X0805.MsgNum = 12456;
            jT808_0X0805.Result = 1;
            jT808_0X0805.MultimediaIds = new List<uint>()
            {
                12306,
                12580
            };
            string hex = JT808Serializer.Serialize(jT808_0X0805).ToHexString();
            Assert.Equal("30A80100020000301200003124", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "30A80100020000301200003124".ToHexBytes();
            JT808_0x0805 jT808_0X0805 = JT808Serializer.Deserialize<JT808_0x0805>(bytes);
            Assert.Equal(12456, jT808_0X0805.MsgNum);
            Assert.Equal(1, jT808_0X0805.Result);
            Assert.Equal(2, jT808_0X0805.MultimediaIdCount);
            Assert.Equal(new List<uint>()
            {
                12306,
                12580
            }, jT808_0X0805.MultimediaIds);
        }
    }
}
