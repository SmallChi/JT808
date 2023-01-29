namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端心跳
    /// </summary>
    public class JT808_0x0002 : JT808Bodies
    {
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public bool SkipSerialization => true;
        /// <summary>
        /// 0x0002
        /// </summary>
        public ushort MsgId  => 0x0002;
        /// <summary>
        /// 终端心跳
        /// </summary>
        public string Description => "终端心跳";
    }
}
