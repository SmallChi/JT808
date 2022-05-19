using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// JT808版本号
    /// JT808 Version
    /// </summary>
    public enum JT808Version:byte
    {
        /// <summary>
        /// 2011
        /// </summary>
        JTT2011 = 0,
        /// <summary>
        /// 2013
        /// </summary>
        JTT2013 =1,
        /// <summary>
        /// 2019
        /// </summary>
        JTT2019=2,
        /// <summary>
        /// 强制2013
        /// </summary>
        JTT2013Force = 99
    }
}
