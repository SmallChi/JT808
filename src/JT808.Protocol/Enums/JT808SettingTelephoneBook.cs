namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 设置电话本 设置类型
    /// </summary>
    public enum JT808SettingTelephoneBook : byte
    {
        /// <summary>
        /// 删除终端上所有存储的联系人
        /// </summary>
        删除终端上所有存储的联系人 = 0,
        /// <summary>
        /// 更新电话本_删除终端中已有全部联系人并追加消息中的联系人
        /// </summary>
        更新电话本_删除终端中已有全部联系人并追加消息中的联系人 = 1,
        /// <summary>
        /// 追加电话本
        /// </summary>
        追加电话本 = 2,
        /// <summary>
        /// 修改电话本_以联系人为索引
        /// </summary>
        修改电话本_以联系人为索引 = 3
    }
}
