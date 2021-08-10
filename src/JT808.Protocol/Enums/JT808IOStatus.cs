using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// IO状态位
    /// </summary>
    [Flags]
    public enum JT808IOStatus:ushort
    {
        /// <summary>
        /// 深度休眠状态
        /// </summary>
        深度休眠状态 = 1,
        /// <summary>
        /// 休眠状态
        /// </summary>
        休眠状态 = 2
    }
}
