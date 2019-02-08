using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 复位电话号码，可采用此电话号码拨打终端电话让终端复位
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0041Formatter))]
    public class JT808_0x8103_0x0041 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0041;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 复位电话号码，可采用此电话号码拨打终端电话让终端复位
        /// </summary>
        public string ParamValue { get; set; }
    }
}
