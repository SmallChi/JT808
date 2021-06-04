using JT808.Protocol.Extensions.SuBiao.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.SuBiao.Test
{
    public class JT808_0x8103_0xF364_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x8103_0xF364_Test()
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddJT808Configure()
                                        .AddSuBiaoConfigure();
            IJT808Config jT808Config = serviceDescriptors.BuildServiceProvider().GetRequiredService<IJT808Config>();
            JT808Serializer = new JT808Serializer(jT808Config);
        }
        [Fact]
        public void Serializer()
        {
            JT808_0x8103 jT808UploadLocationRequest = new JT808_0x8103
            {                 
                ParamList=new List<JT808_0x8103_BodyBase> {
                  new JT808_0x8103_0xF364{
                    ActiveDistancePhotographyDistanceInterval=1,
                    ActivelyTimePhotoInterval=2,
                    ActivePhotographyStrategy=3,
                    AlarmEnable=4,
                    AlarmJudgeSpeedThreshold=5,
                    AlarmPhotoVehicleCloseDistance=6,
                    AlarmPhotoVehicleCloseDistanceInterval=7,
                    BarrierAlarmPhotographs=8,
                    DistanceThresholdObstacleAlarm=9,
                    EventEnable=10,
                    ForwardCollisionAlarmInterval=11,
                    ForwardCollisionAlarmPhotographs=12,
                    ForwardCollisionWarningTimeThreshold=13,
                    FrequentAlarmJudgmentNumberChannelChange=14,
                    FrequentChannelChangeAlarmJudgmentTimePeriod=15,
                    FrequentChannelChangeAlarmPhotos=16,
                    FrequentLaneChangeAlarmInterval=17,
                    GradedSpeedThresholdLaneDeviationAlarm=18,
                    HierarchicalSpeedThresholdForwardCollisionWarning=19,
                    HierarchicalSpeedThresholdFrequentChannelChangeAlarm=20,
                    HierarchicalSpeedThresholdObstacleAlarm=21,
                    LaneDepartureAlarmPhoto=22,
                    LaneDepartureAlarmPhotoInterval=23,
                    ObstacleAlarmInterval=24,
                    PedestrianCollisionAlarmEnablingSpeedThreshold=25,
                    PedestrianCollisionAlarmInterval=26,
                    PedestrianCollisionAlarmPhotos=27,
                    PedestrianCollisionAlarmTimeThreshold=28,
                    PhotoResolution=29,
                    Placeholder1=30,
                    Placeholder2=new byte[]{1,2,3,4 },
                    RoadSignRecognitionPhotographs=32,
                    RoadSignRecognitionPhotographsInterval=33,
                    SingleInitiativePhotos=34,
                    SingleInitiativePhotosInterval=35,
                    VehicleDistanceMonitoringAlarmDistanceThreshold=36,
                    VehicleDistanceMonitoringAndAlarmClassificationSpeedThreshold=37,
                    VideoRecordingResolution=38,
                    VideoRecordingTimeBeforeAndAfterAlarmVehicleProximity=39,
                    VideoRecordingTimeBeforeAndAfterForwardCollisionAlarm=40,
                    VideoRecordingTimeBeforeAndAfterFrequentLaneChangeAlarm=41,
                    VideoRecordingTimeBeforeAndAfterLaneDepartureAlarm=42,
                    VideoRecordingTimeBeforeAndAfterObstacleAlarm=43,
                    VideoRecordingTimeBeforeAndAfterPedestrianCollisionAlarm=44,
                    WarningVolume=45
                  }
               }
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("010000F36438052D030002000122231D26000000040000000A1E09152B08180F0E14291011122A16170D13280C0B1C192C1B1A2425270607202101020304", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x8103>("010000F36438052D030002000122231D26000000040000000A1E09152B08180F0E14291011122A16170D13280C0B1C192C1B1A2425270607202101020304".ToHexBytes());
            JT808_0x8103_0xF364 jT808_0X8103_0XF364 = jT808UploadLocationRequest.ParamList[0] as JT808_0x8103_0xF364;
            Assert.Equal(1, jT808_0X8103_0XF364.ActiveDistancePhotographyDistanceInterval);
            Assert.Equal(2, jT808_0X8103_0XF364.ActivelyTimePhotoInterval);
            Assert.Equal(3, jT808_0X8103_0XF364.ActivePhotographyStrategy);
            Assert.Equal(4u, jT808_0X8103_0XF364.AlarmEnable);
            Assert.Equal(5, jT808_0X8103_0XF364.AlarmJudgeSpeedThreshold);
            Assert.Equal(6, jT808_0X8103_0XF364.AlarmPhotoVehicleCloseDistance);
            Assert.Equal(7, jT808_0X8103_0XF364.AlarmPhotoVehicleCloseDistanceInterval);
            Assert.Equal(8, jT808_0X8103_0XF364.BarrierAlarmPhotographs);
            Assert.Equal(9, jT808_0X8103_0XF364.DistanceThresholdObstacleAlarm);
            Assert.Equal(10u, jT808_0X8103_0XF364.EventEnable);
            Assert.Equal(11, jT808_0X8103_0XF364.ForwardCollisionAlarmInterval);
            Assert.Equal(12, jT808_0X8103_0XF364.ForwardCollisionAlarmPhotographs);
            Assert.Equal(13, jT808_0X8103_0XF364.ForwardCollisionWarningTimeThreshold);
            Assert.Equal(14, jT808_0X8103_0XF364.FrequentAlarmJudgmentNumberChannelChange);
            Assert.Equal(15, jT808_0X8103_0XF364.FrequentChannelChangeAlarmJudgmentTimePeriod);
            Assert.Equal(16, jT808_0X8103_0XF364.FrequentChannelChangeAlarmPhotos);
            Assert.Equal(17, jT808_0X8103_0XF364.FrequentLaneChangeAlarmInterval);
            Assert.Equal(18, jT808_0X8103_0XF364.GradedSpeedThresholdLaneDeviationAlarm);
            Assert.Equal(19, jT808_0X8103_0XF364.HierarchicalSpeedThresholdForwardCollisionWarning);
            Assert.Equal(20, jT808_0X8103_0XF364.HierarchicalSpeedThresholdFrequentChannelChangeAlarm);
            Assert.Equal(21, jT808_0X8103_0XF364.HierarchicalSpeedThresholdObstacleAlarm);
            Assert.Equal(22, jT808_0X8103_0XF364.LaneDepartureAlarmPhoto);
            Assert.Equal(23, jT808_0X8103_0XF364.LaneDepartureAlarmPhotoInterval);
            Assert.Equal(24, jT808_0X8103_0XF364.ObstacleAlarmInterval);
            Assert.Equal(25, jT808_0X8103_0XF364.PedestrianCollisionAlarmEnablingSpeedThreshold);
            Assert.Equal(26, jT808_0X8103_0XF364.PedestrianCollisionAlarmInterval);
            Assert.Equal(27, jT808_0X8103_0XF364.PedestrianCollisionAlarmPhotos);
            Assert.Equal(28, jT808_0X8103_0XF364.PedestrianCollisionAlarmTimeThreshold);
            Assert.Equal(29, jT808_0X8103_0XF364.PhotoResolution);
            Assert.Equal(30, jT808_0X8103_0XF364.Placeholder1);
            Assert.Equal(new byte[] { 1, 2, 3, 4 }.ToHexString(), jT808_0X8103_0XF364.Placeholder2.ToHexString());
            Assert.Equal(32, jT808_0X8103_0XF364.RoadSignRecognitionPhotographs);
            Assert.Equal(33, jT808_0X8103_0XF364.RoadSignRecognitionPhotographsInterval);
            Assert.Equal(34, jT808_0X8103_0XF364.SingleInitiativePhotos);
            Assert.Equal(35, jT808_0X8103_0XF364.SingleInitiativePhotosInterval);
            Assert.Equal(36, jT808_0X8103_0XF364.VehicleDistanceMonitoringAlarmDistanceThreshold);
            Assert.Equal(37, jT808_0X8103_0XF364.VehicleDistanceMonitoringAndAlarmClassificationSpeedThreshold);
            Assert.Equal(38, jT808_0X8103_0XF364.VideoRecordingResolution);
            Assert.Equal(39, jT808_0X8103_0XF364.VideoRecordingTimeBeforeAndAfterAlarmVehicleProximity);
            Assert.Equal(40, jT808_0X8103_0XF364.VideoRecordingTimeBeforeAndAfterForwardCollisionAlarm);
            Assert.Equal(41, jT808_0X8103_0XF364.VideoRecordingTimeBeforeAndAfterFrequentLaneChangeAlarm);
            Assert.Equal(42, jT808_0X8103_0XF364.VideoRecordingTimeBeforeAndAfterLaneDepartureAlarm);
            Assert.Equal(43, jT808_0X8103_0XF364.VideoRecordingTimeBeforeAndAfterObstacleAlarm);
            Assert.Equal(44, jT808_0X8103_0XF364.VideoRecordingTimeBeforeAndAfterPedestrianCollisionAlarm);
            Assert.Equal(45, jT808_0X8103_0XF364.WarningVolume);
            Assert.Equal(JT808_SuBiao_Constants.JT808_0X8103_0xF364, jT808_0X8103_0XF364.ParamId);
        }

        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x8103>("010000F36438052D030002000122231D26000000040000000A1E09152B08180F0E14291011122A16170D13280C0B1C192C1B1A2425270607202101020304".ToHexBytes());
        }
    }
}
