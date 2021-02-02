namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 位置类型
    /// </summary>
    public enum JT808PositionType : byte
    {
        /// <summary>
        /// 无特定位置
        /// </summary>
        无特定位置 = 0x00,
        /// <summary>
        /// 圆形区域
        /// </summary>
        圆形区域 = 0x01,
        /// <summary>
        /// 矩形区域
        /// </summary>
        矩形区域 = 0x02,
        /// <summary>
        /// 多边形区域
        /// </summary>
        多边形区域 = 0x03,
        /// <summary>
        /// 路段
        /// </summary>
        路段 = 0x04
    }
}
