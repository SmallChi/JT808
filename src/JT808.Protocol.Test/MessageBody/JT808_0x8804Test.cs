using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8804Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8804 jT808_0X8804 = new JT808_0x8804
            {
                RecordCmd = JT808RecordCmd.stop,
                RecordTime = 30,
                RecordSave = JT808RecordSave.realtime_upload,
                AudioSampleRate = 1
            };
            string hex = JT808Serializer.Serialize(jT808_0X8804).ToHexString();
            //"00 00 1E 00 01"
            Assert.Equal("00001E0001", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "00 00 1E 00 01".ToHexBytes();
            JT808_0x8804 jT808_0X8804 = JT808Serializer.Deserialize<JT808_0x8804>(bytes);
            Assert.Equal(JT808RecordCmd.stop, jT808_0X8804.RecordCmd);
            Assert.Equal(30, jT808_0X8804.RecordTime);
            Assert.Equal(JT808RecordSave.realtime_upload, jT808_0X8804.RecordSave);
            Assert.Equal(1, jT808_0X8804.AudioSampleRate);
        }
        [Fact]
        public void Test3()
        {
            byte[] bytes = "00001E0001".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8804>(bytes);
        }
    }
}
