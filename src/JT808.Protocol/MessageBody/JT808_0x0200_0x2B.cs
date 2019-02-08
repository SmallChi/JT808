using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;


namespace JT808.Protocol.MessageBody
{
    [JT808Formatter(typeof(JT808_0x0200_0x2BFormatter))]
    public class JT808_0x0200_0x2B : JT808_0x0200_BodyBase
    {
        /// <summary>
        /// 模拟量 bit0-15，AD0；bit16-31，AD1
        /// </summary>
        public int Analog { get; set; }
        public override byte AttachInfoId { get; set; } = 0x2B;
        public override byte AttachInfoLength { get; set; } = 4;
    }
}
