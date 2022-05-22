using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.SuBiao.Enums
{
    /// <summary>
    /// 主动拍照策略
    /// Active photo taking strategy
    /// </summary>
    public enum ActivePhotographyStrategyType:byte
    {
        /// <summary>
        /// 不开启
        /// Is not enabled
        /// </summary>
        not_enabled = 0x00,
        /// <summary>
        /// 定时拍照
        /// Camera Timer
        /// </summary>
        camera_timer = 0x01,
        /// <summary>
        /// 定距拍照
        /// Distance take photos
        /// </summary>
        distance_take_photos = 0x02,
        /// <summary>
        /// 保留
        /// reserve
        /// </summary>
        reserve = 0x03,
        /// <summary>
        /// 不修改参数
        /// Do not Modify parameters
        /// </summary>
        not_modify_parameters = 0xFF
    }
}
