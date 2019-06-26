using JT808.Protocol.Metadata;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JT808.Protocol.Benchmark")]
[assembly: InternalsVisibleTo("JT808.Protocol.Test")]
namespace JT808.Protocol.Internal
{
    internal class DefaultSplitPackageStrategyImpl : IJT808SplitPackageStrategy
    {
        private const int N = 256 * 3;

        public IEnumerable<JT808SplitPackageProperty> Processor(ReadOnlySpan<byte> bigData)
        {
            List<JT808SplitPackageProperty> jT808SplitPackagePropertys = new List<JT808SplitPackageProperty>();
            var quotient = bigData.Length / N;
            var remainder = bigData.Length % N;
            if (remainder != 0)
            {
                quotient = quotient + 1;
            }
            for (int i = 1; i <= quotient; i++)
            {
                JT808SplitPackageProperty jT808SplitPackageProperty = new JT808SplitPackageProperty
                {
                    PackgeIndex = i,
                    PackgeCount = quotient
                };
                if (i == quotient)
                {
                    jT808SplitPackageProperty.Data = bigData.Slice((i - 1) * N).ToArray();
                }
                else
                {
                    jT808SplitPackageProperty.Data = bigData.Slice((i - 1) * N, N).ToArray();
                }
                jT808SplitPackagePropertys.Add(jT808SplitPackageProperty);
            }
            return jT808SplitPackagePropertys;
        }
    }
}
