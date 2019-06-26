using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 驾驶员未登录汇报距离间隔，单位为米（m），>0
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x002D_Formatter))]
    public class JT808_0x8103_0x002D : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x002D;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 驾驶员未登录汇报距离间隔，单位为米（m），>0
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
