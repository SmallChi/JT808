using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;


namespace JT808.Protocol.MessageBody
{
    [JT808Formatter(typeof(JT808_0x0200_0x30_Formatter))]
    public class JT808_0x0200_0x30 : JT808_0x0200_BodyBase
    {
        /// <summary>
        /// 无线通信网络信号强度
        /// </summary>
        public byte WiFiSignalStrength { get; set; }
        public override byte AttachInfoId { get; set; } = 0x30;
        public override byte AttachInfoLength { get; set; } = 1;
    }
}
