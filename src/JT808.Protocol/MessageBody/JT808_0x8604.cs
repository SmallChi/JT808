using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Properties;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置多边形区域
    /// 0x8604
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8604Formatter))]
    public class JT808_0x8604 : JT808Bodies
    {
        /// <summary>
        /// 区域 ID
        /// </summary>
        public uint AreaId { get; set; }
        /// <summary>
        /// 区域属性
        /// <see cref="JT808.Protocol.Enums.JT808SettingProperty"/>
        /// </summary>
        public ushort AreaProperty { get; set; }
        /// <summary>
        /// 起始时间
        /// YY-MM-DD-hh-mm-ss，若区域属性 0 位为 0 则没有该字段
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// YY-MM-DD-hh-mm-ss，若区域属性 0 位为 0 则没有该字段
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 最高速度
        /// Km/h，若区域属性 1 位为 0 则没有该字段
        /// </summary>
        public ushort? HighestSpeed { get; set; }
        /// <summary>
        /// 超速持续时间 
        /// 单位为秒（s）（类似表述，同前修改），若区域属性 1 位为 0 则没有该字段
        /// </summary>
        public byte? OverspeedDuration { get; set; }
        /// <summary>
        /// 区域总顶点数
        /// </summary>
        public ushort PeakCount { get; set; }
        /// <summary>
        /// 顶点项
        /// </summary>
        public List<JT808PeakProperty> PeakItems { get; set; }
    }
}
