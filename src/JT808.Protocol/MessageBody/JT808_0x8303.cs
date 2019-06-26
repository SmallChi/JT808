using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using JT808.Protocol.Metadata;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 信息点播菜单设置
    /// 0x8303
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8303_Formatter))]
    public class JT808_0x8303 : JT808Bodies
    {
        /// <summary>
        /// 设置类型
        /// <see cref="JT808.Protocol.Enums.JT808InformationSettingType"/>
        /// </summary>
        public byte SettingType { get; set; }
        /// <summary>
        /// 信息项总数
        /// </summary>
        public byte InformationItemCount { get; set; }
        /// <summary>
        /// 信息点播信息项组成数据
        /// 信息项列表
        /// </summary>
        public List<JT808InformationItemProperty> InformationItems { get; set; }
    }
}
