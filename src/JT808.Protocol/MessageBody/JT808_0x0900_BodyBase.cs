namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据上行透传
    /// </summary>
    public interface JT808_0x0900_BodyBase
    {
        /// <summary>
        /// 透传消息类型
        /// </summary>
         byte PassthroughType { get; set; }
    }
}
