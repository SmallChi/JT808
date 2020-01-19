using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8800Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                     MsgId= (ushort)JT808MsgId.多媒体数据上传应答,
                      TerminalPhoneNo="123456789",                       
                },
                Bodies = new JT808_0x8800
                {
                    MultimediaId = 129,
                    RetransmitPackageIds = new byte[] { 0x01, 0x02, 0x03, 0x04 }
                }

            };
            string hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E8800000900012345678900010000008102010203048E7E", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E8800000900012345678900010000008102010203048E7E".ToHexBytes();
           var jt808Package  = JT808Serializer.Deserialize<JT808Package>(bytes);
            JT808_0x8800 jT808_0X8800 = jt808Package.Bodies as JT808_0x8800;
            Assert.Equal((uint)129, jT808_0X8800.MultimediaId);
            Assert.Equal(2, jT808_0X8800.RetransmitPackageCount);
            Assert.Equal(new byte[] { 0x01, 0x02, 0x03, 0x04 }, jT808_0X8800.RetransmitPackageIds);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "7E8800000900012345678900010000008102010203048E7E".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808Package>(bytes);
        }
    }
}
