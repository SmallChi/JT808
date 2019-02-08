using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.Benchmark
{
    [Config(typeof(JT808SerializerConfig))]
    [MemoryDiagnoser]
    public class JT808SerializerContext
    {
        private byte[] bytes;

        [Params(100, 10000, 100000)]
        public int N;

        private ushort MsgId;

        [GlobalSetup]
        public void Setup()
        {
            bytes = "7E 02 00 00 26 11 22 33 44 55 66 22 B8 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 57 7E".ToHexBytes();
            MsgId = (ushort)Enums.JT808MsgId.位置信息汇报.ToValue();
        }

        [Benchmark(Description = "0x0200Serialize")]
        public void TestJT808_0x0200()
        {
            for (int i = 0; i < N; i++)
            {
                JT808Package jT808Package = new JT808Package();
                jT808Package.Header = new JT808Header
                {
                    MsgId = MsgId,
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
                jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>();
                jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808_0x0200_BodyBase.AttachId0x01, new JT808_0x0200_0x01
                {
                    Mileage = 100
                });
                jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808_0x0200_BodyBase.AttachId0x02, new JT808_0x0200_0x02
                {
                    Oil = 55
                });
                jT808Package.Bodies = jT808UploadLocationRequest;
                var result = JT808Serializer.Serialize(jT808Package);
            }
        }

        [Benchmark(Description = "0x0200Deserialize")]
        public void TestJT808_0x02001()
        {
            for (int i = 0; i < N; i++)
            {
                var result = JT808Serializer.Deserialize<JT808Package>(bytes);
            }
        }
    }

    public class JT808SerializerConfig : ManualConfig
    {
        public JT808SerializerConfig()
        {
            Add(Job.Default.WithGcServer(false).With(Runtime.Clr).With(Platform.AnyCpu));
            Add(Job.Default.WithGcServer(false).With(CsProjCoreToolchain.NetCoreApp22).With(Platform.AnyCpu));
        }
    }
}
