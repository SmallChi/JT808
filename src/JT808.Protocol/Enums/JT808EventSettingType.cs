namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 事件设置类型
    /// </summary>
    public enum JT808EventSettingType : byte
    {
        /// <summary>
        /// 删除终端现有所有事件_该命令后不带后继字节
        /// </summary>
        删除终端现有所有事件_该命令后不带后继字节 = 0x00,
        /// <summary>
        /// 更新事件
        /// </summary>
        更新事件 = 0x01,
        /// <summary>
        /// 追加事件
        /// </summary>
        追加事件 = 0x02,
        /// <summary>
        /// 修改事件
        /// </summary>
        修改事件 = 0x03,
        /// <summary>
        /// 删除特定几项事件，之后事件项中无需带事件内容
        /// </summary>
        删除特定几项事件_之后事件项中无需带事件内容 = 0x04
    }
}
