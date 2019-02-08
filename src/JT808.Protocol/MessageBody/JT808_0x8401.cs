using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Properties;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置电话本
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8401Formatter))]
    public class JT808_0x8401 : JT808Bodies
    {
        /// <summary>
        /// 设置类型
        /// </summary>
        public JT808SettingTelephoneBook SettingTelephoneBook { get; set; }
        /// <summary>
        /// 联系人总数
        /// </summary>
        public byte ContactCount { get; set; }
        /// <summary>
        /// 联系人项
        /// </summary>
        public IList<JT808ContactProperty> JT808ContactProperties { get; set; }
    }
}
