namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端注销请求
    /// </summary>
    public class JT808_0x0003 : JT808Bodies
    {
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public override bool SkipSerialization { get; set; } = true;
    }
}
