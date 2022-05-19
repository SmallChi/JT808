namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 升级结果
    /// Upgrade Result
    /// </summary>
    public enum JT808UpgradeResult : byte
    {
        /// <summary>
        /// 成功
        /// success
        /// </summary>
        success = 0x00,
        /// <summary>
        /// 失败
        /// fail
        /// </summary>
        fail = 0x01,
        /// <summary>
        /// 取消
        /// cancel
        /// </summary>
        cancel = 0x02,
        /// <summary>
        /// 粤标主动安全-未找到目标设备
        /// Yue Biao active Safety. - Target device not found
        /// </summary>
        yue_biao_active_safety_target_device_not_found = 0x10,
        /// <summary>
        /// 粤标主动安全-硬件型号不支持
        /// Yue Biao Active Safety - hardware models not supported
        /// </summary>
        yue_biao_active_safety_hardware_not_supported = 0x11,
        /// <summary>
        /// 粤标主动安全-软件版本相同
        /// Yue Biao Active Safety - Same Software Version
        /// </summary>
        yue_biao_active_safety_same_software_version = 0x12,
        /// <summary>
        /// 粤标主动安全-软件版本不支持
        /// Yue Biao Active Safety - The software version is not supported
        /// </summary>
        yue_biao_active_safety_same_software_version_not_supported = 0x13
    }
}
