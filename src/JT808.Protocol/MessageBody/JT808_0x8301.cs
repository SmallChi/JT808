using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Properties;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 事件设置
    /// 0x8301
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8301Formatter))]
    public class JT808_0x8301 : JT808Bodies
    {
        /// <summary>
        /// 设置类型
        /// <see cref="JT808.Protocol.Enums.JT808EventSettingType"/>
        /// </summary>
        public byte SettingType { get; set; }
        /// <summary>
        /// 设置总数
        /// </summary>
        public byte SettingCount { get; set; }
        /// <summary>
        /// 事件项
        /// </summary>
        public List<JT808EventProperty> EventItems { get; set; }
    }
}
