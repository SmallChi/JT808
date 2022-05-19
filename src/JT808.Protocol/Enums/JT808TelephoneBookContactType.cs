namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 电话本联系人标志
    /// Phone book contact mark
    /// </summary>
    public enum JT808TelephoneBookContactType : byte
    {
        /// <summary>
        /// 呼入
        /// call_in
        /// </summary>
        callin = 1,
        /// <summary>
        /// 呼出
        /// call_out
        /// </summary>
        callout = 2,
        /// <summary>
        /// 呼入_呼出
        /// call_in|call_out
        /// </summary>
        call_in_out = 3
    }
}
