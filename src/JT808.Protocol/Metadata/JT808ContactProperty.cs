using JT808.Protocol.Enums;

namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// 电话本联系人项数据
    /// </summary>
    public struct JT808ContactProperty
    {
        /// <summary>
        /// 标志 1：呼入；2：呼出；3：呼入/呼出
        /// </summary>
        public JT808TelephoneBookContactType TelephoneBookContactType { get; set; }
        /// <summary>
        /// 号码长度
        /// </summary>
        public byte PhoneNumberLength { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 联系人长度
        /// </summary>
        public byte ContactLength { get; set; }
        /// <summary>
        /// 联系人 经 GBK 编码
        /// </summary>
        public string Contact { get; set; }
    }
}
