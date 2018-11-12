using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0801Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8001 jT808_0X8001 = new JT808_0x8001();
            jT808_0X8001.JT808PlatformResult = Enums.JT808PlatformResult.成功;
            jT808_0X8001.MsgId = 999;
            jT808_0X8001.MsgNum = 123;


        }
    }
}
