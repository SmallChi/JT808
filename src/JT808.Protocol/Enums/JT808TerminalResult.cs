namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 通用应答返回
    /// Universal reply return
    /// </summary>
    public enum JT808TerminalResult : byte
    {
        /// <summary>
        /// 成功/确认
        /// </summary>
        Success = 0x00,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 0x01,
        /// <summary>
        /// 消息有误
        /// </summary>
        MessageError = 0x02,
        /// <summary>
        /// 不支持
        /// </summary>
        NotSupport = 0x03
    }
}
