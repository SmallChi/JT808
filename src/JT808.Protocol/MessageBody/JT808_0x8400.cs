using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 电话回拨
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8400Formatter))]
    public class JT808_0x8400 : JT808Bodies
    {
        /// <summary>
        /// 0:普通通话；1:监听
        /// </summary>
        public JT808CallBackType CallBack { get; set; }
        /// <summary>
        /// 电话号码 
        /// 最长为 20 字节
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
