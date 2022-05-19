namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 位置类型
    /// Position type
    /// </summary>
    public enum JT808PositionType : byte
    {
        /// <summary>
        /// 无特定位置
        /// No specific position
        /// </summary>
        no_specific_position = 0x00,
        /// <summary>
        /// 圆形区域
        /// circular region
        /// </summary>
        circular_region = 0x01,
        /// <summary>
        /// 矩形区域
        /// </summary>
        rectangular_region = 0x02,
        /// <summary>
        /// 多边形区域
        /// polyarea
        /// </summary>
        polyarea = 0x03,
        /// <summary>
        /// 路段
        /// Road Segment
        /// </summary>
        road_segment = 0x04
    }
}
