namespace JT808.Protocol.Enums
{
    /// <summary>
    /// IC 卡读取结果
    /// </summary>
    public enum JT808ICCardReadResult : byte
    {
        IC卡读卡成功 = 0x00,
        读卡失败_原因为卡片密钥认证未通过 = 0x01,
        读卡失败_原因为卡片已被锁定 = 0x02,
        读卡失败_原因为卡片被拔出 = 0x03,
        读卡失败_原因为数据校验错误 = 0x04,
    }
}
