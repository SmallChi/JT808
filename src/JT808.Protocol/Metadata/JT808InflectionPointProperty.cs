namespace JT808.Protocol.Metadata
{
    /// <summary>
    /// 拐点属性
    /// </summary>
    public struct JT808InflectionPointProperty
    {
        /// <summary>
        /// 拐点 ID
        /// </summary>
        public uint InflectionPointId { get; set; }
        /// <summary>
        /// 路段 ID
        /// </summary>
        public uint SectionId { get; set; }
        /// <summary>
        /// 拐点纬度
        /// 以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public uint InflectionPointLat { get; set; }
        /// <summary>
        /// 拐点经度
        /// 以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public uint InflectionPointLng { get; set; }
        /// <summary>
        /// 路段宽度
        /// 单位为米（m），路段为该拐点到下一拐点
        /// </summary>
        public byte SectionWidth { get; set; }
        /// <summary>
        /// 路段属性
        /// </summary>
        public byte SectionProperty { get; set; }
        /// <summary>
        /// 路段行驶过长阈值
        /// 单位为秒（s），若路段属性 0 位为 0 则没有该字段
        /// </summary>
        public ushort? SectionLongDrivingThreshold { get; set; }
        /// <summary>
        /// 路段行驶不足阈值
        /// 单位为秒（s），若路段属性 0 位为 0 则没有该字段
        /// </summary>
        public ushort? SectionDrivingUnderThreshold { get; set; }
        /// <summary>
        /// 路段最高速度
        /// 单位为公里每小时（km/h），若路段属性 1 位为 0 则没有该字段
        /// </summary>
        public ushort? SectionHighestSpeed { get; set; }
        /// <summary>
        /// 超速持续时间 
        /// 单位为秒（s），若路段属性 1 位为 0 则没有该字段
        /// </summary>
        public byte? SectionOverspeedDuration { get; set; }
        /// <summary>
        /// 夜间最高速度
        /// 单位为千米每小时(km/h),若路段属性 1 位为 0 则没有该字段
        /// </summary>
        public ushort? NightMaximumSpeed { get; set; }
    }
}
