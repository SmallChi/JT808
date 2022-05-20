using System;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// JT808车辆状态位
    /// Vehicle status position
    /// </summary>
    [Flags]
    public enum JT808Status : uint
    {
        /// <summary>
        /// ACC开
        /// The ACC open  
        /// </summary>
        acc_open = 1,
        /// <summary>
        /// 定位
        /// location
        /// </summary>
        location = 2,
        /// <summary>
        /// 南纬
        /// latitude
        /// </summary>
        latitude = 4,
        /// <summary>
        /// 西经
        /// longitude
        /// </summary>
        longitude = 8,
        /// <summary>
        /// 停运状态
        /// Shut down the state
        /// </summary>
        shutdown_state = 16,
        /// <summary>
        /// 经纬度已经保密插件加密
        /// Latitude and longitude are encrypted by secret plug-in
        /// </summary>
        lat_lng_encrypt = 32,
        //保留 = 64,
        //保留 = 128,
        /// <summary>
        /// 半载
        /// half_load
        /// </summary>
        half_load = 256,
        //保留 = 512,
        /// <summary>
        /// 满载
        /// full load
        /// </summary>
        full_load = 768,
        /// <summary>
        /// 车辆油路断开
        /// The fuel line of the vehicle is disconnected
        /// </summary>
        vehicle_fuel_line_disconnected= 1024,
        /// <summary>
        /// 车辆电路断开
        /// Vehicle circuit disconnection
        /// </summary>
        vehicle_circuit_disconnection = 2048,
        /// <summary>
        /// 车门加锁
        /// The door lock
        /// </summary>
        door_lock = 4096,
        /// <summary>
        /// 前门开
        /// The front door open
        /// </summary>
        front_door_open = 8192,
        /// <summary>
        /// 中门开
        /// The door opened
        /// </summary>
        door_opened = 16384,
        /// <summary>
        /// 后门开
        /// The back door open
        /// </summary>
        back_door_open = 32768,
        /// <summary>
        /// 驾驶席门开
        /// The driver's seat door is open
        /// </summary>
        drivers_seat_door_open = 65536,
        /// <summary>
        /// 自定义
        /// custom
        /// </summary>
        custom = 131072,
        /// <summary>
        /// 使用GPS卫星进行定位
        /// GPS satellites are used for positioning
        /// </summary>
        used_gps = 262144,
        /// <summary>
        /// 使用北斗卫星进行定位
        /// Beidou satellites were used for positioning
        /// </summary>
        used_beidou = 524288,
        /// <summary>
        /// 使用GLONASS卫星进行定位
        /// GLONASS satellite was used for positioning  
        /// </summary>
        used_glonass = 1048576,
        /// <summary>
        /// 使用Galileo卫星进行定位+
        /// Galileo satellite is used for positioning  
        /// </summary>
        used_galileo = 2097152
    }
}
