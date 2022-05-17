namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 事件项编码
    /// Event item coding
    /// </summary>
    public enum JT808EventItemCoding : byte
    {
        /// <summary>
        /// 平台下发指令
        /// Platform delivery order
        /// </summary>
        platform_delivery_order = 0x00,
        /// <summary>
        /// 定时动作
        /// Regular action
        /// </summary>
        regular_action = 0x01,
        /// <summary>
        /// 抢劫报警触发
        /// Robbery alarm trigger
        /// </summary>
        robbery_alarm_trigger = 0x02,
        /// <summary>
        /// 碰撞侧翻报警触发
        /// Collision rollover alarm triggered
        /// </summary>
        collision_rollover_alarm_triggered = 0x03
    }
}
