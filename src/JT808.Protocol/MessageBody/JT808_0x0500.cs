using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆控制应答
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0500Formatter))]
    public class JT808_0x0500 : JT808Bodies
    {
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 位置信息汇报消息体
        /// </summary>
        public JT808_0x0200 JT808_0x0200 { get; set; }
    }
}
