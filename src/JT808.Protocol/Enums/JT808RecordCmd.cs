namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 录音命令
    /// </summary>
    public enum JT808RecordCmd : byte
    {
        /// <summary>
        /// 停止录音
        /// </summary>
        停止录音 = 0x00,
        /// <summary>
        /// 停止录音
        /// </summary>
        开始录音 = 0x01
    }
}
