namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 信息设置类型
    /// </summary>
    public enum JT808InformationSettingType : byte
    {
        /// <summary>
        /// 删除终端全部信息项
        /// </summary>
        删除终端全部信息项 = 0x00,
        /// <summary>
        /// 更新菜单
        /// </summary>
        更新菜单 = 0x01,
        /// <summary>
        /// 追加菜单
        /// </summary>
        追加菜单 = 0x02,
        /// <summary>
        /// 修改菜单
        /// </summary>
        修改菜单 = 0x03
    }
}
