using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using JT808.Protocol.Metadata;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 存储多媒体数据检索应答
    /// 0x0802
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0802_Formatter))]
    public class JT808_0x0802 : JT808Bodies
    {
        /// <summary>
        /// 应答流水号
        /// 对应的多媒体数据检索消息的流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 多媒体数据总项数
        /// 满足检索条件的多媒体数据总项数
        /// </summary>
        public ushort MultimediaItemCount { get; set; }
        /// <summary>
        /// 检索项集合
        /// </summary>
        public List<JT808MultimediaSearchProperty> MultimediaSearchItems { get; set; }
    }
}
