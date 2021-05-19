using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JTActiveSafety.Enums
{
    /// <summary>
    /// 工作状态
    /// </summary>
    public enum WorkingConditionType:byte
    {
        /// <summary>
        /// 正常工作
        /// </summary>
        正常工作 = 0x01,
        /// <summary>
        /// 待机状态
        /// </summary>
        待机状态 = 0x02,
        /// <summary>
        /// 升级维护
        /// </summary>
        升级维护 = 0x03,
        /// <summary>
        /// 设备异常
        /// </summary>
        设备异常 = 0x04,
        /// <summary>
        /// 断开连接
        /// </summary>
        断开连接 = 0x10,
    }
}
