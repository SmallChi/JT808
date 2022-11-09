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
    [MarkdownExporterAttribute.GitHub]
    [MemoryDiagnoser]
    [CategoriesColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class JT808SerializerContext
    {
        private byte[] bytes0x0200;
        private byte[] bytes0x0100;

        [Params(100, 10000, 100000)]
        public int N;

        private ushort MsgId0x0200;
        private ushort MsgId0x0100;
        JT808Serializer JT808Serializer;
        [GlobalSetup]
        public void Setup()
        {
            JT808Serializer = new JT808Serializer();
            bytes0x0200 = "7E0200005C11223344556622B8000000010000000200BA7F0E07E4F11C0028003C00001807151010100104000000640202003703020038040200011105010000000112060100000001011307000000020022012504000000172A0200F42B04000000F2300102310105167E".ToHexBytes();
            MsgId0x0200 = Enums.JT808MsgId._0x0200.ToUInt16Value();
            MsgId0x0100 = Enums.JT808MsgId._0x0100.ToUInt16Value();
            bytes0x0100 = "7E 01 00 00 2D 00 01 23 45 67 89 00 0A 00 28 00 32 31 32 33 34 30 73 6D 61 6C 6C 63 68 69 31 32 33 30 30 30 30 30 30 30 30 30 43 48 49 31 32 33 30 01 D4 C1 41 31 32 33 34 35 BA 7E".ToHexBytes();
        }

        [Benchmark(Description = "0x0200_All_AttachId_Serialize"), BenchmarkCategory("0x0200Serializer")]
        public void TestJT808_0x0200_All_AttachId_Serialize()
        {
            for (int i = 0; i < N; i++)
            {
                JT808Package jT808Package = new JT808Package();
                jT808Package.Header = new JT808Header
                {
                    MsgId = MsgId0x0200,
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
                jT808UploadLocationRequest.BasicLocationAttachData = new Dictionary<byte, JT808_0x0200_BodyBase>();
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x01, new JT808_0x0200_0x01
                {
                    Mileage = 100
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x02, new JT808_0x0200_0x02
                {
                    Oil = 55
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x03, new JT808_0x0200_0x03
                {
                     Speed=56
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x04, new JT808_0x0200_0x04
                {
                     EventId=1
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x11, new JT808_0x0200_0x11
                {
                     AreaId=1,
                     JT808PositionType= Enums.JT808PositionType.circular_region
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x12, new JT808_0x0200_0x12
                {
                    AreaId = 1,
                    JT808PositionType = Enums.JT808PositionType.circular_region,
                    Direction= Enums.JT808DirectionType.direction_out
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x13, new JT808_0x0200_0x13
                {
                     DrivenRoute= Enums.JT808DrivenRouteType.overlength,
                     DrivenRouteId=2,
                     Time=34
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x25, new JT808_0x0200_0x25
                {
                     CarSignalStatus=23
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x2A, new JT808_0x0200_0x2A
                {
                    IOStatus=244
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x2B, new JT808_0x0200_0x2B
                {
                     Analog = 242
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x30, new JT808_0x0200_0x30
                {
                     WiFiSignalStrength=0x02
                });
                jT808UploadLocationRequest.BasicLocationAttachData.Add(JT808Constants.JT808_0x0200_0x31, new JT808_0x0200_0x31
                {
                     GNSSCount=0x05
                });
                jT808Package.Bodies = jT808UploadLocationRequest;
                var result = JT808Serializer.Serialize(jT808Package);
            }
        }

        [Benchmark(Description = "0x0200_All_AttachId_Deserialize"), BenchmarkCategory("0x0200Serializer")]
        public void TestJT808_0x0200_Deserialize()
        {
            for (int i = 0; i < N; i++)
            {
                var result = JT808Serializer.Deserialize(bytes0x0200);
            }
        }

        [Benchmark(Description = "0x0100Serialize"), BenchmarkCategory("0x0100Serializer")]
        public void TestJT808_0x0100_Serialize()
        {
            for (int i = 0; i < N; i++)
            {
                JT808Package jT808Package = new JT808Package();
                jT808Package.Header = new JT808Header
                {
                    MsgId = MsgId0x0100,
                    MsgNum = (ushort)(i + 1),
                    TerminalPhoneNo = "112233445566",
                };
                JT808_0x0100 jT808_0X0100 = new JT808_0x0100();
                jT808_0X0100.AreaID = 12345;
                jT808_0X0100.CityOrCountyId = 23454;
                jT808_0X0100.PlateColor = 0x02;
                jT808_0X0100.PlateNo = "测A123456";
                jT808_0X0100.TerminalId = "1234567";
                jT808_0X0100.TerminalModel = "1234567890000";
                jT808_0X0100.MakerId = "12345";
                jT808Package.Bodies = jT808_0X0100;
                var result = JT808Serializer.Serialize(jT808Package);
            }
        }

        [Benchmark(Description = "0x0100Deserialize"), BenchmarkCategory("0x0100Serializer")]
        public void TestJT808_0x0100_Deserialize()
        {
            for (int i = 0; i < N; i++)
            {
                var result = JT808Serializer.Deserialize(bytes0x0100);
            }
        }
    }

    public class JT808SerializerConfig : ManualConfig
    {
        public JT808SerializerConfig()
        {
            AddJob(Job.Default.WithGcServer(false).WithToolchain(CsProjCoreToolchain.NetCoreApp70).WithPlatform(Platform.AnyCpu));
        }
    }
}
