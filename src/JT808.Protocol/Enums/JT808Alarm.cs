using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 报警标志
    /// Alarm Flag
    /// </summary>
    [Flags]
    public enum JT808Alarm : uint
    {
        /// <summary>
        /// 紧急报警_触动报警开关后触发
        /// The emergency alarm is triggered after the alarm switch is touched
        /// </summary>
        emergency_alarm = 1,
        /// <summary>
        /// 超速报警 标志维持至报警条件解除
        /// Overspeed alarm
        /// </summary>
        overspeed_alarm = 2,
        /// <summary> 
        /// 疲劳驾驶 标志维持至报警条件解除
        /// fatigue driving
        /// </summary>
        fatigue_driving = 4,
        /// <summary>
        /// 危险预警 标志维持至报警条件解除
        /// danger warning
        /// </summary>
        danger_warning = 8,
        /// <summary>
        /// GNSS模块发生故障 标志维持至报警条件解除
        /// The GNSS module is faulty
        /// </summary>
        gnss_module_fault = 16,
        /// <summary>
        /// GNSS天线未接或被剪断 标志维持至报警条件解除
        /// The GNSS antenna is not connected or cut off
        /// </summary>
        gnss_ant_not_connected = 32,
        /// <summary>
        /// GNSS天线短路 标志维持至报警条件解除
        /// GNSS antenna short-circuited  
        /// </summary>
        gnss_ant_short = 64,
        /// <summary>
        /// 终端主电源欠压 标志维持至报警条件解除
        /// The main power supply of the terminal is undervoltage
        /// </summary>
        terminal_main_power_undervoltage = 128,
        /// <summary>
        /// 终端主电源掉电 标志维持至报警条件解除
        /// The main power supply of the terminal fails
        /// </summary>
        terminal_main_power_down = 256,
        /// <summary>
        /// 终端LCD或显示器故障 标志维持至报警条件解除
        /// The LCD or monitor of the terminal is faulty
        /// </summary>
        terminal_display_fault = 512,
        /// <summary>
        /// TTS模块故障 标志维持至报警条件解除
        /// The TTS module is faulty  
        /// </summary>
        tts_module_fault = 1024,
        /// <summary>
        /// 摄像头故障 标志维持至报警条件解除
        /// Camera fault
        /// </summary>
        camera_fault = 2048,
        /// <summary>
        /// 道路运输证IC卡模块故障 标志维持至报警条件解除
        /// The IC card module of the road transport certificate is faulty
        /// </summary>
        road_transport_cert_ic_card_module_fault = 4096,
        /// <summary>
        /// 超速预警 标志维持至报警条件解除
        /// Overspeed warning
        /// </summary>
        overspeed_warning = 8192,
        /// <summary>
        /// 疲劳驾驶预警 标志维持至报警条件解除
        /// Fatigue driving warning
        /// </summary>
        fatigue_driving_warning = 16384,
        /// <summary>
        /// 保留1
        /// reserve1
        /// </summary>
        reserve1 = 32768,
        /// <summary>
        /// 保留2
        /// reserve2
        /// </summary>
        reserve2 = 65536,
        /// <summary>
        /// 保留3
        /// reserve3
        /// </summary>
        reserve3 = 131072,
        /// <summary>
        /// 当天累计驾驶超时 标志维持至报警条件解除
        /// Accumulated driving overtime that day
        /// </summary>
        day_accumulated_driving_timeout = 262144,
        /// <summary>
        /// 超时停车 标志维持至报警条件解除
        /// Timeout parking
        /// </summary>
        timeout_parking = 524288,
        /// <summary>
        /// 进出区域 收到应答后清零
        /// In and out of the area
        /// </summary>
        in_area = 1048576,
        /// <summary>
        /// 进出路线 收到应答后清零
        /// </summary>
        in_route = 2097152,
        /// <summary>
        /// 路段行驶时间不足或过长 收到应答后清零
        /// Road section driving time is insufficient or too long
        /// </summary>
        road_driving_time_insufficient = 4194304,
        /// <summary>
        /// 路线偏离报警 标志维持至报警条件解除
        /// Route deviation alarm
        /// </summary>
        route_deviation_alarm = 8388608,
        /// <summary>
        /// 车辆VSS故障 标志维持至报警条件解除
        /// VSS of the vehicle is faulty
        /// </summary>
        vehicle_vss_fault = 16777216,
        /// <summary>
        /// 车辆油量异常 标志维持至报警条件解除
        /// Abnormal vehicle fuel level
        /// </summary>
        vehicle_fuel_abnormal = 33554432,
        /// <summary>
        /// 车辆被盗通过车辆防盗器 标志维持至报警条件解除
        /// The vehicle is stolen
        /// </summary>
        vehicle_stolen = 67108864,
        /// <summary>
        /// 车辆非法点火
        /// Illegal ignition of vehicles
        /// </summary>
        vehicle_illegal_ignition = 134217728,
        /// <summary>
        /// 车辆非法位移 收到应答后清零
        /// Illegal displacement of vehicle
        /// </summary>
        vehicle_illegal_displacement = 268435456,
        /// <summary>
        /// 碰撞预警 标志维持至报警条件解除
        /// collision Warning
        /// </summary>
        collision_warning = 536870912,
        /// <summary>
        /// 侧翻预警 标志维持至报警条件解除
        /// rollover warning
        /// </summary>
        rollover_warning = 1073741824,
        /// <summary>
        /// 非法开门报警（终端未设置区域时，不判断非法开门） 收到应答后清零
        /// Illegal door opening alarm
        /// </summary>
        illegal_opening_door_alarm = 2147483648
    }
}
