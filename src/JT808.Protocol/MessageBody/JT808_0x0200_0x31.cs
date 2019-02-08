using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    [JT808Formatter(typeof(JT808_0x0200_0x31Formatter))]
    public class JT808_0x0200_0x31 : JT808_0x0200_BodyBase
    {
        /// <summary>
        /// GNSS 定位卫星数
        /// </summary>
        public byte GNSSCount { get; set; }
        public override byte AttachInfoId { get; set; } = 0x31;
        public override byte AttachInfoLength { get; set; } = 1;
    }
}
