using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Properties;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置矩形区域
    /// 0x8602
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8602Formatter))]
    public class JT808_0x8602 : JT808Bodies
    {
        /// <summary>
        /// 设置属性
        /// <see cref="JT808.Protocol.Enums.JT808SettingProperty"/>
        /// </summary>
        public byte SettingAreaProperty { get; set; }
        /// <summary>
        /// 区域总数
        /// </summary>
        public byte AreaCount { get; set; }
        /// <summary>
        /// 区域项
        /// </summary>
        public List<JT808RectangleAreaProperty> AreaItems { get; set; }

    }
}
