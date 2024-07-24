using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Extensions.JT1078.Enums;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public class JT808_0x0200Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x0200Test()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1
                            .AddJT808Configure()
                            .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<IJT808Config>();
            JT808Serializer = defaultConfig.GetSerializer();
        }

        [Fact]
        public void Test_0x14_1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2020-01-31 20:20:20"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>()
            };
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_JT1078_Constants.JT808_0X0200_0x14, new JT808_0x0200_0x14
            {
                VideoRelateAlarm = (uint)(VideoRelateAlarmType.video_signal_occlusion_alarm | VideoRelateAlarmType.other_video_equipment_fault_alarm)
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C000020013120202014040000000A", hex);
        }

        [Fact]
        public void Test_0x14_2()
        {
            byte[] bodys = "000000010000000200BA7F0E07E4F11C0028003C000020013120202014040000000A".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal((uint)1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2020-01-31 20:20:20"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal((uint)2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal((uint)(VideoRelateAlarmType.video_signal_occlusion_alarm | VideoRelateAlarmType.other_video_equipment_fault_alarm), ((JT808_0x0200_0x14)jT808UploadLocationRequest.CustomLocationAttachData[JT808_JT1078_Constants.JT808_0X0200_0x14]).VideoRelateAlarm);
        }

        [Fact]
        public void Test_0x14_3()
        {
            byte[] bodys = "000000010000000200BA7F0E07E4F11C0028003C000020013120202014040000000A".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0200>(bodys);
        }

        [Fact]
        public void Test_0x15_1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2020-01-31 20:20:20"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>()
            };
            jT808UploadLocationRequest.CustomLocationAttachData.Add(JT808_JT1078_Constants.JT808_0X0200_0x15, new JT808_0x0200_0x15
            {
                VideoSignalLoseAlarmStatus = 3
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C0000200131202020150400000003", hex);
        }

        [Fact]
        public void Test_0x15_2()
        {
            byte[] bodys = "000000010000000200BA7F0E07E4F11C0028003C0000200131202020150400000003".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal((uint)1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2020-01-31 20:20:20"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal((uint)2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(3u, ((JT808_0x0200_0x15)jT808UploadLocationRequest.CustomLocationAttachData[JT808_JT1078_Constants.JT808_0X0200_0x15]).VideoSignalLoseAlarmStatus);
        }

        [Fact]
        public void Test_0x15_3()
        {
            byte[] bodys = "000000010000000200BA7F0E07E4F11C0028003C0000200131202020150400000003".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808_0x0200>(bodys);
        }

        /// <summary>
        /// 感谢大兄弟提供的数据
        /// ref:https://github.com/SmallChi/JTTools/issues/8
        /// </summary>
        [Fact]
        public void Test_0x15_4()
        {
            byte[] bodys = "7e020040420100000000013419905507021200040000001410010213679206c4a97d01001300000002220720103957010400000e3e020200000302025825040000001030014531010814040000000115040000000c797e".ToHexBytes();
            string json = JT808Serializer.Analyze<JT808Package>(bodys);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test_0x15_5()
        {
            JT808Serializer.Instance.Register(JT808_JT1078_Constants.GetCurrentAssembly());
            byte[] bodys = "7e020040420100000000013419905507021200040000001410010213679206c4a97d01001300000002220720103957010400000e3e020200000302025825040000001030014531010814040000000115040000000c797e".ToHexBytes();
            string json = JT808Serializer.Instance.Analyze<JT808Package>(bodys);
        }

        [Theory]
        [InlineData("7e020040600100000000653695850003022a00000000000c000300145b2206312b7a002e000000002407021142520104000001c404020000030200001404000000041504000000001604000000001702000218030000001904000000002504000000002b0400000000300100310119520100c57e")]
        public void Test_0x17(string hex)
        {
            var package = JT808Serializer.Deserialize(hex.ToHexBytes());
            Assert.IsType<JT808_0x0200>(package.Bodies);

            var _0x0200 = (JT808_0x0200)package.Bodies;
            var _0x17 = _0x0200.CustomLocationAttachData.FirstOrDefault(x => x.Key == 0x017).Value;
            Assert.IsType<JT808_0x0200_0x17>(_0x17);
            var _0x0200_0x17 = (JT808_0x0200_0x17)_0x17;
            Assert.True(_0x0200_0x17.StorageFault.FirstOrDefault(x => x.Index == 2).Fault);
        }
    }
}
