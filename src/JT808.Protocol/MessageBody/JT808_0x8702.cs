namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 上报驾驶员身份信息请求
    /// </summary>
    public class JT808_0x8702 : JT808Bodies
    {
        /// <summary>
        /// 0x8702
        /// </summary>
        public ushort MsgId { get; } = 0x8702;
        /// <summary>
        /// 上报驾驶员身份信息请求
        /// </summary>
        public string Description => "上报驾驶员身份信息请求";
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public bool SkipSerialization  => true;
    }
}
