using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置汇报方案，0：根据 ACC 状态； 1：根据登录状态和 ACC 状态，先判断登录状态，若登录再根据 ACC 状态
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0021_Formatter))]
    public class JT808_0x8103_0x0021 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0021;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 位置汇报方案，0：根据 ACC 状态； 1：根据登录状态和 ACC 状态，先判断登录状态，若登录再根据 ACC 状态
        /// </summary>
        public uint ParamValue { get; set; }
    }
}
