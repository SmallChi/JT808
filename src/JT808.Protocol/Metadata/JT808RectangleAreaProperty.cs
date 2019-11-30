using JT808.Protocol.Interfaces;
using System;

namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// 矩形区域属性
    /// </summary>
    public struct JT808RectangleAreaProperty : IJT808_2019_Version
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
        /// 左上点纬度
        /// 以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public uint UpLeftPointLat { get; set; }
        /// <summary>
        /// 左上点经度
        /// 以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public uint UpLeftPointLng { get; set; }
        /// <summary>
        /// 右下点纬度
        /// 以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public uint LowRightPointLat { get; set; }
        /// <summary>
        /// 右下点经度
        /// 以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public uint LowRightPointLng { get; set; }
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
        /// 夜间最高速度
        /// 2019版本
        /// </summary>
        public ushort NightMaximumSpeed { get; set; }
        /// <summary>
        /// 名称长度
        /// 2019版本
        /// </summary>
        public ushort NameLength { get; set; }
        /// <summary>
        /// 区域名称
        /// 2019版本
        /// </summary>
        public string AreaName { get; set; }
    }
}
