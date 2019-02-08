using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 临时位置跟踪控制
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8202Formatter))]
    public class JT808_0x8202 : JT808Bodies
    {
        /// <summary>
        /// 时间间隔
        /// 单位为秒（s），0 则停止跟踪。停止跟踪无需带后继字段
        /// </summary>
        public ushort Interval { get; set; }

        /// <summary>
        /// 位置跟踪有效期
        /// 单位为秒（s），终端在接收到位置跟踪控制消息后，在有效期截止时间之前，依据消息中的时间间隔发送位置汇报
        /// </summary>
        public int LocationTrackingValidity { get; set; }
    }
}
