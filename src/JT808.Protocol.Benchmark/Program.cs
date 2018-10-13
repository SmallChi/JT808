using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808LocationAttach;
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
            Summary summary = BenchmarkRunner.Run<JT808TestContext>();
        }
    }

    [Config(typeof(MyConfig))]
    //[ClrJob, MonoJob, CoreJob]
    [RPlotExporter]
    [MemoryDiagnoser]
    public class JT808TestContext
    {


        private byte[] bytes;

        [Params(100, 10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            bytes ="7E 02 00 00 26 11 22 33 44 55 66 22 B8 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 57 7E".ToHexBytes();
        }

        //   [Benchmark(Description = "0x0200Serialize",OperationsPerInvoke =10)]
        [Benchmark(Description = "0x0200Serialize")]
        public void TestJT808_0x0200()
        {
            for (int i = 0; i < N; i++)
            {
                JT808Package jT808Package = new JT808Package();
                jT808Package.Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.位置信息汇报,
                    MsgNum = 8888,
                    TerminalPhoneNo = "112233445566",
                };
                JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200();
                jT808UploadLocationRequest.AlarmFlag = 1;
                jT808UploadLocationRequest.Altitude = 40;
                jT808UploadLocationRequest.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
                jT808UploadLocationRequest.Lat = 12222222;
                jT808UploadLocationRequest.Lng = 132444444;
                jT808UploadLocationRequest.Speed = 60;
                jT808UploadLocationRequest.Direction = 0;
                jT808UploadLocationRequest.StatusFlag = 2;
                jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
                jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
                {
                    Mileage = 100
                });
                jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
                {
                    Oil = 55
                });
                jT808Package.Bodies = jT808UploadLocationRequest;
                var result = JT808Serializer.Serialize(jT808Package);
            }
            //Parallel.For(0, N, new ParallelOptions { MaxDegreeOfParallelism = 10 }, (N) =>
            //{
            //    JT808Package jT808Package = new JT808Package();
            //    jT808Package.Header = new JT808Header
            //    {
            //        MsgId = Enums.JT808MsgId.位置信息汇报,
            //        MsgNum = 8888,
            //        TerminalPhoneNo = "112233445566",
            //    };
            //    JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200();
            //    jT808UploadLocationRequest.AlarmFlag = 1;
            //    jT808UploadLocationRequest.Altitude = 40;
            //    jT808UploadLocationRequest.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
            //    jT808UploadLocationRequest.Lat = 12222222;
            //    jT808UploadLocationRequest.Lng = 132444444;
            //    jT808UploadLocationRequest.Speed = 60;
            //    jT808UploadLocationRequest.Direction = 0;
            //    jT808UploadLocationRequest.StatusFlag = 2;
            //    jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            //    jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            //    {
            //        Mileage = 100
            //    });
            //    jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            //    {
            //        Oil = 55
            //    });
            //    jT808Package.Bodies = jT808UploadLocationRequest;
            //    var result = JT808Serializer.Serialize(jT808Package);
            //});
        }

        //[Benchmark(Description = "0x0200Deserialize",OperationsPerInvoke =10)]
        [Benchmark(Description = "0x0200Deserialize")]
        public void TestJT808_0x02001()
        {
            for (int i = 0; i < N; i++)
            {
                var result = JT808Serializer.Deserialize<JT808Package>(bytes);
            }
            //Parallel.For(0, N,new ParallelOptions { MaxDegreeOfParallelism=10}, (N) =>
            //{
            //    var result = JT808Serializer.Deserialize<JT808Package>(bytes);
            //});
        }
    }

    public class MyConfig : ManualConfig
    {
        public MyConfig()
        {
            Add(Job.Default.WithGcServer(true).With(Runtime.Clr).With(Platform.AnyCpu));
            Add(Job.Default.WithGcServer(true).With(CsProjCoreToolchain.NetCoreApp21).With(Platform.AnyCpu));
            Add(Job.Default.WithGcServer(true).With(Runtime.Mono).With(Platform.AnyCpu));
        }
    }
}
