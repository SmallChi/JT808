using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808头部数据包
    /// </summary>
    [JT808Formatter(typeof(JT808HeaderPackageFromatter))]
    public class JT808HeaderPackage
    {
        /// <summary>
        /// 头数据
        /// </summary>
        public JT808Header Header { get;  set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] Bodies { get; set; }
    }
}
