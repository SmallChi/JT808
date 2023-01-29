namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询终端参数
    /// </summary>
    public class JT808_0x8104 : JT808Bodies
    {
        /// <summary>
        /// 0x8104
        /// </summary>
        public ushort MsgId  => 0x8104;
        /// <summary>
        /// 查询终端参数
        /// </summary>
        public string Description => "查询终端参数";
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public bool SkipSerialization  => true;
    }
}
