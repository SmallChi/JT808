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
        public override ushort MsgId { get; } = 0x8204;
        /// <summary>
        /// 跳过序列化器
        /// </summary>
        public override bool SkipSerialization { get; set; } = true;
        /// <summary>
        /// 链路检测
        /// </summary>
        public override string Description => "链路检测";
    }
}
