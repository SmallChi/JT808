namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 设置电话本设置类型
    /// Set the phone book setting type
    /// </summary>
    public enum JT808SettingTelephoneBook : byte
    {
        /// <summary>
        /// 删除终端上所有存储的联系人
        /// Delete all contacts stored on the terminal
        /// </summary>
        delete_all = 0,
        /// <summary>
        /// 更新电话本_删除终端中已有全部联系人并追加消息中的联系人
        /// Update the phone book _ Delete all contacts from the terminal and add contacts to the message 
        /// </summary>
        update_phone_book=1,
        /// <summary>
        /// 追加电话本
        /// Append phone book
        /// </summary>
        append_phone_book = 2,
        /// <summary>
        /// 修改电话本_以联系人为索引
        /// Modify the phone book to index contacts
        /// </summary>
        modify_phone_book = 3
    }
}
