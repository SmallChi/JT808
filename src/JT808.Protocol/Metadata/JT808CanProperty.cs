namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// Can属性
    /// </summary>
    public struct JT808CanProperty
    {
        /// <summary>
        /// CAN ID
        /// 4
        /// </summary>
        public byte[] CanId { get; set; }
        /// <summary>
        /// CAN 数据
        /// 8
        /// </summary>
        public byte[] CanData { get; set; }
    }
}
