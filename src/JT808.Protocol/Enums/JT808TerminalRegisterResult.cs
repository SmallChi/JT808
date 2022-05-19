namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 终端注册返回结果
    /// The terminal registration result is returned
    /// </summary>
    public enum JT808TerminalRegisterResult : byte
    {
        /// <summary>
        /// 成功
        /// success
        /// </summary>
        success = 0x00,
        /// <summary>
        /// 车辆已被注册
        /// The vehicle has been registered
        /// </summary>
        vehicle_registered = 0x01,
        /// <summary>
        /// 数据库中无该车辆
        /// The vehicle is not in the database
        /// </summary>
        vehicle_not_database = 0x02,
        /// <summary>
        /// 终端已被注册
        /// The terminal has been registered
        /// </summary>
        terminal_registered = 0x03,
        /// <summary>
        /// 数据库中无该终端
        /// The terminal is not in the database
        /// </summary>
        terminal_not_database = 0x04
    }
}
