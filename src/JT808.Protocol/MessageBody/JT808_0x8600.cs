using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Properties;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置圆形区域
    /// 0x8600
    /// 注：本条消息协议支持周期时间范围，如要限制每天的8:30-18:00，起始/结束时间设为：00-00-00-08-30-00/00-00-00-18-00-00，其他以此类推
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8600Formatter))]
    public class JT808_0x8600 : JT808Bodies
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
        public List<JT808CircleAreaProperty> AreaItems { get; set; }
    }
}
