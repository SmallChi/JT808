using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JTActiveSafety.Enums
{
    /// <summary>
    /// USB编号类型
    /// </summary>
    public enum USBIDType:byte
    {
        /// <summary>
        /// 高级驾驶辅助系统
        /// </summary>
        ADAS = 0x64,
        /// <summary>
        /// 驾驶员状态监控系统
        /// </summary>
        DSM = 0x65,
        /// <summary>
        /// 轮胎气压监测系统
        /// </summary>
        TPMS = 0x66,
        /// <summary>
        /// 盲点监测系统
        /// </summary>
        BSD = 0x67
    }
}
