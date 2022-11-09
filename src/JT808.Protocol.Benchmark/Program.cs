using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JT808.Protocol.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //安装NuGet包，BenchmarkDotNet
            //在需要做性能测试的方法前加上属性[Benchmark]。 
            Summary summary = BenchmarkRunner.Run<JT808SerializerContext>();
        }
    }
}
