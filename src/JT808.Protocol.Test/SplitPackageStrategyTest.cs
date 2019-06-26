using JT808.Protocol.Interfaces;
using JT808.Protocol.Internal;
using System;
using System.IO;
using Xunit;

namespace JT808.Protocol.Test
{
    public class SplitPackageStrategyTest
    {
        [Fact]
        public void Test1()
        {
            IJT808SplitPackageStrategy splitPackageStrategy = new DefaultSplitPackageStrategyImpl();
            byte[] data;
            using (FileStream input = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "test.txt")))
            {
                data = new byte[input.Length];
                input.Read(data, 0, (int)input.Length);
            }
            var result = splitPackageStrategy.Processor(data);
        }
    }
}
