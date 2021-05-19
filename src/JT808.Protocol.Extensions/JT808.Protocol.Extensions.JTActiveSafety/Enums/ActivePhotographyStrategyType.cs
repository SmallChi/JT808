using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JTActiveSafety.Enums
{
    /// <summary>
    /// 主动拍照策略
    /// </summary>
    public enum ActivePhotographyStrategyType:byte
    {
        /// <summary>
        /// 不开启
        /// </summary>
        不开启 = 0x00,
        /// <summary>
        /// 定时拍照
        /// </summary>
        定时拍照 = 0x01,
        /// <summary>
        /// 定距拍照
        /// </summary>
        定距拍照 = 0x02,
        /// <summary>
        /// 保留
        /// </summary>
        保留 = 0x03,
        /// <summary>
        /// 不修改参数
        /// </summary>
        不修改参数 = 0xFF
    }
}
