using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息查询应答
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0201_Formatter))]
    public class JT808_0x0201 : JT808Bodies
    {
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public ushort MsgNum { get; set; }

        /// <summary>
        /// 位置信息汇报见 8.12
        /// </summary>
        public JT808_0x0200 Position { get; set; }
    }
}
