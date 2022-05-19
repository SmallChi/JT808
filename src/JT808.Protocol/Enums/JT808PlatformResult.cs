namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 返回结果
    /// return to the result
    /// </summary>
    public enum JT808PlatformResult : byte
    {
        /// <summary>
        /// 成功/确认
        /// succeed
        /// </summary>
        succeed = 0x00,
        /// <summary>
        /// 失败
        /// fail
        /// </summary>
        fail = 0x01,
        /// <summary>
        /// 消息有误
        /// The message is wrong
        /// </summary>
        message_wrong = 0x02,
        /// <summary>
        /// 不支持
        /// nonsupport
        /// </summary>
        nonsupport = 0x03,
        /// <summary>
        /// 报警处理确认
        /// Alarm processing confirmation
        /// </summary>
        alarm_processing_confirmation = 0x04
    }
}
