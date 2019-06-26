namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 位置类型
    /// </summary>
    public enum JT808PositionType : byte
    {
        无特定位置 = 0x00,
        圆形区域 = 0x01,
        矩形区域 = 0x02,
        多边形区域 = 0x03,
        路段 = 0x04
    }
}
