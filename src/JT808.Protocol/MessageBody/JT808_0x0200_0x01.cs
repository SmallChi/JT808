using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using System.Runtime.Serialization;

namespace JT808.Protocol.MessageBody
{

    [JT808Formatter(typeof(JT808_0x0200_0x01Formatter))]
    public class JT808_0x0200_0x01 : JT808_0x0200_BodyBase
    {
        public override byte AttachInfoId { get; set; } = 0x01;
        public override byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 里程
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// 里程 1/10km，对应车上里程表读数
        /// </summary>
        [IgnoreDataMember]
        public double ConvertMileage => Mileage / 10.0;
    }
}
