using JT808.Protocol.Extensions.SuBiao.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.Extensions.SuBiao.MessageBody
{
    /// <summary>
    /// 驾驶员状态监测系统参数
    /// </summary>
    public class JT808_0x8103_0xF365 : JT808MessagePackFormatter<JT808_0x8103_0xF365>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 驾驶员状态监测系统参数
        /// </summary>
        public uint ParamId { get; set; } = JT808_SuBiao_Constants.JT808_0X8103_0xF365;
        /// <summary>
        /// 驾驶员状态监测系统参数长度
        /// </summary>
        public byte ParamLength { get; set; }
        /// <summary>
        /// 报警判断速度阈值
        /// </summary>
        public byte AlarmJudgeSpeedThreshold { get; set; }
        /// <summary>
        /// 报警提示音量
        /// </summary>
        public byte WarningVolume { get; set; }
        /// <summary>
        /// 主动拍照策略
        /// </summary>
        public byte ActivePhotographyStrategy { get; set; }
        /// <summary>
        /// 主动定时拍照时间间隔
        /// </summary>
        public ushort ActivelyTimePhotoInterval { get; set; }
        /// <summary>
        /// 主动定距拍照距离间隔
        /// </summary>
        public ushort ActiveDistancePhotographyDistanceInterval { get; set; }
        /// <summary>
        /// 单次主动拍照张数
        /// </summary>
        public byte SingleInitiativePhotos { get; set; }
        /// <summary>
        /// 单次主动拍照时间间隔
        /// </summary>
        public byte SingleInitiativePhotosInterval { get; set; }
        /// <summary>
        /// 拍照分辨率
        /// </summary>
        public byte PhotoResolution { get; set; }
        /// <summary>
        /// 视频录制分辨率
        /// </summary>
        public byte VideoRecordingResolution { get; set; }
        /// <summary>
        /// 报警使能
        /// </summary>
        public uint AlarmEnable { get; set; }
        /// <summary>
        /// 事件使能
        /// </summary>
        public uint EventEnable { get; set; }
        /// <summary>
        /// 吸烟报警判断时间间隔
        /// </summary>
        public ushort TimeIntervalSmokingAlarmJudgment { get; set; }
        /// <summary>
        /// 接打电话报警判断时间间隔
        /// </summary>
        public ushort CallAlarmDetermineTimeInterval{ get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public byte[] Reserve { get; set; } = new byte[3];
        /// <summary>
        /// 疲劳驾驶报警分级速度阈值
        /// </summary>
        public byte GradedSpeedThresholdFatigueDrivingAlarm { get; set; }
        /// <summary>
        /// 疲劳驾驶报警前后视频录制时间
        /// </summary>
        public byte VideoRecordingTimeBeforeAndAfterFatigueDrivingAlarm { get; set; }
        /// <summary>
        /// 疲劳驾驶报警拍照张数
        /// </summary>
        public byte FatigueDrivingAlarmPhotograph { get; set; }
        /// <summary>
        /// 疲劳驾驶报警拍照间隔时间
        /// </summary>
        public byte FatigueDrivingAlarmPhotographInterval { get; set; }
        /// <summary>
        /// 接打电话报警分级速度阈值
        /// </summary>
        public byte ClassifiedSpeedThresholdCallAlarm{ get; set; }
        /// <summary>
        /// 接打电话报警前后视频录制时间
        /// </summary>
        public byte VideoRecordingTimeBeforeAndAfterCallAlarm{ get; set; }
        /// <summary>
        /// 接打电话报警拍驾驶员面部特征照片张数
        /// </summary>
        public byte CallAlarmTakePicturesDriverFacialFeatures{ get; set; }
        /// <summary>
        /// 接打电话报警拍驾驶员面部特征照片间隔时间
        /// </summary>
        public byte CallAlarmTakePicturesDriverFacialFeaturesInterval { get; set; }
        /// <summary>
        /// 抽烟报警分级车速阈值
        /// </summary>
        public byte ClassifiedSpeedThresholdSmokingAlarm{ get; set; }
        /// <summary>
        /// 抽烟报警前后视频录制时间
        /// </summary>
        public byte VideoRecordingTimeBeforeAndAfterSmokingAlarm{ get; set; }
        /// <summary>
        /// 抽烟报警拍驾驶员面部特征照片张数
        /// </summary>
        public byte SmokingAlarmPhotographsDriverFaceCharacteristics { get; set; }
        /// <summary>
        /// 抽烟报警拍驾驶员面部特征照片间隔时间
        /// </summary>
        public byte SmokingAlarmPhotographsDriverFaceCharacteristicsInterval { get; set; }
        /// <summary>
        /// 分神驾驶报警分级车速阈值
        /// </summary>
        public byte ClassifiedSpeedThresholdDistractedDrivingAlarm { get; set; }
        /// <summary>
        /// 分神驾驶报警拍照张数
        /// </summary>
        public byte DistractedDrivingAlarmPhotography{ get; set; }
        /// <summary>
        /// 分神驾驶报警拍照间隔时间
        /// </summary>
        public byte DistractedDrivingAlarmPhotographyInterval { get; set; }
        /// <summary>
        /// 驾驶行为异常视频录制时间
        /// </summary>
        public byte VideoRecordingTimeAbnormalDrivingBehavior{ get; set; }
        /// <summary>
        /// 驾驶行为异常抓拍照片张数
        /// </summary>
        public byte PhotographsAbnormalDrivingBehavior{ get; set; }
        /// <summary>
        /// 驾驶行为异常拍照间隔
        /// </summary>
        public byte PictureIntervalAbnormalDrivingBehavior{ get; set; }
        /// <summary>
        /// 驾驶员身份识别触发
        /// </summary>
        public byte DriverIdentificationTrigger { get; set; }
        /// <summary>
        /// 保留字段
        /// </summary>
        public byte[] Retain { get; set; } = new byte[2];
        /// <summary>
        /// 驾驶员状态监测系统参数
        /// </summary>
        public string Description => "驾驶员状态监测系统参数";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0xF365 value = new JT808_0x8103_0xF365();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ParamId.ReadNumber()}]参数ID", value.ParamId);
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]参数长度", value.ParamLength);
            value.AlarmJudgeSpeedThreshold = reader.ReadByte();
            writer.WriteNumber($"[{value.AlarmJudgeSpeedThreshold.ReadNumber()}]报警判断速度阈值", value.AlarmJudgeSpeedThreshold);
            value.WarningVolume = reader.ReadByte();
            writer.WriteNumber($"[{value.WarningVolume.ReadNumber()}]报警提示音量", value.WarningVolume);
            value.ActivePhotographyStrategy = reader.ReadByte();
            var activePhotographyStrategy = (ActivePhotographyStrategyType)ActivePhotographyStrategy;
            writer.WriteNumber($"[{value.ActivePhotographyStrategy.ReadNumber()}]主动拍照策略-{activePhotographyStrategy.ToString()}", value.ActivePhotographyStrategy);
            value.ActivelyTimePhotoInterval = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ActivelyTimePhotoInterval.ReadNumber()}]主动定时拍照时间间隔", value.ActivelyTimePhotoInterval);
            value.ActiveDistancePhotographyDistanceInterval = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ActiveDistancePhotographyDistanceInterval.ReadNumber()}]主动定距拍照距离间隔", value.ActiveDistancePhotographyDistanceInterval);
            value.SingleInitiativePhotos = reader.ReadByte();
            writer.WriteNumber($"[{value.SingleInitiativePhotos.ReadNumber()}]单次主动拍照张数", value.SingleInitiativePhotos);
            value.SingleInitiativePhotosInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.SingleInitiativePhotosInterval.ReadNumber()}]单次主动拍照时间间隔", value.SingleInitiativePhotosInterval);
            value.PhotoResolution = reader.ReadByte();
            var photoResolutionType = (PhotoResolutionType)value.PhotoResolution;
            writer.WriteNumber($"[{value.PhotoResolution.ReadNumber()}]拍照分辨率-{photoResolutionType.ToString()}", value.PhotoResolution);
            value.VideoRecordingResolution = reader.ReadByte();
            var videoRecordingResolution = (VideoRecordingResolutionType)value.VideoRecordingResolution;
            writer.WriteNumber($"[{value.VideoRecordingResolution.ReadNumber()}]视频录制分辨率-{videoRecordingResolution.ToString()}", value.VideoRecordingResolution);
            value.AlarmEnable = reader.ReadUInt32();
            writer.WriteNumber($"[{value.AlarmEnable.ReadNumber()}]报警使能", value.AlarmEnable);
            var alarmEnableBits = Convert.ToString(value.AlarmEnable, 2).PadLeft(32, '0').AsSpan();
            writer.WriteStartObject("报警使能对象");
            writer.WriteString("[bit30~bit31]预留", alarmEnableBits.Slice(30, 2).ToString());
            writer.WriteString("[bit17~bit29]用户自定义", alarmEnableBits.Slice(17, 13).ToString());
            writer.WriteString("[bit16]道路标识超限报警", alarmEnableBits[16] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit12~bit15]道路标识超限报警", alarmEnableBits.Slice(12, 4).ToString());
            writer.WriteString("[bit11]车距过近二级报警", alarmEnableBits[11] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit10]车距过近一级报警", alarmEnableBits[10] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit9]行人碰撞二级报警", alarmEnableBits[9] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit8]行人碰撞一级报警", alarmEnableBits[8] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit7]前向碰撞二级报警", alarmEnableBits[7] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit6]前向碰撞一级报警", alarmEnableBits[6] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit5]车道偏离二级报警", alarmEnableBits[5] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit4]车道偏离一级报警", alarmEnableBits[4] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit3]频繁变道二级报警", alarmEnableBits[3] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit2]频繁变道一级报警", alarmEnableBits[2] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit1]障碍检测二级报警", alarmEnableBits[1] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit0]障碍检测一级报警", alarmEnableBits[0] == '0' ? "关闭" : "打开");
            writer.WriteEndObject();
            value.EventEnable = reader.ReadUInt32();
            writer.WriteNumber($"[{value.EventEnable.ReadNumber()}]事件使能", value.EventEnable);
            var eventEnableBits = Convert.ToString(value.EventEnable, 2).PadLeft(32, '0').AsSpan();
            writer.WriteStartObject("事件使能对象");
            writer.WriteString("[bit30~bit31]预留", eventEnableBits.Slice(30, 2).ToString());
            writer.WriteString("[bit2~bit29]用户自定义", alarmEnableBits.Slice(2, 28).ToString());
            writer.WriteString("[bit1]主动拍照", alarmEnableBits[1] == '0' ? "关闭" : "打开");
            writer.WriteString("[bit0]道路标识识别", alarmEnableBits[0] == '0' ? "关闭" : "打开");
            writer.WriteEndObject();
            value.TimeIntervalSmokingAlarmJudgment = reader.ReadUInt16();
            writer.WriteNumber($"[{value.TimeIntervalSmokingAlarmJudgment.ReadNumber()}]吸烟报警判断时间间隔", value.TimeIntervalSmokingAlarmJudgment);
            value.CallAlarmDetermineTimeInterval = reader.ReadUInt16();
            writer.WriteNumber($"[{value.CallAlarmDetermineTimeInterval.ReadNumber()}]接打电话报警判断时间间隔", value.CallAlarmDetermineTimeInterval);
            value.Reserve = reader.ReadArray(3).ToArray();
            writer.WriteString("预留字段", value.Reserve.ToHexString());
            value.GradedSpeedThresholdFatigueDrivingAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.GradedSpeedThresholdFatigueDrivingAlarm.ReadNumber()}]疲劳驾驶报警分级速度阈值", value.GradedSpeedThresholdFatigueDrivingAlarm);
            value.VideoRecordingTimeBeforeAndAfterFatigueDrivingAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeBeforeAndAfterFatigueDrivingAlarm.ReadNumber()}]疲劳驾驶报警前后视频录制时间", value.VideoRecordingTimeBeforeAndAfterFatigueDrivingAlarm);
            value.FatigueDrivingAlarmPhotograph = reader.ReadByte();
            writer.WriteNumber($"[{value.FatigueDrivingAlarmPhotograph.ReadNumber()}]疲劳驾驶报警拍照张数", value.FatigueDrivingAlarmPhotograph);
            value.FatigueDrivingAlarmPhotographInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.FatigueDrivingAlarmPhotographInterval.ReadNumber()}]疲劳驾驶报警拍照间隔时间", value.FatigueDrivingAlarmPhotographInterval);
            value.ClassifiedSpeedThresholdCallAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.ClassifiedSpeedThresholdCallAlarm.ReadNumber()}]接打电话报警分级速度阈值", value.ClassifiedSpeedThresholdCallAlarm);
            value.VideoRecordingTimeBeforeAndAfterCallAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeBeforeAndAfterCallAlarm.ReadNumber()}]接打电话报警前后视频录制时间", value.VideoRecordingTimeBeforeAndAfterCallAlarm);
            value.CallAlarmTakePicturesDriverFacialFeatures = reader.ReadByte();
            writer.WriteNumber($"[{value.CallAlarmTakePicturesDriverFacialFeatures.ReadNumber()}]接打电话报警拍驾驶员面部特征照片张数", value.CallAlarmTakePicturesDriverFacialFeatures);
            value.CallAlarmTakePicturesDriverFacialFeaturesInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.CallAlarmTakePicturesDriverFacialFeaturesInterval.ReadNumber()}]接打电话报警拍驾驶员面部特征照片间隔时间", value.CallAlarmTakePicturesDriverFacialFeaturesInterval);
            value.ClassifiedSpeedThresholdSmokingAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.ClassifiedSpeedThresholdSmokingAlarm.ReadNumber()}]抽烟报警分级车速阈值", value.ClassifiedSpeedThresholdSmokingAlarm);
            value.VideoRecordingTimeBeforeAndAfterSmokingAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeBeforeAndAfterSmokingAlarm.ReadNumber()}]抽烟报警前后视频录制时间", value.VideoRecordingTimeBeforeAndAfterSmokingAlarm);
            value.SmokingAlarmPhotographsDriverFaceCharacteristics = reader.ReadByte();
            writer.WriteNumber($"[{value.SmokingAlarmPhotographsDriverFaceCharacteristics.ReadNumber()}]抽烟报警拍驾驶员面部特征照片张数", value.SmokingAlarmPhotographsDriverFaceCharacteristics);
            value.SmokingAlarmPhotographsDriverFaceCharacteristicsInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.SmokingAlarmPhotographsDriverFaceCharacteristicsInterval.ReadNumber()}]抽烟报警拍驾驶员面部特征照片间隔时间", value.SmokingAlarmPhotographsDriverFaceCharacteristicsInterval);
            value.ClassifiedSpeedThresholdDistractedDrivingAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.ClassifiedSpeedThresholdDistractedDrivingAlarm.ReadNumber()}]分神驾驶报警分级车速阈值", value.ClassifiedSpeedThresholdDistractedDrivingAlarm);
            value.DistractedDrivingAlarmPhotography = reader.ReadByte();
            writer.WriteNumber($"[{value.DistractedDrivingAlarmPhotography.ReadNumber()}]分神驾驶报警拍照张数", value.DistractedDrivingAlarmPhotography);
            value.DistractedDrivingAlarmPhotographyInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.DistractedDrivingAlarmPhotographyInterval.ReadNumber()}]分神驾驶报警拍照间隔时间", value.DistractedDrivingAlarmPhotographyInterval);
            value.VideoRecordingTimeAbnormalDrivingBehavior = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeAbnormalDrivingBehavior.ReadNumber()}]驾驶行为异常视频录制时间", value.VideoRecordingTimeAbnormalDrivingBehavior);
            value.PhotographsAbnormalDrivingBehavior = reader.ReadByte();
            writer.WriteNumber($"[{value.PhotographsAbnormalDrivingBehavior.ReadNumber()}]驾驶行为异常抓拍照片张数", value.PhotographsAbnormalDrivingBehavior);
            value.PictureIntervalAbnormalDrivingBehavior = reader.ReadByte();
            writer.WriteNumber($"[{value.PictureIntervalAbnormalDrivingBehavior.ReadNumber()}]驾驶行为异常拍照间隔", value.PictureIntervalAbnormalDrivingBehavior);
            value.DriverIdentificationTrigger = reader.ReadByte();
            writer.WriteNumber($"[{value.DriverIdentificationTrigger.ReadNumber()}]驾驶员身份识别触发", value.DriverIdentificationTrigger);
            value.Retain = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            writer.WriteString("保留字段", value.Retain.ToHexString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0xF365 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0xF365 value = new JT808_0x8103_0xF365();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.AlarmJudgeSpeedThreshold = reader.ReadByte();
            value.WarningVolume = reader.ReadByte();
            value.ActivePhotographyStrategy = reader.ReadByte();
            value.ActivelyTimePhotoInterval = reader.ReadUInt16();
            value.ActiveDistancePhotographyDistanceInterval = reader.ReadUInt16();
            value.SingleInitiativePhotos = reader.ReadByte();
            value.SingleInitiativePhotosInterval = reader.ReadByte();
            value.PhotoResolution = reader.ReadByte();
            value.VideoRecordingResolution = reader.ReadByte();
            value.AlarmEnable = reader.ReadUInt32();
            value.EventEnable = reader.ReadUInt32();
            value.TimeIntervalSmokingAlarmJudgment = reader.ReadUInt16();
            value.CallAlarmDetermineTimeInterval = reader.ReadUInt16();
            value.Reserve = reader.ReadArray(3).ToArray();
            value.GradedSpeedThresholdFatigueDrivingAlarm = reader.ReadByte();
            value.VideoRecordingTimeBeforeAndAfterFatigueDrivingAlarm = reader.ReadByte();
            value.FatigueDrivingAlarmPhotograph = reader.ReadByte();
            value.FatigueDrivingAlarmPhotographInterval = reader.ReadByte();
            value.ClassifiedSpeedThresholdCallAlarm = reader.ReadByte();
            value.VideoRecordingTimeBeforeAndAfterCallAlarm = reader.ReadByte();
            value.CallAlarmTakePicturesDriverFacialFeatures = reader.ReadByte();
            value.CallAlarmTakePicturesDriverFacialFeaturesInterval = reader.ReadByte();
            value.ClassifiedSpeedThresholdSmokingAlarm = reader.ReadByte();
            value.VideoRecordingTimeBeforeAndAfterSmokingAlarm = reader.ReadByte();
            value.SmokingAlarmPhotographsDriverFaceCharacteristics = reader.ReadByte();
            value.SmokingAlarmPhotographsDriverFaceCharacteristicsInterval = reader.ReadByte();
            value.ClassifiedSpeedThresholdDistractedDrivingAlarm = reader.ReadByte();
            value.DistractedDrivingAlarmPhotography = reader.ReadByte();
            value.DistractedDrivingAlarmPhotographyInterval = reader.ReadByte();
            value.VideoRecordingTimeAbnormalDrivingBehavior = reader.ReadByte();
            value.PhotographsAbnormalDrivingBehavior = reader.ReadByte();
            value.PictureIntervalAbnormalDrivingBehavior = reader.ReadByte();
            value.DriverIdentificationTrigger = reader.ReadByte();
            value.Retain = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0xF365 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int ParamLengthPosition);
            writer.WriteByte(value.AlarmJudgeSpeedThreshold);
            writer.WriteByte(value.WarningVolume);
            writer.WriteByte(value.ActivePhotographyStrategy);
            writer.WriteUInt16(value.ActivelyTimePhotoInterval);
            writer.WriteUInt16(value.ActiveDistancePhotographyDistanceInterval);
            writer.WriteByte(value.SingleInitiativePhotos);
            writer.WriteByte(value.SingleInitiativePhotosInterval);
            writer.WriteByte(value.PhotoResolution);
            writer.WriteByte(value.VideoRecordingResolution);
            writer.WriteUInt32(value.AlarmEnable);
            writer.WriteUInt32(value.EventEnable);
            writer.WriteUInt16(value.TimeIntervalSmokingAlarmJudgment);
            writer.WriteUInt16(value.CallAlarmDetermineTimeInterval);
            writer.WriteArray(value.Reserve);
            writer.WriteByte(value.GradedSpeedThresholdFatigueDrivingAlarm);
            writer.WriteByte(value.VideoRecordingTimeBeforeAndAfterFatigueDrivingAlarm);
            writer.WriteByte(value.FatigueDrivingAlarmPhotograph);
            writer.WriteByte(value.FatigueDrivingAlarmPhotographInterval);
            writer.WriteByte(value.ClassifiedSpeedThresholdCallAlarm);
            writer.WriteByte(value.VideoRecordingTimeBeforeAndAfterCallAlarm);
            writer.WriteByte(value.CallAlarmTakePicturesDriverFacialFeatures);
            writer.WriteByte(value.CallAlarmTakePicturesDriverFacialFeaturesInterval);
            writer.WriteByte(value.ClassifiedSpeedThresholdSmokingAlarm);
            writer.WriteByte(value.VideoRecordingTimeBeforeAndAfterSmokingAlarm);
            writer.WriteByte(value.SmokingAlarmPhotographsDriverFaceCharacteristics);
            writer.WriteByte(value.SmokingAlarmPhotographsDriverFaceCharacteristicsInterval);
            writer.WriteByte(value.ClassifiedSpeedThresholdDistractedDrivingAlarm);
            writer.WriteByte(value.DistractedDrivingAlarmPhotography);
            writer.WriteByte(value.DistractedDrivingAlarmPhotographyInterval);
            writer.WriteByte(value.VideoRecordingTimeAbnormalDrivingBehavior);
            writer.WriteByte(value.PhotographsAbnormalDrivingBehavior);
            writer.WriteByte(value.PictureIntervalAbnormalDrivingBehavior);
            writer.WriteByte(value.DriverIdentificationTrigger);
            writer.WriteArray(value.Retain);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - ParamLengthPosition - 1), ParamLengthPosition);
        }
    }
}
