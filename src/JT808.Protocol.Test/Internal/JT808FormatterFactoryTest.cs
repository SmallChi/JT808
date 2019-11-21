using JT808.Protocol.Internal;
using JT808.Protocol.Test.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.Internal
{
   public class JT808FormatterFactoryTest
    {
        [Fact]
        public void Test1()
        {
            JT808FormatterFactory jT808FormatterFactory = new JT808FormatterFactory();
            Assert.Contains(jT808FormatterFactory.FormatterDict, f => f.Key == typeof(JT808Package).GUID);
        }

        [Fact]
        public void Test2()
        {
            JT808FormatterFactory jT808FormatterFactory = new JT808FormatterFactory();
            jT808FormatterFactory.SetMap<JT808_0x9999>();
            Assert.Contains(jT808FormatterFactory.FormatterDict, f => f.Key == typeof(JT808_0x9999).GUID);
        }
    }
}
