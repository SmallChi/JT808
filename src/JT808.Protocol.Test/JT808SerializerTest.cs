using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JT808.Protocol.Test
{
    public class JT808SerializerTest
    {
        [Fact]
        public void Test1()
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
    }
}
