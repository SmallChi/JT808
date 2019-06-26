namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// 信息点播属性
    /// </summary>
    public struct JT808InformationItemProperty
    {
        /// <summary>
        /// 信息类型
        /// 若终端已有同类型的信息项，则被覆盖
        /// </summary>
        public byte InformationType { get; set; }
        /// <summary>
        /// 信息名称长度
        /// 信息名称字段字节长度
        /// </summary>
        public ushort InformationLength { get; set; }
        /// <summary>
        /// 信息名称
        /// 经 GBK 编码处理
        /// </summary>
        public string InformationName { get; set; }
    }
}
