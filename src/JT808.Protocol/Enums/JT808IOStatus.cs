using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// IO状态位
    /// IO status bits
    /// </summary>
    [Flags]
    public enum JT808IOStatus:ushort
    {
        /// <summary>
        /// 深度休眠状态
        /// Deep dormant state
        /// </summary>
        deep_dormant_state = 1,
        /// <summary>
        /// 休眠状态
        /// dormant state
        /// </summary>
        dormant_state = 2
    }
}
