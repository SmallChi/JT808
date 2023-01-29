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
    /// 高级驾驶辅助系统参数
    /// </summary>
    public class JT808_0x8103_0xF364 : JT808MessagePackFormatter<JT808_0x8103_0xF364>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 高级驾驶辅助系统参数
        /// </summary>
        public uint ParamId { get; set; } = JT808_SuBiao_Constants.JT808_0X8103_0xF364;
        /// <summary>
        /// 高级驾驶辅助系统参数长度
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
        /// <see cref="JT808.Protocol.Extensions.SuBiao.Enums.ActivePhotographyStrategyType"/>
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
        /// <see cref="JT808.Protocol.Extensions.SuBiao.Enums.PhotoResolutionType"/>
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
        /// 预留字段
        /// </summary>
        public byte Placeholder1 { get; set; }
        /// <summary>
        /// 障碍物报警距离阈值
        /// </summary>
        public byte DistanceThresholdObstacleAlarm { get; set; }
        /// <summary>
        /// 障碍物报警分级速度阈值
        /// </summary>
        public byte HierarchicalSpeedThresholdObstacleAlarm { get; set; }
        /// <summary>
        /// 障碍物报警前后视频录制时间
        /// </summary>
        public byte VideoRecordingTimeBeforeAndAfterObstacleAlarm { get; set; }
        /// <summary>
        /// 障碍物报警拍照张数
        /// </summary>
        public byte BarrierAlarmPhotographs { get; set; }
        /// <summary>
        /// 障碍物报警拍照间隔
        /// </summary>
        public byte ObstacleAlarmInterval { get; set; }
        /// <summary>
        /// 频繁变道报警判断时间段
        /// </summary>
        public byte FrequentChannelChangeAlarmJudgmentTimePeriod { get; set; }

        /// <summary>
        /// 频繁变道报警判断次数
        /// </summary>
        public byte FrequentAlarmJudgmentNumberChannelChange { get; set; }
        /// <summary>
        /// 频繁变道报警分级速度阈值
        /// </summary>
        public byte HierarchicalSpeedThresholdFrequentChannelChangeAlarm { get; set; }
        /// <summary>
        /// 频繁变道报警前后视频录制时间
        /// </summary>
        public byte VideoRecordingTimeBeforeAndAfterFrequentLaneChangeAlarm { get; set; }
        /// <summary>
        /// 频繁变道报警拍照张数
        /// </summary>
        public byte FrequentChannelChangeAlarmPhotos { get; set; }
        /// <summary>
        /// 频繁变道报警拍照间隔
        /// </summary>
        public byte FrequentLaneChangeAlarmInterval { get; set; }
        /// <summary>
        /// 车道偏离报警分级速度阈值
        /// </summary>
        public byte GradedSpeedThresholdLaneDeviationAlarm { get; set; }
        /// <summary>
        /// 车道偏离报警前后视频录制时间
        /// </summary>
        public byte VideoRecordingTimeBeforeAndAfterLaneDepartureAlarm { get; set; }
        /// <summary>
        /// 车道偏离报警拍照张数
        /// </summary>
        public byte LaneDepartureAlarmPhoto { get; set; }
        /// <summary>
        /// 车道偏离报警拍照间隔
        /// </summary>
        public byte LaneDepartureAlarmPhotoInterval { get; set; }
        /// <summary>
        /// 前向碰撞报警时间阈值
        /// </summary>
        public byte ForwardCollisionWarningTimeThreshold { get; set; }
        /// <summary>
        /// 前向碰撞报警分级速度阈值
        /// </summary>
        public byte HierarchicalSpeedThresholdForwardCollisionWarning { get; set; }
        /// <summary>
        /// 前向碰撞报警前后视频录制时间
        /// </summary>
        public byte VideoRecordingTimeBeforeAndAfterForwardCollisionAlarm { get; set; }
        /// <summary>
        /// 前向碰撞报警拍照张数
        /// </summary>
        public byte ForwardCollisionAlarmPhotographs { get; set; }
        /// <summary>
        /// 前向碰撞报警拍照间隔
        /// </summary>
        public byte ForwardCollisionAlarmInterval { get; set; }
        /// <summary>
        /// 行人碰撞报警时间阈值
        /// </summary>
        public byte PedestrianCollisionAlarmTimeThreshold { get; set; }
        /// <summary>
        /// 行人碰撞报警使能速度阈值
        /// </summary>
        public byte PedestrianCollisionAlarmEnablingSpeedThreshold { get; set; }
        /// <summary>
        /// 行人碰撞报警前后视频录制时间
        /// </summary>
        public byte VideoRecordingTimeBeforeAndAfterPedestrianCollisionAlarm { get; set; }
        /// <summary>
        /// 行人碰撞报警拍照张数
        /// </summary>
        public byte PedestrianCollisionAlarmPhotos { get; set; }
        /// <summary>
        /// 行人碰撞报警拍照间隔
        /// </summary>
        public byte PedestrianCollisionAlarmInterval { get; set; }
        /// <summary>
        /// 车距监控报警距离阈值
        /// </summary>
        public byte VehicleDistanceMonitoringAlarmDistanceThreshold { get; set; }
        /// <summary>
        /// 车距监控报警分级速度阈值
        /// </summary>
        public byte VehicleDistanceMonitoringAndAlarmClassificationSpeedThreshold { get; set; }
        /// <summary>
        /// 车距过近报警前后视频录制时间
        /// </summary>
        public byte VideoRecordingTimeBeforeAndAfterAlarmVehicleProximity { get; set; }
        /// <summary>
        /// 车距过近报警拍照张数
        /// </summary>
        public byte AlarmPhotoVehicleCloseDistance { get; set; }
        /// <summary>
        /// 车距过近报警拍照间隔
        /// </summary>
        public byte AlarmPhotoVehicleCloseDistanceInterval { get; set; }
        /// <summary>
        /// 道路标志识别拍照张数
        /// </summary>
        public byte RoadSignRecognitionPhotographs { get; set; }
        /// <summary>
        /// 道路标志识别拍照间隔
        /// </summary>
        public byte RoadSignRecognitionPhotographsInterval { get; set; }
        /// <summary>
        /// 保留字段
        /// </summary>
        public byte[] Placeholder2 { get; set; } = new byte[4];
        /// <summary>
        /// 高级驾驶辅助系统参数
        /// </summary>
        public string Description => "高级驾驶辅助系统参数";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0xF364 value = new JT808_0x8103_0xF364();
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
            writer.WriteString("[bit30~bit31]预留", alarmEnableBits.Slice(30,2).ToString());
            writer.WriteString("[bit17~bit29]用户自定义", alarmEnableBits.Slice(17, 13).ToString());
            writer.WriteString("[bit16]道路标识超限报警", alarmEnableBits[16]=='0'?"关闭":"打开");
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
            value.Placeholder1 = reader.ReadByte();
            writer.WriteNumber($"[{value.Placeholder1.ReadNumber()}]预留字段", value.Placeholder1);
            value.DistanceThresholdObstacleAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.DistanceThresholdObstacleAlarm.ReadNumber()}]障碍物报警距离阈值", value.DistanceThresholdObstacleAlarm);
            value.HierarchicalSpeedThresholdObstacleAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.HierarchicalSpeedThresholdObstacleAlarm.ReadNumber()}]障碍物报警分级速度阈值", value.HierarchicalSpeedThresholdObstacleAlarm);
            value.VideoRecordingTimeBeforeAndAfterObstacleAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeBeforeAndAfterObstacleAlarm.ReadNumber()}]障碍物报警前后视频录制时间", value.VideoRecordingTimeBeforeAndAfterObstacleAlarm);
            value.BarrierAlarmPhotographs = reader.ReadByte();
            writer.WriteNumber($"[{value.BarrierAlarmPhotographs.ReadNumber()}]障碍物报警拍照张数", value.BarrierAlarmPhotographs);
            value.ObstacleAlarmInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.ObstacleAlarmInterval.ReadNumber()}]障碍物报警拍照间隔", value.ObstacleAlarmInterval);
            value.FrequentChannelChangeAlarmJudgmentTimePeriod = reader.ReadByte();
            writer.WriteNumber($"[{value.FrequentChannelChangeAlarmJudgmentTimePeriod.ReadNumber()}]频繁变道报警判断时间段", value.FrequentChannelChangeAlarmJudgmentTimePeriod);
            value.FrequentAlarmJudgmentNumberChannelChange = reader.ReadByte();
            writer.WriteNumber($"[{value.FrequentAlarmJudgmentNumberChannelChange.ReadNumber()}]频繁变道报警判断次数", value.FrequentAlarmJudgmentNumberChannelChange);
            value.HierarchicalSpeedThresholdFrequentChannelChangeAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.HierarchicalSpeedThresholdFrequentChannelChangeAlarm.ReadNumber()}]频繁变道报警分级速度阈值", value.HierarchicalSpeedThresholdFrequentChannelChangeAlarm);
            value.VideoRecordingTimeBeforeAndAfterFrequentLaneChangeAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeBeforeAndAfterFrequentLaneChangeAlarm.ReadNumber()}]频繁变道报警前后视频录制时间", value.VideoRecordingTimeBeforeAndAfterFrequentLaneChangeAlarm);
            value.FrequentChannelChangeAlarmPhotos = reader.ReadByte();
            writer.WriteNumber($"[{value.FrequentChannelChangeAlarmPhotos.ReadNumber()}]频繁变道报警拍照张数", value.FrequentChannelChangeAlarmPhotos);
            value.FrequentLaneChangeAlarmInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.FrequentLaneChangeAlarmInterval.ReadNumber()}]频繁变道报警拍照间隔", value.FrequentLaneChangeAlarmInterval);
            value.GradedSpeedThresholdLaneDeviationAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.GradedSpeedThresholdLaneDeviationAlarm.ReadNumber()}]车道偏离报警分级速度阈值", value.GradedSpeedThresholdLaneDeviationAlarm);
            value.VideoRecordingTimeBeforeAndAfterLaneDepartureAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeBeforeAndAfterLaneDepartureAlarm.ReadNumber()}]车道偏离报警前后视频录制时间", value.VideoRecordingTimeBeforeAndAfterLaneDepartureAlarm);
            value.LaneDepartureAlarmPhoto = reader.ReadByte();
            writer.WriteNumber($"[{value.LaneDepartureAlarmPhoto.ReadNumber()}]车道偏离报警拍照张数", value.LaneDepartureAlarmPhoto);
            value.LaneDepartureAlarmPhotoInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.LaneDepartureAlarmPhotoInterval.ReadNumber()}]车道偏离报警拍照间隔", value.LaneDepartureAlarmPhotoInterval);
            value.ForwardCollisionWarningTimeThreshold = reader.ReadByte();
            writer.WriteNumber($"[{value.ForwardCollisionWarningTimeThreshold.ReadNumber()}]前向碰撞报警时间阈值", value.ForwardCollisionWarningTimeThreshold);
            value.HierarchicalSpeedThresholdForwardCollisionWarning = reader.ReadByte();
            writer.WriteNumber($"[{value.HierarchicalSpeedThresholdForwardCollisionWarning.ReadNumber()}]前向碰撞报警分级速度阈值", value.HierarchicalSpeedThresholdForwardCollisionWarning);
            value.VideoRecordingTimeBeforeAndAfterForwardCollisionAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeBeforeAndAfterForwardCollisionAlarm.ReadNumber()}]前向碰撞报警前后视频录制时间", value.VideoRecordingTimeBeforeAndAfterForwardCollisionAlarm);
            value.ForwardCollisionAlarmPhotographs = reader.ReadByte();
            writer.WriteNumber($"[{value.ForwardCollisionAlarmPhotographs.ReadNumber()}]前向碰撞报警拍照张数", value.ForwardCollisionAlarmPhotographs);
            value.ForwardCollisionAlarmInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.ForwardCollisionAlarmInterval.ReadNumber()}]前向碰撞报警拍照间隔", value.ForwardCollisionAlarmInterval);
            value.PedestrianCollisionAlarmTimeThreshold = reader.ReadByte();
            writer.WriteNumber($"[{value.PedestrianCollisionAlarmTimeThreshold.ReadNumber()}]行人碰撞报警时间阈值", value.PedestrianCollisionAlarmTimeThreshold);
            value.PedestrianCollisionAlarmEnablingSpeedThreshold = reader.ReadByte();
            writer.WriteNumber($"[{value.PedestrianCollisionAlarmEnablingSpeedThreshold.ReadNumber()}]行人碰撞报警使能速度阈值", value.PedestrianCollisionAlarmEnablingSpeedThreshold);
            value.VideoRecordingTimeBeforeAndAfterPedestrianCollisionAlarm = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeBeforeAndAfterPedestrianCollisionAlarm.ReadNumber()}]行人碰撞报警前后视频录制时间", value.VideoRecordingTimeBeforeAndAfterPedestrianCollisionAlarm);
            value.PedestrianCollisionAlarmPhotos = reader.ReadByte();
            writer.WriteNumber($"[{value.PedestrianCollisionAlarmPhotos.ReadNumber()}]行人碰撞报警拍照张数", value.PedestrianCollisionAlarmPhotos);
            value.PedestrianCollisionAlarmInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.PedestrianCollisionAlarmInterval.ReadNumber()}]行人碰撞报警拍照间隔", value.PedestrianCollisionAlarmInterval);
            value.VehicleDistanceMonitoringAlarmDistanceThreshold = reader.ReadByte();
            writer.WriteNumber($"[{value.VehicleDistanceMonitoringAlarmDistanceThreshold.ReadNumber()}]车距监控报警距离阈值", value.VehicleDistanceMonitoringAlarmDistanceThreshold);
            value.VehicleDistanceMonitoringAndAlarmClassificationSpeedThreshold = reader.ReadByte();
            writer.WriteNumber($"[{value.VehicleDistanceMonitoringAndAlarmClassificationSpeedThreshold.ReadNumber()}]车距监控报警分级速度阈值", value.VehicleDistanceMonitoringAndAlarmClassificationSpeedThreshold);
            value.VideoRecordingTimeBeforeAndAfterAlarmVehicleProximity = reader.ReadByte();
            writer.WriteNumber($"[{value.VideoRecordingTimeBeforeAndAfterAlarmVehicleProximity.ReadNumber()}]车距过近报警前后视频录制时间", value.VideoRecordingTimeBeforeAndAfterAlarmVehicleProximity);
            value.AlarmPhotoVehicleCloseDistance = reader.ReadByte();
            writer.WriteNumber($"[{value.AlarmPhotoVehicleCloseDistance.ReadNumber()}]车距过近报警拍照张数", value.AlarmPhotoVehicleCloseDistance);
            value.AlarmPhotoVehicleCloseDistanceInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.AlarmPhotoVehicleCloseDistanceInterval.ReadNumber()}]车距过近报警拍照间隔", value.AlarmPhotoVehicleCloseDistanceInterval);
            value.RoadSignRecognitionPhotographs = reader.ReadByte();
            writer.WriteNumber($"[{value.RoadSignRecognitionPhotographs.ReadNumber()}]道路标志识别拍照张数", value.RoadSignRecognitionPhotographs);
            value.RoadSignRecognitionPhotographsInterval = reader.ReadByte();
            writer.WriteNumber($"[{value.RoadSignRecognitionPhotographsInterval.ReadNumber()}]道路标志识别拍照间隔", value.RoadSignRecognitionPhotographsInterval);
            value.Placeholder2 = reader.ReadArray(4).ToArray();
            writer.WriteString("保留字段", value.Placeholder2.ToHexString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0xF364 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0xF364 value = new JT808_0x8103_0xF364();
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
            value.Placeholder1 = reader.ReadByte();
            value.DistanceThresholdObstacleAlarm = reader.ReadByte();
            value.HierarchicalSpeedThresholdObstacleAlarm = reader.ReadByte();
            value.VideoRecordingTimeBeforeAndAfterObstacleAlarm = reader.ReadByte();
            value.BarrierAlarmPhotographs = reader.ReadByte();
            value.ObstacleAlarmInterval = reader.ReadByte();
            value.FrequentChannelChangeAlarmJudgmentTimePeriod = reader.ReadByte();
            value.FrequentAlarmJudgmentNumberChannelChange = reader.ReadByte();
            value.HierarchicalSpeedThresholdFrequentChannelChangeAlarm = reader.ReadByte();
            value.VideoRecordingTimeBeforeAndAfterFrequentLaneChangeAlarm = reader.ReadByte();
            value.FrequentChannelChangeAlarmPhotos = reader.ReadByte();
            value.FrequentLaneChangeAlarmInterval = reader.ReadByte();
            value.GradedSpeedThresholdLaneDeviationAlarm = reader.ReadByte();
            value.VideoRecordingTimeBeforeAndAfterLaneDepartureAlarm = reader.ReadByte();
            value.LaneDepartureAlarmPhoto = reader.ReadByte();
            value.LaneDepartureAlarmPhotoInterval = reader.ReadByte();
            value.ForwardCollisionWarningTimeThreshold = reader.ReadByte();
            value.HierarchicalSpeedThresholdForwardCollisionWarning = reader.ReadByte();
            value.VideoRecordingTimeBeforeAndAfterForwardCollisionAlarm = reader.ReadByte();
            value.ForwardCollisionAlarmPhotographs = reader.ReadByte();
            value.ForwardCollisionAlarmInterval = reader.ReadByte();
            value.PedestrianCollisionAlarmTimeThreshold = reader.ReadByte();
            value.PedestrianCollisionAlarmEnablingSpeedThreshold = reader.ReadByte();
            value.VideoRecordingTimeBeforeAndAfterPedestrianCollisionAlarm = reader.ReadByte();
            value.PedestrianCollisionAlarmPhotos = reader.ReadByte();
            value.PedestrianCollisionAlarmInterval = reader.ReadByte();
            value.VehicleDistanceMonitoringAlarmDistanceThreshold = reader.ReadByte();
            value.VehicleDistanceMonitoringAndAlarmClassificationSpeedThreshold = reader.ReadByte();
            value.VideoRecordingTimeBeforeAndAfterAlarmVehicleProximity = reader.ReadByte();
            value.AlarmPhotoVehicleCloseDistance = reader.ReadByte();
            value.AlarmPhotoVehicleCloseDistanceInterval = reader.ReadByte();
            value.RoadSignRecognitionPhotographs = reader.ReadByte();
            value.RoadSignRecognitionPhotographsInterval = reader.ReadByte();
            value.Placeholder2 = reader.ReadArray(4).ToArray();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0xF364 value, IJT808Config config)
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
            writer.WriteByte(value.Placeholder1);
            writer.WriteByte(value.DistanceThresholdObstacleAlarm);
            writer.WriteByte(value.HierarchicalSpeedThresholdObstacleAlarm);
            writer.WriteByte(value.VideoRecordingTimeBeforeAndAfterObstacleAlarm);
            writer.WriteByte(value.BarrierAlarmPhotographs);
            writer.WriteByte(value.ObstacleAlarmInterval);
            writer.WriteByte(value.FrequentChannelChangeAlarmJudgmentTimePeriod);
            writer.WriteByte(value.FrequentAlarmJudgmentNumberChannelChange);
            writer.WriteByte(value.HierarchicalSpeedThresholdFrequentChannelChangeAlarm);
            writer.WriteByte(value.VideoRecordingTimeBeforeAndAfterFrequentLaneChangeAlarm);
            writer.WriteByte(value.FrequentChannelChangeAlarmPhotos);
            writer.WriteByte(value.FrequentLaneChangeAlarmInterval);
            writer.WriteByte(value.GradedSpeedThresholdLaneDeviationAlarm);
            writer.WriteByte(value.VideoRecordingTimeBeforeAndAfterLaneDepartureAlarm);
            writer.WriteByte(value.LaneDepartureAlarmPhoto);
            writer.WriteByte(value.LaneDepartureAlarmPhotoInterval);
            writer.WriteByte(value.ForwardCollisionWarningTimeThreshold);
            writer.WriteByte(value.HierarchicalSpeedThresholdForwardCollisionWarning);
            writer.WriteByte(value.VideoRecordingTimeBeforeAndAfterForwardCollisionAlarm);
            writer.WriteByte(value.ForwardCollisionAlarmPhotographs);
            writer.WriteByte(value.ForwardCollisionAlarmInterval);
            writer.WriteByte(value.PedestrianCollisionAlarmTimeThreshold);
            writer.WriteByte(value.PedestrianCollisionAlarmEnablingSpeedThreshold);
            writer.WriteByte(value.VideoRecordingTimeBeforeAndAfterPedestrianCollisionAlarm);
            writer.WriteByte(value.PedestrianCollisionAlarmPhotos);
            writer.WriteByte(value.PedestrianCollisionAlarmInterval);
            writer.WriteByte(value.VehicleDistanceMonitoringAlarmDistanceThreshold);
            writer.WriteByte(value.VehicleDistanceMonitoringAndAlarmClassificationSpeedThreshold);
            writer.WriteByte(value.VideoRecordingTimeBeforeAndAfterAlarmVehicleProximity);
            writer.WriteByte(value.AlarmPhotoVehicleCloseDistance);
            writer.WriteByte(value.AlarmPhotoVehicleCloseDistanceInterval);
            writer.WriteByte(value.RoadSignRecognitionPhotographs);
            writer.WriteByte(value.RoadSignRecognitionPhotographsInterval);
            writer.WriteArray(value.Placeholder2);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - ParamLengthPosition - 1), ParamLengthPosition);
        }
    }
}
