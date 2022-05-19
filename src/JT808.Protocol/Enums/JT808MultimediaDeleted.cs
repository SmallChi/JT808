namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 删除标志
    /// 单条存储多媒体数据检索上传命令
    /// Single storage multimedia data retrieval upload command
    /// </summary>
    public enum JT808MultimediaDeleted : byte
    {
        /// <summary>
        /// 保留
        /// reserve
        /// </summary>
        reserve = 0,
        /// <summary>
        /// 删除
        /// delete
        /// </summary>
        delete = 1
    }
}
