using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 碰撞报警参数设置
    /// b7-b0： 碰撞时间，单位 4ms；
    /// b15-b8：碰撞加速度，单位 0.1g，设置范围在：0-79 之间，默认为10。
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x005D_Formatter))]
    public class JT808_0x8103_0x005D : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x005D;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 碰撞报警参数设置
        /// b7-b0： 碰撞时间，单位 4ms；
        /// b15-b8：碰撞加速度，单位 0.1g，设置范围在：0-79 之间，默认为10。
        /// </summary>
        public ushort ParamValue { get; set; }
    }
}
