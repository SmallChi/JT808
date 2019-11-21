using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Test.Internal
{
    public class JT808MsgIdFactoryTest
    {
        [Fact]
        public void Test1()
        {
            IJT808MsgIdFactory jT808MsgIdFactory = new JT808MsgIdFactory();

        }
    }
}
