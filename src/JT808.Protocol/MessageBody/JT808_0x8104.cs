namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询终端参数
    /// </summary>
    public class JT808_0x8104 : JT808Bodies
    {
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public override bool SkipSerialization { get; set; } = true;
    }
}
