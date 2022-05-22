using System;

namespace JT808.Protocol.Extensions.JT1078.Enums
{
    /// <summary>
    /// 视频相关报警
    /// Video related alarm
    /// </summary>
    [Flags]
    public enum VideoRelateAlarmType:uint
    {
        /// <summary>
        /// 视频信号丢失报警
        /// Video signal loss alarm
        /// </summary>
        video_signal_loss_alarm = 0,
        /// <summary>
        /// 视频信号遮挡报警
        /// Video signal occlusion alarm
        /// </summary>
        video_signal_occlusion_alarm = 2,
        /// <summary>
        /// 存储单元故障报警
        /// The storage unit is faulty
        /// </summary>
        storage_unit_faulty = 4,
        /// <summary>
        /// 其他视频设备故障报警
        /// Other video equipment fault alarm
        /// </summary>
        other_video_equipment_fault_alarm = 8,
        /// <summary>
        /// 客车超员报警
        /// Bus overcrowding alarm
        /// </summary>
        bus_overcrowding_alarm = 16,
        /// <summary>
        /// 异常驾驶行为报警
        /// Abnormal driving behavior alarm
        /// </summary>
        abnormal_driving_behavior_alarm = 32,
        /// <summary>
        /// 特殊报警录像达到存储阈值报警
        /// Special alarm video reaches storage threshold alarm
        /// </summary>
        special_alarm_video_reaches_storage_threshold_alarm = 64,
    }
}
