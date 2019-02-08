using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;


namespace JT808.Protocol.MessageBody
{

    [JT808Formatter(typeof(JT808_0x0200_0x13Formatter))]
    public class JT808_0x0200_0x13 : JT808_0x0200_BodyBase
    {
        public override byte AttachInfoId { get; set; } = 0x13;
        public override byte AttachInfoLength { get; set; } = 7;

        /// <summary>
        /// 路段 ID
        /// </summary>
        public int DrivenRouteId { get; set; }

        /// <summary>
        /// 路段行驶时间
        /// 单位为秒（s)
        /// </summary>
        public ushort Time { get; set; }

        /// <summary>
        ///  结果 0：不足；1：过长
        /// </summary>
        public JT808DrivenRouteType DrivenRoute { get; set; }
    }
}
