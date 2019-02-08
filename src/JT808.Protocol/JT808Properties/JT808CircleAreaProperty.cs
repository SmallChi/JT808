using System;

namespace JT808.Protocol.JT808Properties
{
    /// <summary>
    /// 圆形区域属性
    /// </summary>
    public class JT808CircleAreaProperty
    {
        /// <summary>
        /// 区域 ID
        /// </summary>
        public uint AreaId { get; set; }
        /// <summary>
        /// 区域属性
        /// </summary>
        public ushort AreaProperty { get; set; }
        /// <summary>
        /// 中心点纬度
        /// 以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public uint CenterPointLat { get; set; }
        /// <summary>
        /// 中心点经度
        /// 以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public uint CenterPointLng { get; set; }
        /// <summary>
        /// 半径
        /// 单位为米（m），路段为该拐点到下一拐点
        /// </summary>
        public uint Radius { get; set; }
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
    }
}
