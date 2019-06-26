using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 恢复出厂设置电话号码，可采用此电话号码拨打终端电话让终端恢复出厂设置
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8103_0x0042_Formatter))]
    public class JT808_0x8103_0x0042 : JT808_0x8103_BodyBase
    {
        public override uint ParamId { get; set; } = 0x0042;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 恢复出厂设置电话号码，可采用此电话号码拨打终端电话让终端恢复出厂设置
        /// </summary>
        public string ParamValue { get; set; }
    }
}
