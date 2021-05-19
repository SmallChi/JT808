using System;

namespace JT808.Protocol.Extensions.JT1078.Enums
{
    [Flags]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public enum VideoRelateAlarmType:uint
    {
        视频信号丢失报警=0,
        视频信号遮挡报警=2,
        存储单元故障报警=4,
        其他视频设备故障报警=8,
        客车超员报警=16,
        异常驾驶行为报警=32,
        特殊报警录像达到存储阈值报警=64,
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
