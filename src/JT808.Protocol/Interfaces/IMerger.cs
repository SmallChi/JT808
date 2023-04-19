namespace JT808.Protocol.Interfaces
{
    /// <summary>
    /// 合并分包数据接口
    /// </summary>
    public interface IMerger
    {
        /// <summary>
        /// 合并元数据并反序列化数据包
        /// </summary>
        /// <param name="header">消息头</param>
        /// <param name="data">分包数据包中的消息体部分元数据<see cref="JT808Package.SubDataBodies"/></param>
        /// <param name="config">配置项</param>
        /// <param name="body">反序列化得出的数据包</param>
        /// <returns>是否反序列化成功</returns>
        bool TryMerge(JT808Header header, byte[] data, IJT808Config config, out JT808Bodies body);
    }
}