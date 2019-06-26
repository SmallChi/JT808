using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    [JT808Formatter(typeof(JT808_0x0200_0x12_Formatter))]
    public class JT808_0x0200_0x12 : JT808_0x0200_BodyBase
    {
        /// <summary>
        /// 位置类型
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 方向 
        /// 0：进
        /// 1：出
        /// </summary>
        public JT808DirectionType Direction { get; set; }
        public override byte AttachInfoId { get; set; } = 0x12;
        public override byte AttachInfoLength { get; set; } = 6;
    }
}
