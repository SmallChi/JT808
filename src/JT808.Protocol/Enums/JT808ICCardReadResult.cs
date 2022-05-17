namespace JT808.Protocol.Enums
{
    /// <summary>
    /// IC 卡读取结果
    /// IC card read result
    /// </summary>
    public enum JT808ICCardReadResult : byte
    {
        /// <summary>
        /// IC卡读卡成功
        /// IC card Reading succeeded.
        /// </summary>
        ic_card_reading_succeeded = 0x00,
        /// <summary>
        /// 读卡失败_原因为卡片密钥认证未通过
        /// Read the card failure:The cause is that the card key authentication fails
        /// </summary>
        read_card_failure_auth = 0x01,
        /// <summary>
        /// 读卡失败_原因为卡片已被锁定
        /// Read the card failure:The reason is that the card is locked
        /// </summary>
        read_card_failure_locked = 0x02,
        /// <summary>
        /// 读卡失败_原因为卡片被拔出
        /// Read the card failure:The cause is that the card is removed
        /// </summary>
        read_card_failure_removed = 0x03,
        /// <summary>
        /// 读卡失败_原因为数据校验错误
        ///  Read the card failure:The cause is a data verification error
        /// </summary>
        read_card_failure_data_verify_error = 0x04,
    }
}
