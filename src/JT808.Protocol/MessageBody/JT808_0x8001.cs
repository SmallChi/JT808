using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 平台通用应答
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8001Formatter))]
    public class JT808_0x8001 : JT808Bodies
    {
        public ushort MsgNum { get; set; }
        public JT808MsgId MsgId { get; set; }
        public JT808PlatformResult JT808PlatformResult { get; set; }
    }
}
