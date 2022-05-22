using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.SuBiao.Enums
{
    /// <summary>
    /// 工作状态
    /// Working
    /// </summary>
    public enum WorkingConditionType:byte
    {
        /// <summary>
        /// 正常工作
        /// normal_normal
        /// </summary>
        normal_normal = 0x01,
        /// <summary>
        /// 待机状态
        /// stand by
        /// </summary>
        stand_by = 0x02,
        /// <summary>
        /// 升级维护
        /// Upgrade maintenance
        /// </summary>
        upgrade_maintain = 0x03,
        /// <summary>
        /// 设备异常
        /// unit exception
        /// </summary>
        unit_exception = 0x04,
        /// <summary>
        /// 断开连接
        /// disconnect 
        /// </summary>
        disconnect = 0x10,
    }
}
