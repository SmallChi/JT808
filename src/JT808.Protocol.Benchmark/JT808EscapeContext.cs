using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Benchmark
{
    [Config(typeof(JT808EscapeConfig))]
    [MemoryDiagnoser]
    public class JT808DeEscapeContext
    {
        private byte[] bytes;

        [Params(100, 10000, 100000)]
        public int N;


        [GlobalSetup]
        public void Setup()
        {
            bytes = "7E02000026123456789012007D02000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D01137E".ToHexBytes();
        }

        [Benchmark(Description = "DeEscapeOld")]
        public void TestDeEscapeOld()
        {
            for (int i = 0; i < N; i++)
            {
                ReadOnlySpan<byte> buffer = JT808PackageFromatter.JT808DeEscapeOld(bytes, 0, bytes.Length);
            }
        }

        [Benchmark(Description = "DeEscape")]
        public void TestDeEscape()
        {
            for (int i = 0; i < N; i++)
            {
                ReadOnlySpan<byte> buffer = JT808PackageFromatter.JT808DeEscape(bytes);
            }
        }
    }

    [Config(typeof(JT808EscapeConfig))]
    [MemoryDiagnoser]
    public class JT808EscapeContext
    {
        private byte[] byteslist;
        private byte[] bytespoll;
        private int offset = 0;

        [Params(100, 10000, 100000)]
        public int N;


        [GlobalSetup]
        public void Setup()
        {
            byteslist = JT808ArrayPool.Rent(1024);
            bytespoll = JT808ArrayPool.Rent(1024);
            var tmp = "7E02000026123456789012007E000000010000000200BA7F0E07E4F11C0028003C00001810151010100104000000640202007D137E".ToHexBytes();
            offset = tmp.Length;
            Array.Copy(tmp, 0, byteslist, 0, offset);
            Array.Copy(tmp, 0, bytespoll, 0, offset);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            JT808ArrayPool.Return(byteslist);
            JT808ArrayPool.Return(bytespoll);
        }

        [Benchmark(Description = "EscapeOld")]
        public void TestEscapeList()
        {
            for (int i = 0; i < N; i++)
            {
                JT808PackageFromatter.JT808EscapeOld(ref byteslist, offset);
            }
        }

        [Benchmark(Description = "Escape")]
        public void TestEscapePoll()
        {
            for (int i = 0; i < N; i++)
            {
                 JT808PackageFromatter.JT808Escape(ref bytespoll, offset);
            }
        }
    }

    public class JT808EscapeConfig : ManualConfig
    {
        public JT808EscapeConfig()
        {
            Add(Job.Default.WithGcServer(false).With(Runtime.Clr).With(Platform.AnyCpu));
            Add(Job.Default.WithGcServer(false).With(CsProjCoreToolchain.NetCoreApp21).With(Platform.AnyCpu));
            Add(Job.Default.WithGcServer(false).With(CsProjCoreToolchain.NetCoreApp22).With(Platform.AnyCpu));
        }
    }
}
