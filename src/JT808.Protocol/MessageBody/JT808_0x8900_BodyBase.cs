namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据下行透传
    /// </summary>
    public interface JT808_0x8900_BodyBase
    {
        /// <summary>
        /// 透传消息类型
        /// 透传消息类型定义见 表 93
        /// </summary>
        byte PassthroughType { get; set; }
    }
}
