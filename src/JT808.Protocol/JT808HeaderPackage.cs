using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808头部数据包
    /// </summary>
    [JT808Formatter(typeof(JT808HeaderPackageFormatter))]
    public class JT808HeaderPackage
    {
        /// <summary>
        /// 起始符
        /// </summary>
        public byte Begin { get; set; }
        /// <summary>
        /// 头数据
        /// </summary>
        public JT808Header Header { get;  set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] Bodies { get; set; }
        /// <summary>
        /// 校验码
        /// 从消息头开始，同后一字节异或，直到校验码前一个字节，占用一个字节。
        /// </summary>
        public byte CheckCode { get; set; }
        /// <summary>
        /// 终止符
        /// </summary>
        public byte End { get; set; }
    }
}
