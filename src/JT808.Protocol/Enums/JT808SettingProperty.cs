namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 设置属性
    /// set attributes
    /// </summary>
    public enum JT808SettingProperty : byte
    {
        /// <summary>
        /// 更新区域
        /// update region
        /// </summary>
        update_region = 0x00,
        /// <summary>
        /// 追加区域
        /// append region
        /// </summary>
        append_region = 0x01,
        /// <summary>
        /// 修改区域
        /// modify region
        /// </summary>
        modify_region = 0x02
    }
}
