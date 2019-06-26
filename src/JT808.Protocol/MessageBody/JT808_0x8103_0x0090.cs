using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 定位模式，定义如下：
    /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
    /// bit1，0：禁用北斗定位， 1：启用北斗定位；
    /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
    /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0090_Formatter))]
    public class JT808_0x8103_0x0090 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0090;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 定位模式，定义如下：
        /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
        /// bit1，0：禁用北斗定位， 1：启用北斗定位；
        /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
        /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
        /// </summary>
        public byte ParamValue { get; set; }
    }
}
