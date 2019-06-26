using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆控制
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8500_Formatter))]
    public class JT808_0x8500 : JT808Bodies
    {
        /// <summary>
        /// 控制标志 
        /// 控制指令标志位数据格式
        /// 0：车门解锁；1：车门加锁
        /// 1-7 保留
        /// </summary>
        public byte ControlFlag { get; set; }
    }
}
