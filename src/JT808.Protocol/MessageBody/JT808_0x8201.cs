namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息查询
    /// </summary>
    public class JT808_0x8201: JT808Bodies
    {
        /// <summary>
        /// 0x8201
        /// </summary>
        public override ushort MsgId { get; } = 0x8201;
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public override bool SkipSerialization { get; set; } = true;
        /// <summary>
        /// 位置信息查询
        /// </summary>
        public override string Description => "位置信息查询";
    }
}
