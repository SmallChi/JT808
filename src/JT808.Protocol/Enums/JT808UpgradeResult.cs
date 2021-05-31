namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 升级结果
    /// </summary>
    public enum JT808UpgradeResult : byte
    {
        /// <summary>
        /// 成功
        /// </summary>
        成功 = 0x00,
        /// <summary>
        /// 失败
        /// </summary>
        失败 = 0x01,
        /// <summary>
        /// 取消
        /// </summary>
        取消 = 0x02,
        /// <summary>
        /// 粤标主动安全-未找到目标设备
        /// </summary>
        未找到目标设备 = 0x10,
        /// <summary>
        /// 粤标主动安全-硬件型号不支持
        /// </summary>
        硬件型号不支持 = 0x11,
        /// <summary>
        /// 粤标主动安全-软件版本相同
        /// </summary>
        软件版本相同 = 0x12,
        /// <summary>
        /// 粤标主动安全-软件版本不支持
        /// </summary>
        软件版本不支持 = 0x13
    }
}
