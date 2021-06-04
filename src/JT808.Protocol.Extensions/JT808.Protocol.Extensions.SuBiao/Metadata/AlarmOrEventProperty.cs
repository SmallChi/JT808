using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.SuBiao.Metadata
{
    /// <summary>
    /// 胎压监测系统报警/事件信息
    /// </summary>
    public class AlarmOrEventProperty
    {
        /// <summary>
        /// 胎压报警位置
        /// </summary>
        public byte TirePressureAlarmPosition { get; set; }
        /// <summary>
        /// 报警/事件类型
        /// </summary>
        public ushort AlarmOrEventType { get; set; }
        /// <summary>
        /// 胎压
        /// </summary>
        public ushort TirePressure { get; set; }
        /// <summary>
        /// 胎温
        /// </summary>
        public ushort TireTemperature { get; set; }
        /// <summary>
        /// 电池电量
        /// </summary>
        public ushort BatteryLevel  { get; set; }
    }
}
