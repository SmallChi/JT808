using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// CAN 总线 ID 单独采集设置：
    /// bit63-bit32 表示此 ID 采集时间间隔(ms)，0 表示不采集；
    /// bit31 表示 CAN 通道号，0：CAN1，1：CAN2；
    /// bit30 表示帧类型，0：标准帧，1：扩展帧；
    /// bit29 表示数据采集方式，0：原始数据，1：采集区间的计算值；
    /// bit28-bit0 表示 CAN 总线 ID。
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0110_Formatter))]
    public class JT808_0x8103_0x0110 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0110;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// CAN 总线 ID 单独采集设置：
        /// bit63-bit32 表示此 ID 采集时间间隔(ms)，0 表示不采集；
        /// bit31 表示 CAN 通道号，0：CAN1，1：CAN2；
        /// bit30 表示帧类型，0：标准帧，1：扩展帧；
        /// bit29 表示数据采集方式，0：原始数据，1：采集区间的计算值；
        /// bit28-bit0 表示 CAN 总线 ID。
        /// </summary>
        public byte[] ParamValue { get; set; }
    }
}
