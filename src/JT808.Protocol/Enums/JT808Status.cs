using System;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// JT808车辆状态位
    /// </summary>
    [Flags]
    public enum JT808Status : uint
    {
        /// <summary>
        /// ACC开
        /// </summary>
        ACC开 = 1,
        /// <summary>
        /// 定位
        /// </summary>
        定位 = 2,
        /// <summary>
        /// 南纬
        /// </summary>
        南纬 = 4,
        /// <summary>
        /// 西经
        /// </summary>
        西经 = 8,
        /// <summary>
        /// 停运状态
        /// </summary>
        停运状态 = 16,
        /// <summary>
        /// 经纬度已经保密插件加密
        /// </summary>
        经纬度已经保密插件加密 = 32,
        //保留 = 64,
        //保留 = 128,
        /// <summary>
        /// 半载
        /// </summary>
        半载 = 256,
        //保留 = 512,
        /// <summary>
        /// 满载
        /// </summary>
        满载 = 768,
        /// <summary>
        /// 车辆油路断开
        /// </summary>
        车辆油路断开 = 1024,
        /// <summary>
        /// 车辆电路断开
        /// </summary>
        车辆电路断开 = 2048,
        /// <summary>
        /// 车门加锁
        /// </summary>
        车门加锁 = 4096,
        /// <summary>
        /// 前门开
        /// </summary>
        前门开 = 8192,
        /// <summary>
        /// 中门开
        /// </summary>
        中门开 = 16384,
        /// <summary>
        /// 后门开
        /// </summary>
        后门开 = 32768,
        /// <summary>
        /// 驾驶席门开
        /// </summary>
        驾驶席门开 = 65536,
        /// <summary>
        /// 自定义
        /// </summary>
        自定义 = 131072,
        /// <summary>
        /// 使用GPS卫星进行定位
        /// </summary>
        使用GPS卫星进行定位 = 262144,
        /// <summary>
        /// 使用北斗卫星进行定位
        /// </summary>
        使用北斗卫星进行定位 = 524288,
        /// <summary>
        /// 使用GLONASS卫星进行定位
        /// </summary>
        使用GLONASS卫星进行定位 = 1048576,
        /// <summary>
        /// 使用Galileo卫星进行定位
        /// </summary>
        使用Galileo卫星进行定位 = 2097152
    }
}
