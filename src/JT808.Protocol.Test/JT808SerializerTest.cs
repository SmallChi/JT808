using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JT808.Protocol.Test
{
    public class JT808SerializerTest
    {
        [Fact]
        public void ParallelTest1()
        {
            var result = Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (i) => 
            {
                IJT808Config jT808Config = new DefaultGlobalConfig();
                JT808Serializer jT808Serializer = new JT808Serializer(jT808Config);
            });
            if (result.IsCompleted)
            {

            }
        }

        [Fact]
        public unsafe void DefaultGlobalConfigTest1()
        {
            List<DefaultGlobalConfig> defaultGlobalConfigs = new List<DefaultGlobalConfig>();
            for(var i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    defaultGlobalConfigs.Add(new DefaultGlobalConfig(i.ToString()));
                }
                else
                {
                    defaultGlobalConfigs.Add(new DefaultGlobalConfig(i.ToString()));
                }
            }
        }
    }
}
