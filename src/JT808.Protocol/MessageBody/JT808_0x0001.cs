using JT808.Protocol.Enums;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using JT808.Protocol.Attributes;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端通用应答
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0001_Formatter))]
    public class JT808_0x0001 : JT808Bodies
    {
        /// <summary>
        /// 应答流水号
        /// 对应的平台消息的流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 应答 ID
        /// 对应的平台消息的 ID
        /// <see cref="JT808.Protocol.Enums.JT808MsgId"/>
        /// </summary>
        public ushort MsgId { get; set; }
        /// <summary>
        /// 结果
        /// 0：成功/确认；1：失败；2：消息有误；3：不支持
        /// </summary>
        public JT808TerminalResult JT808TerminalResult { get; set; }
    }
}
