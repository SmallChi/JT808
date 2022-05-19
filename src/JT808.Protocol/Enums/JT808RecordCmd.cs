namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 录音命令
    /// The recording command
    /// </summary>
    public enum JT808RecordCmd : byte
    {
        /// <summary>
        /// 停止录音
        /// stop
        /// </summary>
        stop = 0x00,
        /// <summary>
        /// 开始录音
        /// start
        /// </summary>
        start = 0x01
    }
}
