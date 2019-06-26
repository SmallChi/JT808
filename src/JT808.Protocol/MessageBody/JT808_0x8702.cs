namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 上报驾驶员身份信息请求
    /// </summary>
    public class JT808_0x8702 : JT808Bodies
    {
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public override bool SkipSerialization { get; set; } = true;
    }
}
