using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 删除路线
    /// 0x8607
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8607_Formatter))]
    public class JT808_0x8607 : JT808Bodies
    {
        /// <summary>
        /// 区域数
        /// 本条消息中包含的区域数，不超过 125 个，多于 125个建议用多条消息，0 为删除所有圆形区域
        /// </summary>
        public byte AreaCount { get; set; }
        /// <summary>
        /// 区域ID集合
        /// </summary>
        public List<uint> AreaIds { get; set; }
    }
}
