using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808_0x8103_Body;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test.MessageBody
{
   public  class JT808_0x8302Test
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8302 jT808_0X8302 = new JT808_0x8302();
            jT808_0X8302.AnswerId = 128;
            jT808_0X8302.AnswerContent = "123456";
            jT808_0X8302.Flag = 1;
            jT808_0X8302.Issue = "sdddaff";

            var hex = JT808Serializer.Serialize(jT808_0X8302).ToHexString();
            Assert.Equal("010773646464616666800600313233343536", hex);
        }

        [Fact]
        public void Test1_1()
        {
#warning 待测试
            byte[] bytes = "010773646464616666800600313233343536".ToHexBytes();
            JT808_0x8302 jT808_0X8302 = JT808Serializer.Deserialize<JT808_0x8302>(bytes);
            Assert.Equal(128, jT808_0X8302.AnswerId);
            Assert.Equal("123456", jT808_0X8302.AnswerContent);
            Assert.Equal(1, jT808_0X8302.Flag);
            Assert.Equal("sdddaff", jT808_0X8302.Issue);
            Assert.Equal(6, jT808_0X8302.AnswerContentLength);
            Assert.Equal(7, jT808_0X8302.IssueContentLength);
        }
    }
}
