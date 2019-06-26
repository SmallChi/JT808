namespace JT808.Protocol.Enums
{
    public enum JT808PlatformResult : byte
    {
        /// <summary>
        /// 成功/确认
        /// </summary>
        成功 = 0x00,
        /// <summary>
        /// 失败
        /// </summary>
        失败 = 0x01,
        /// <summary>
        /// 消息有误
        /// </summary>
        消息有误 = 0x02,
        /// <summary>
        /// 不支持
        /// </summary>
        不支持 = 0x03,
        /// <summary>
        /// 报警处理确认
        /// </summary>
        报警处理确认 = 0x04
    }
}
