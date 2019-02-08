namespace JT808.Protocol.JT808Properties
{
    /// <summary>
    /// 分包属性
    /// </summary>
    public class JT808SplitPackageProperty
    {
        public int PackgeIndex { get; set; }

        public int PackgeCount { get; set; }

        public byte[] Data { get; set; }
    }
}
