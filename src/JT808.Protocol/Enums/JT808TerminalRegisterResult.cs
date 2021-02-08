namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 终端注册返回结果
    /// </summary>
    public enum JT808TerminalRegisterResult : byte
    {
        /// <summary>
        /// 成功
        /// </summary>
        成功 = 0x00,
        /// <summary>
        /// 车辆已被注册
        /// </summary>
        车辆已被注册 = 0x01,
        /// <summary>
        /// 数据库中无该车辆
        /// </summary>
        数据库中无该车辆 = 0x02,
        /// <summary>
        /// 终端已被注册
        /// </summary>
        终端已被注册 = 0x03,
        /// <summary>
        /// 数据库中无该终端
        /// </summary>
        数据库中无该终端 = 0x04
    }
}
