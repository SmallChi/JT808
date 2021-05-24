using System;

namespace JT808.Protocol.Extensions.JT1078.Enums
{
    /// <summary>
    /// 视频相关报警
    /// </summary>
    [Flags]
    public enum VideoRelateAlarmType:uint
    {
        /// <summary>
        /// 视频信号丢失报警
        /// </summary>
        视频信号丢失报警 = 0,
        /// <summary>
        /// 视频信号遮挡报警
        /// </summary>
        视频信号遮挡报警 = 2,
        /// <summary>
        /// 存储单元故障报警
        /// </summary>
        存储单元故障报警 = 4,
        /// <summary>
        /// 其他视频设备故障报警
        /// </summary>
        其他视频设备故障报警 = 8,
        /// <summary>
        /// 客车超员报警
        /// </summary>
        客车超员报警 = 16,
        /// <summary>
        /// 异常驾驶行为报警
        /// </summary>
        异常驾驶行为报警 = 32,
        /// <summary>
        /// 特殊报警录像达到存储阈值报警
        /// </summary>
        特殊报警录像达到存储阈值报警 = 64,
    }
}
