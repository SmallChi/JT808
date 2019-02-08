using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808数据包
    /// </summary>
    [JT808Formatter(typeof(JT808PackageFromatter))]
    public class JT808Package
    {
        /// <summary>
        /// 起始符
        /// </summary>
        public const byte BeginFlag = 0x7e;
        /// <summary>
        /// 终止符
        /// </summary>
        public const byte EndFlag = 0x7e;

        /// <summary>
        /// 起始符
        /// </summary>
        public byte Begin { get; set; } = BeginFlag;

        /// <summary>
        /// 起始符
        /// </summary>
        public byte End { get; set; } = EndFlag;

        /// <summary>
        /// 校验码
        /// 从消息头开始，同后一字节异或，直到校验码前一个字节，占用一个字节。
        /// </summary>
        public byte CheckCode { get; set; }

        /// <summary>
        /// 头数据
        /// </summary>
        public JT808Header Header { get; set; }

        /// <summary>
        /// 数据体
        /// </summary>
        public JT808Bodies Bodies { get; set; }
    }
}
