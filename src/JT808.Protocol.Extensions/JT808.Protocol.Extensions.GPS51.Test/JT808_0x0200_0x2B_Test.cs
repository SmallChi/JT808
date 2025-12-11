using JT808.Protocol.Extensions.GPS51.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT808.Protocol.Extensions.GPS51.Test
{
    public class JT808_0x0200_0x2B_Ext_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200_0x2B_Ext_Test()
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure()
                                        .AddGPS51Configure();

            IJT808Config jT808Config = serviceDescriptors.BuildServiceProvider().GetRequiredService<IJT808Config>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }

        [Fact]
        public void Test_attcahid_0x2b_1()
        {
            var bytes = "7e020000470412106280030233000000000000200201d365df072f15d500280000002d21091719155801040002a10f2a0200042b049203a46f520103eb06000100ce0a5730011b31010951080000000000000000ca7e".ToHexBytes();
            var package = JT808Serializer.Analyze(bytes);
        }
    }
}
