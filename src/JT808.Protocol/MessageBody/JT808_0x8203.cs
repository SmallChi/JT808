using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 人工确认报警消息
    /// 0x8203
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8203_Formatter))]
    public class JT808_0x8203 : JT808Bodies
    {
        /// <summary>
        /// 报警消息流水号
        /// 需人工确认的报警消息流水号，0 表示该报警类型所有消息
        /// </summary>
        public ushort AlarmMsgNum { get; set; }
        /// <summary>
        /// 人工确认报警类型
        /// </summary>
        public uint ManualConfirmAlarmType { get; set; }
    }
}
