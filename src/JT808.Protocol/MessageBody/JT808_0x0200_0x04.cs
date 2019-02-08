using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    [JT808Formatter(typeof(JT808_0x0200_0x04Formatter))]
    public class JT808_0x0200_0x04 : JT808_0x0200_BodyBase
    {
        /// <summary>
        /// 需要人工确认报警事件的 ID，从 1 开始计数
        /// </summary>
        public ushort EventId { get; set; }
        public override byte AttachInfoId { get; set; } = 0x04;
        public override byte AttachInfoLength { get; set; } = 2;
    }
}
