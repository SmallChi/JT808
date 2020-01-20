using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public  class JT808_0x8302Test
    {
        JT808Serializer JT808Serializer = new JT808Serializer();
        [Fact]
        public void Test1()
        {
            JT808_0x8302 jT808_0X8302 = new JT808_0x8302
            {
                Answers=new List<JT808_0x8302.Answer>()
                {
                    new JT808_0x8302.Answer()
                    {
                        Id = 128,
                        Content = "123456",
                    },
                    new JT808_0x8302.Answer()
                    {
                        Id = 127,
                        Content = "123457",
                    }
                },
                Flag = 1,
                Issue = "sdddaff"
            };
            var hex = JT808Serializer.Serialize(jT808_0X8302).ToHexString();
            //01
            //07
            //73 64 64 64 61 66 66 
            //80
            //00 06
            //31 32 33 34 35 36 
            //7F
            //00 06
            //31 32 33 34 35 37
            Assert.Equal("0107736464646166668000063132333435367F0006313233343537", hex);
        }

        [Fact]
        public void Test1_1()
        {
            byte[] bytes = "0107736464646166668000063132333435367F0006313233343537".ToHexBytes();
            JT808_0x8302 jT808_0X8302 = JT808Serializer.Deserialize<JT808_0x8302>(bytes);
            Assert.Equal(1, jT808_0X8302.Flag);
            Assert.Equal(7, jT808_0X8302.IssueContentLength);
            Assert.Equal("sdddaff", jT808_0X8302.Issue);

            Assert.Equal(6, jT808_0X8302.Answers[0].ContentLength);
            Assert.Equal(128, jT808_0X8302.Answers[0].Id);
            Assert.Equal("123456", jT808_0X8302.Answers[0].Content);

            Assert.Equal(6, jT808_0X8302.Answers[1].ContentLength);
            Assert.Equal(127, jT808_0X8302.Answers[1].Id);
            Assert.Equal("123457", jT808_0X8302.Answers[1].Content);
        }

        [Fact]
        public void Test1_2()
        {
            byte[] bytes = "0107736464646166668000063132333435367F0006313233343537".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x8302>(bytes);
        }
    }
}
