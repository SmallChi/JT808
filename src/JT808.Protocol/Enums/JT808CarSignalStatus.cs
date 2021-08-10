using System;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 扩展车辆信号状态位
    /// </summary>
    [Flags]
    public enum JT808CarSignalStatus : uint
    {
        /// <summary>
        /// 开启近光信号灯
        /// </summary>
        近光灯信号 = 1,
        /// <summary>
        /// 远光灯信号
        /// </summary>
        远光灯信号 = 2,
        /// <summary>
        /// 右转向灯信号
        /// </summary>
        右转向灯信号 = 4,
        /// <summary>
        /// 左转向灯信号
        /// </summary>
        左转向灯信号 = 8,
        /// <summary>
        /// 制动信号
        /// </summary>
        制动信号 = 16,
        /// <summary>
        /// 倒档信号
        /// </summary>
        倒档信号 = 32,
        /// <summary>
        /// 雾灯信号
        /// </summary>
        雾灯信号 = 64,
        /// <summary>
        /// 示廓灯
        /// </summary>
        示廓灯 = 128,
        /// <summary>
        /// 喇叭信号
        /// </summary>
        喇叭信号 = 256,
        /// <summary>
        /// 空调状态
        /// </summary>
        空调状态 = 512,
        /// <summary>
        /// 空挡信号
        /// </summary>
        空挡信号 = 1024,
        /// <summary>
        /// 缓速器工作
        /// </summary>
        缓速器工作 = 2048,
        /// <summary>
        /// ABS工作
        /// </summary>
        ABS工作 = 4096,
        /// <summary>
        /// 加热器工作
        /// </summary>
        加热器工作 = 8192,
        /// <summary>
        /// 离合器状态
        /// </summary>
        离合器状态 = 16384,
    }
}
