using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using System.Runtime.Serialization;

namespace JT808.Protocol.MessageBody
{
    [JT808Formatter(typeof(JT808_0x0200_0x02_Formatter))]
    public class JT808_0x0200_0x02 : JT808_0x0200_BodyBase
    {
        /// <summary>
        /// 油量
        /// </summary>
        public ushort Oil { get; set; }
        /// <summary>
        /// 油量 1/10L，对应车上油量表读数
        /// </summary>
        [IgnoreDataMember]
        public double ConvertOil => Oil / 10.0;
        public override byte AttachInfoId { get; set; } = 0x02;
        public override byte AttachInfoLength { get; set; } = 2;
    }
}
