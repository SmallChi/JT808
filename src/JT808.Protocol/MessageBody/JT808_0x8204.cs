using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 链路检测
    /// 2019版本
    /// </summary>
    public class JT808_0x8204 : JT808Bodies, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8204
        /// </summary>
        public ushort MsgId => 0x8204;
        /// <summary>
        /// 跳过序列化器
        /// </summary>
        public bool SkipSerialization => true;
        /// <summary>
        /// 链路检测
        /// </summary>
        public  string Description => "链路检测";
    }
}
