using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8804Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8804 jT808_0X8804 = new JT808_0x8804
            {
                RecordCmd = JT808RecordCmd.停止录音,
                RecordTime = 30,
                RecordSave = JT808RecordSave.实时上传,
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
            Assert.Equal(JT808RecordCmd.停止录音, jT808_0X8804.RecordCmd);
            Assert.Equal(30, jT808_0X8804.RecordTime);
            Assert.Equal(JT808RecordSave.实时上传, jT808_0X8804.RecordSave);
            Assert.Equal(1, jT808_0X8804.AudioSampleRate);
        }
    }
}
