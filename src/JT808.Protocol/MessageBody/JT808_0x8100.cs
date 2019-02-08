using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端注册应答
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8100Formatter))]
    public class JT808_0x8100 : JT808Bodies
    {
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public ushort MsgNum { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public JT808TerminalRegisterResult JT808TerminalRegisterResult { get; set; }

        /// <summary>
        /// 鉴权码
        /// 只有在成功后才有该字段
        /// </summary>
        public string Code { get; set; }
    }
}
