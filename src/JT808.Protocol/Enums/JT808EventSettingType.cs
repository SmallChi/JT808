namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 事件设置类型
    /// Event setting Type
    /// </summary>
    public enum JT808EventSettingType : byte
    {
        /// <summary>
        /// 删除终端现有所有事件_该命令后不带后继字节
        /// Delete all existing events on the terminal This command does not contain subsequent bytes
        /// </summary>
        delete_terminal_all_existing_events = 0x00,
        /// <summary>
        /// 更新事件
        /// Update events
        /// </summary>
        update_events = 0x01,
        /// <summary>
        /// 追加事件
        /// Append events
        /// </summary>
        append_events = 0x02,
        /// <summary>
        /// 修改事件
        /// Modify event
        /// </summary>
        modify_events = 0x03,
        /// <summary>
        /// 删除特定几项事件，之后事件项中无需带事件内容
        /// Delete specific events
        /// </summary>
        delete_specific_events = 0x04
    }
}
