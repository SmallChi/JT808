using JT808.Protocol.Extensions.SuBiao.MessageBody;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.SuBiao.Test
{
    public class JT808_0x8103_0xF365_Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x8103_0xF365_Test()
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
                  new JT808_0x8103_0xF365{
                        ActiveDistancePhotographyDistanceInterval=1,
                        ActivelyTimePhotoInterval=2,
                        ActivePhotographyStrategy=3,
                        AlarmEnable=4,
                        AlarmJudgeSpeedThreshold=5,
                        EventEnable=10,      
                        PhotoResolution=29,
                        SingleInitiativePhotos=34,
                        SingleInitiativePhotosInterval=35,
                        VideoRecordingResolution=38,
                        WarningVolume=45,
                        CallAlarmDetermineTimeInterval=46,
                        CallAlarmTakePicturesDriverFacialFeatures=47,
                        CallAlarmTakePicturesDriverFacialFeaturesInterval=48,
                        ClassifiedSpeedThresholdCallAlarm=49,
                        ClassifiedSpeedThresholdDistractedDrivingAlarm=50,
                        ClassifiedSpeedThresholdSmokingAlarm=51,
                        DistractedDrivingAlarmPhotography=52,
                        DistractedDrivingAlarmPhotographyInterval=53,
                        DriverIdentificationTrigger=54,
                        FatigueDrivingAlarmPhotograph=55,
                        FatigueDrivingAlarmPhotographInterval=56,
                        GradedSpeedThresholdFatigueDrivingAlarm=57,
                        PhotographsAbnormalDrivingBehavior=58,
                        PictureIntervalAbnormalDrivingBehavior=59,
                        Reserve=new byte[]{1,2,3 },
                        Retain=new byte[]{5,6 },
                        SmokingAlarmPhotographsDriverFaceCharacteristics=60,
                        SmokingAlarmPhotographsDriverFaceCharacteristicsInterval=61,
                        TimeIntervalSmokingAlarmJudgment=62,
                        VideoRecordingTimeAbnormalDrivingBehavior=63,
                        VideoRecordingTimeBeforeAndAfterCallAlarm=64,
                        VideoRecordingTimeBeforeAndAfterFatigueDrivingAlarm=65,
                        VideoRecordingTimeBeforeAndAfterSmokingAlarm=66
                  }
               }
            };
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("010000F3652F052D030002000122231D26000000040000000A003E002E0102033941373831402F3033423C3D3234353F3A3B360506", hex);
        }
        [Fact]
        public void Deserialize()
        {
            var jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x8103>("010000F3652F052D030002000122231D26000000040000000A003E002E0102033941373831402F3033423C3D3234353F3A3B360506".ToHexBytes());
            JT808_0x8103_0xF365 jT808_0X8103_0XF365 = jT808UploadLocationRequest.ParamList[0] as JT808_0x8103_0xF365;
            Assert.Equal(1, jT808_0X8103_0XF365.ActiveDistancePhotographyDistanceInterval);
            Assert.Equal(2, jT808_0X8103_0XF365.ActivelyTimePhotoInterval);
            Assert.Equal(3, jT808_0X8103_0XF365.ActivePhotographyStrategy);
            Assert.Equal(4u, jT808_0X8103_0XF365.AlarmEnable);
            Assert.Equal(5, jT808_0X8103_0XF365.AlarmJudgeSpeedThreshold);
            Assert.Equal(10u, jT808_0X8103_0XF365.EventEnable);
            Assert.Equal(29, jT808_0X8103_0XF365.PhotoResolution);
            Assert.Equal(34, jT808_0X8103_0XF365.SingleInitiativePhotos);
            Assert.Equal(35, jT808_0X8103_0XF365.SingleInitiativePhotosInterval);
            Assert.Equal(38, jT808_0X8103_0XF365.VideoRecordingResolution);
            Assert.Equal(45, jT808_0X8103_0XF365.WarningVolume);
            Assert.Equal(46, jT808_0X8103_0XF365.CallAlarmDetermineTimeInterval);
            Assert.Equal(47, jT808_0X8103_0XF365.CallAlarmTakePicturesDriverFacialFeatures);
            Assert.Equal(48, jT808_0X8103_0XF365.CallAlarmTakePicturesDriverFacialFeaturesInterval);
            Assert.Equal(49, jT808_0X8103_0XF365.ClassifiedSpeedThresholdCallAlarm);
            Assert.Equal(50, jT808_0X8103_0XF365.ClassifiedSpeedThresholdDistractedDrivingAlarm);
            Assert.Equal(51, jT808_0X8103_0XF365.ClassifiedSpeedThresholdSmokingAlarm);
            Assert.Equal(52, jT808_0X8103_0XF365.DistractedDrivingAlarmPhotography);
            Assert.Equal(53, jT808_0X8103_0XF365.DistractedDrivingAlarmPhotographyInterval);
            Assert.Equal(54, jT808_0X8103_0XF365.DriverIdentificationTrigger);
            Assert.Equal(55, jT808_0X8103_0XF365.FatigueDrivingAlarmPhotograph);
            Assert.Equal(56, jT808_0X8103_0XF365.FatigueDrivingAlarmPhotographInterval);
            Assert.Equal(57, jT808_0X8103_0XF365.GradedSpeedThresholdFatigueDrivingAlarm);
            Assert.Equal(58, jT808_0X8103_0XF365.PhotographsAbnormalDrivingBehavior);
            Assert.Equal(59, jT808_0X8103_0XF365.PictureIntervalAbnormalDrivingBehavior);
            Assert.Equal(new byte[] { 1, 2, 3 }.ToHexString(), jT808_0X8103_0XF365.Reserve.ToHexString());
            Assert.Equal(new byte[] { 5, 6 }.ToHexString(), jT808_0X8103_0XF365.Retain.ToHexString());
            Assert.Equal(60, jT808_0X8103_0XF365.SmokingAlarmPhotographsDriverFaceCharacteristics);
            Assert.Equal(61, jT808_0X8103_0XF365.SmokingAlarmPhotographsDriverFaceCharacteristicsInterval);
            Assert.Equal(62, jT808_0X8103_0XF365.TimeIntervalSmokingAlarmJudgment);
            Assert.Equal(63, jT808_0X8103_0XF365.VideoRecordingTimeAbnormalDrivingBehavior);
            Assert.Equal(64, jT808_0X8103_0XF365.VideoRecordingTimeBeforeAndAfterCallAlarm);
            Assert.Equal(65, jT808_0X8103_0XF365.VideoRecordingTimeBeforeAndAfterFatigueDrivingAlarm);
            Assert.Equal(66, jT808_0X8103_0XF365.VideoRecordingTimeBeforeAndAfterSmokingAlarm);
            Assert.Equal(JT808_SuBiao_Constants.JT808_0X8103_0xF365, jT808_0X8103_0XF365.ParamId);
        }
        [Fact]
        public void Json()
        {
            var json = JT808Serializer.Analyze<JT808_0x8103>("010000F3652F052D030002000122231D26000000040000000A003E002E0102033941373831402F3033423C3D3234353F3A3B360506".ToHexBytes());
        }
    }
}
