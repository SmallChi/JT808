namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// 分包属性
    /// </summary>
    public struct JT808SplitPackageProperty
    {
        public int PackgeIndex { get; set; }

        public int PackgeCount { get; set; }

        public byte[] Data { get; set; }
    }
}
