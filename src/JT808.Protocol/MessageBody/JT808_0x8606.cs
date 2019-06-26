using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using JT808.Protocol.Metadata;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 设置路线
    /// 0x8606
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8606_Formatter))]
    public class JT808_0x8606 : JT808Bodies
    {
        /// <summary>
        /// 路线 ID
        /// </summary>
        public uint RouteId { get; set; }
        /// <summary>
        /// 路线属性
        /// 路线属性数据格式见 表 67
        /// </summary>
        public ushort RouteProperty { get; set; }
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
        /// 路线总拐点数
        /// </summary>
        public ushort InflectionPointCount { get; set; }
        /// <summary>
        /// 拐点项
        /// </summary>
        public List<JT808InflectionPointProperty> InflectionPointItems { get; set; }
    }
}
