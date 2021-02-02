namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// 分包属性
    /// </summary>
    public struct JT808SplitPackageProperty
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PackgeIndex { get; set; }
        /// <summary>
        /// 分页总数
        /// </summary>
        public int PackgeCount { get; set; }
        /// <summary>
        /// 分包数据
        /// </summary>
        public byte[] Data { get; set; }
    }
}
