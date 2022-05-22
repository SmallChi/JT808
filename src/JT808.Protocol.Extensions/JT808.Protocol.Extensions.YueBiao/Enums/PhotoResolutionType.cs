using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao.Enums
{
    /// <summary>
    /// 拍照分辨率
    /// </summary>
    public enum PhotoResolutionType:byte
    {
        /// <summary>
        /// 352x288
        /// </summary>
        x352_288 = 0x01,
        /// <summary>
        /// 704x288
        /// </summary>
        x704_288 = 0x02,
        /// <summary>
        /// 704x576
        /// </summary>
        x704_576 = 0x03,
        /// <summary>
        /// 640x480
        /// </summary>
        x640_480 = 0x04,
        /// <summary>
        /// 1280x720
        /// </summary>
        x1280_720 = 0x05,
        /// <summary>
        /// 1920x1080
        /// </summary>
        x1920_1080 = 0x06,
        /// <summary>
        /// 不修改参数
        /// not Modify parameters
        /// </summary>
        not_modify_parameters = 0xFF
    }
}
