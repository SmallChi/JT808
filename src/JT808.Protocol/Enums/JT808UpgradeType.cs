namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 升级类型
    /// Upgrade Type
    /// </summary>
    public enum JT808UpgradeType : byte
    {
        /// <summary>
        /// 终端
        /// terminal
        /// </summary>
        terminal = 0,
        /// <summary>
        /// 道路运输证IC卡读卡器
        /// Road transport certificate IC card reader
        /// </summary>
        road_transport_cert_ic_card_reader = 0x0C,
        /// <summary>
        /// 北斗卫星定位模块
        /// Beidou satellite positioning module
        /// </summary>
        beidou_module = 0x34,
        /// <summary>
        /// 粤标主动安全-高级驾驶辅助系统
        /// Advanced driver assistance system
        /// </summary>
        adas = 0x64,
        /// <summary>
        /// 粤标主动安全-驾驶状态监控系统
        /// Driving condition monitoring system
        /// </summary>
        dsm = 0x65,
        /// <summary>
        /// 粤标主动安全-胎压监测系统
        /// tire pressure monitoring system
        /// </summary>
        tpms = 0x66,
        /// <summary>
        /// 粤标主动安全-盲点监测系统
        /// Blind Spot Monitoring system
        /// </summary>
        bsd = 0x67,
    }
}
