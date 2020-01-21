using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Test.MessageBody.JT808_0x0701BodiesImpl;
using System.Reflection;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0701Test
    {
        JT808Serializer JT808Serializer;

        public JT808_0x0701Test()
        {
            IJT808Config jT808Config = new DefaultGlobalConfig();
            jT808Config.FormatterFactory.SetMap<JT808_0x0701TestBodiesImpl>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Test1()
        {
            JT808_0x0701 jT808_0X0701 = new JT808_0x0701();
            var body = new JT808_0x0701TestBodiesImpl
            {
                Id = 333,
                UserName = "汉smallchi"
            };
            jT808_0X0701.ElectronicContentObj = body;
            var hex = JT808Serializer.Serialize(jT808_0X0701).ToHexString();
            Assert.Equal("000000100000014D000ABABA736D616C6C636869", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "000000100000014D000ABABA736D616C6C636869".ToHexBytes();
            JT808_0x0701 jT808_0X0701 = JT808Serializer.Deserialize<JT808_0x0701>(bytes);
            Assert.Equal((uint)16, jT808_0X0701.ElectronicWaybillLength);
            JT808_0x0701TestBodiesImpl jT808_0X0701_content = JT808Serializer.Deserialize<JT808_0x0701TestBodiesImpl>(jT808_0X0701.ElectronicContent);
            Assert.Equal((uint)333, jT808_0X0701_content.Id);
            Assert.Equal("汉smallchi", jT808_0X0701_content.UserName);
            Assert.Equal(10, jT808_0X0701_content.UserNameLength);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "000000100000014D000ABABA736D616C6C636869".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0701>(bytes);
        }
    }
}
