namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 事件项编码
    /// </summary>
    public enum JT808EventItemCoding : byte
    {
        平台下发指令 = 0x00,
        定时动作 = 0x01,
        抢劫报警触发 = 0x02,
        碰撞侧翻报警触发 = 0x03
    }
}
