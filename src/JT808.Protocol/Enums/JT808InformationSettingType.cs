namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 信息设置类型
    /// Information setting type
    /// </summary>
    public enum JT808InformationSettingType : byte
    {
        /// <summary>
        /// 删除终端全部信息项
        /// Delete all terminal information items
        /// </summary>
        delete_all_items = 0x00,
        /// <summary>
        /// 更新菜单
        /// Update menu
        /// </summary>
        update_menu = 0x01,
        /// <summary>
        /// 追加菜单
        /// Append menu
        /// </summary>
        append_menu = 0x02,
        /// <summary>
        /// 修改菜单
        /// Modify the menu
        /// </summary>
        modify_menu = 0x03
    }
}
