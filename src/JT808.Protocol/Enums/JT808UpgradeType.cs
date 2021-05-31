namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 升级类型
    /// </summary>
    public enum JT808UpgradeType : byte
    {
        /// <summary>
        /// 终端
        /// </summary>
        终端 = 0,
        /// <summary>
        /// 道路运输证IC卡读卡器
        /// </summary>
        道路运输证IC卡读卡器 = 0x0C,
        /// <summary>
        /// 北斗卫星定位模块
        /// </summary>
        北斗卫星定位模块 = 0x34,
        /// <summary>
        /// 粤标主动安全-高级驾驶辅助系统
        /// </summary>
        高级驾驶辅助系统 = 0x64,
        /// <summary>
        /// 粤标主动安全-驾驶状态监控系统
        /// </summary>
        驾驶状态监控系统 = 0x65,
        /// <summary>
        /// 粤标主动安全-胎压监测系统
        /// </summary>
        胎压监测系统 = 0x66,
        /// <summary>
        /// 粤标主动安全-盲点监测系统
        /// </summary>
        盲点监测系统 = 0x67,
    }
}
