using JT808.Protocol.Interfaces;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询服务器时间请求
    /// 2019版本
    /// </summary>
    public class JT808_0x0004 : JT808Bodies, IJT808_2019_Version
    {
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public bool SkipSerialization => true;
        /// <summary>
        /// 0x0004
        /// </summary>
        public ushort MsgId => 0x0004;
        /// <summary>
        /// 查询服务器时间请求
        /// </summary>
        public string Description => "查询服务器时间请求";
    }
}
