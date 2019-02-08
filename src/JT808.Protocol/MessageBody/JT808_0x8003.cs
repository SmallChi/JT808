using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 补传分包请求
    /// 0x8003
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8003Formatter))]
    public class JT808_0x8003 : JT808Bodies
    {
        /// <summary>
        /// 原始消息流水号
        /// 对应要求补传的原始消息第一包的消息流水号
        /// </summary>
        public ushort OriginalMsgNum { get; set; }
        /// <summary>
        /// 重传包总数
        /// n
        /// </summary>
        public byte AgainPackageCount { get; set; }
        /// <summary>
        /// 重传包 ID 列表
        /// BYTE[2*n]
        /// 重传包序号顺序排列，如“包 ID1 包 ID2......包 IDn”。
        /// </summary>
        public byte[] AgainPackageData { get; set; }
    }
}
