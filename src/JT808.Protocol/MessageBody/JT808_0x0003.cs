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
        public bool SkipSerialization  => true;
        /// <summary>
        /// 0x0003
        /// </summary>
        public ushort MsgId =>0x0003;
        /// <summary>
        /// 终端注销
        /// </summary>
        public string Description => "终端注销";
    }
}
