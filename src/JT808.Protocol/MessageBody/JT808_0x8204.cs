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
        public override ushort MsgId { get; } = 0x8204;

        public override bool SkipSerialization { get; set; } = true;

        public override string Description => "链路检测";
    }
}
