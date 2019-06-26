namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// 事件属性
    /// </summary>
    public struct JT808EventProperty
    {
        /// <summary>
        /// 事件 ID 
        /// 若终端已有同 ID 的事件，则被覆盖
        /// </summary>
        public byte EventId { get; set; }
        /// <summary>
        /// 事件内容长度
        /// 后继事件内容字段字节长度
        /// </summary>
        public byte EventContentLength { get; set; }
        /// <summary>
        /// 事件内容
        /// 事件内容，经 GBK 编码
        /// </summary>
        public string EventContent { get; set; }
    }
}
