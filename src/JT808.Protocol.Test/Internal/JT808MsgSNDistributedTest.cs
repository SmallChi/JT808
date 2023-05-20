using System;
using System.Collections.Generic;
using System.Text;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using Xunit;

namespace JT808.Protocol.Test.Internal
{
    public class JT808MsgSNDistributedTest
    {
        [Fact]
        public void Test1()
        {
            IJT808MsgSNDistributed JT808MsgSNDistributed = new DefaultMsgSNDistributedImpl();
            for (int i = 0; i < 10; i++)
            {
                var a = JT808MsgSNDistributed.Increment("1234");
                Assert.Equal(i, a);
            }
        }
    }
}
