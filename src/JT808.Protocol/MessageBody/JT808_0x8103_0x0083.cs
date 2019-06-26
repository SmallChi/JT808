using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 公安交通管理部门颁发的机动车号牌
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0083_Formatter))]
    public class JT808_0x8103_0x0083 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0083;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 公安交通管理部门颁发的机动车号牌
        /// </summary>
        public string ParamValue { get; set; }
    }
}
