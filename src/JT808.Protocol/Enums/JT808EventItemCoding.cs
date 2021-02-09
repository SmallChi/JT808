namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 事件项编码
    /// </summary>
    public enum JT808EventItemCoding : byte
    {
        /// <summary>
        /// 平台下发指令
        /// </summary>
        平台下发指令 = 0x00,
        /// <summary>
        /// 定时动作
        /// </summary>
        定时动作 = 0x01,
        /// <summary>
        /// 抢劫报警触发
        /// </summary>
        抢劫报警触发 = 0x02,
        /// <summary>
        /// 碰撞侧翻报警触发
        /// </summary>
        碰撞侧翻报警触发 = 0x03
    }
}
