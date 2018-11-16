using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.Test.MessageBody.JT808_0X8900_BodiesImpl;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x8900Test
    {
        public JT808_0x8900Test()
        {
            // 注册数据下行透传
            JT808GlobalConfig.Instance.Register_0x8900_Ext<JT808_0X8900_Test_BodiesImpl>(0x0B);
        }

        [Fact]
        public void Test1()
        {
            JT808_0x8900 jT808_0X8900 = new JT808_0x8900();
            jT808_0X8900.PassthroughType = 0x0B;
            jT808_0X8900.JT808_0X8900_BodyBase = new JT808_0X8900_Test_BodiesImpl
            {
                Id=12345,
                Sex=0x01
            };
            string hex = JT808Serializer.Serialize(jT808_0X8900).ToHexString();
            Assert.Equal("0B0000303901", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "0B0000303901".ToHexBytes();
            JT808_0x8900 jT808_0X8900 = JT808Serializer.Deserialize<JT808_0x8900>(bytes);
            Assert.Equal(0x0B, jT808_0X8900.PassthroughType);
            Assert.Equal((uint)12345, ((JT808_0X8900_Test_BodiesImpl)jT808_0X8900.JT808_0X8900_BodyBase).Id);
            Assert.Equal(0x01, ((JT808_0X8900_Test_BodiesImpl)jT808_0X8900.JT808_0X8900_BodyBase).Sex);
        }
    }
}
