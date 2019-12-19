using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.Internal
{
    public class JT808MsgSNDistributedTest
    {
        [Fact]
        public void Test1()
        {
            IJT808MsgSNDistributed JT808MsgSNDistributed = new DefaultMsgSNDistributedImpl();
            var a=JT808MsgSNDistributed.Increment("1234");
            Assert.Equal(1, a);
            var a1 = JT808MsgSNDistributed.Increment("1234");
            Assert.Equal(2, a1);
        }
    }
}
