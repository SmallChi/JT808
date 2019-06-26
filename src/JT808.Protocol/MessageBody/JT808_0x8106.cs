using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using System;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询指定终端参数
    /// 0x8106
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8106_Formatter))]
    public class JT808_0x8106 : JT808Bodies
    {
        /// <summary>
        /// 参数总数
        /// 参数总数为 n
        /// </summary>
        public byte ParameterCount { get; set; }
        /// <summary>
        /// 参数 ID 列表
        /// 参数顺序排列，如“参数 ID1 参数 ID2......参数IDn”。
        /// </summary>
        public UInt32[] Parameters { get; set; }
    }
}
