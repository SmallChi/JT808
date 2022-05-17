using System;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 扩展车辆信号状态位
    /// Extended vehicle signal status bits
    /// </summary>
    [Flags]
    public enum JT808CarSignalStatus : uint
    {
        /// <summary>
        /// 开启近光信号灯
        /// Close light signal
        /// </summary>
        close_light_signal = 1,
        /// <summary>
        /// 远光灯信号
        /// High beam signal
        /// </summary>
        high_beam_signal = 2,
        /// <summary>
        /// 右转向灯信号
        /// Right turn signal
        /// </summary>
        right_turn_signal = 4,
        /// <summary>
        /// 左转向灯信号
        /// Left turn signal
        /// </summary>
        left_turn_signal = 8,
        /// <summary>
        /// 制动信号
        /// brake signal
        /// </summary>
        brake_signal = 16,
        /// <summary>
        /// 倒档信号
        /// Reverse signals
        /// </summary>
        reverse_signals = 32,
        /// <summary>
        /// 雾灯信号
        /// The fog light signal
        /// </summary>
        fog_light_signal = 64,
        /// <summary>
        /// 示廓灯
        /// Clearance Lamp
        /// </summary>
        clearance_lamp = 128,
        /// <summary>
        /// 喇叭信号
        /// horn signal
        /// </summary>
        horn_signal = 256,
        /// <summary>
        /// 空调状态
        /// Air condition status
        /// </summary>
        air_condition_status = 512,
        /// <summary>
        /// 空挡信号
        /// Neutral signal
        /// </summary>
        neutral_signal = 1024,
        /// <summary>
        /// 缓速器工作
        /// Retarder working
        /// </summary>
        retarder_working = 2048,
        /// <summary>
        /// ABS工作
        /// abs_working
        /// </summary>
        abs_working = 4096,
        /// <summary>
        /// 加热器工作
        /// Heater operation
        /// </summary>
        heater_operation = 8192,
        /// <summary>
        /// 离合器状态
        /// Clutch condition
        /// </summary>
        clutch_condition = 16384,
    }
}
