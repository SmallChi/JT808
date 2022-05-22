using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.SuBiao.Enums
{
    /// <summary>
    /// 视频录制分辨率
    /// </summary>
    public enum VideoRecordingResolutionType:byte
    {
        /// <summary>
        /// CIF
        /// </summary>
        CIF = 0x01,
        /// <summary>
        /// HD1
        /// </summary>
        HD1 = 0x02,
        /// <summary>
        /// D1
        /// </summary>
        D1 = 0x03,
        /// <summary>
        /// WD1
        /// </summary>
        WD1 = 0x04,
        /// <summary>
        /// VGA
        /// </summary>
        VGA = 0x05,
        /// <summary>
        /// 720P
        /// </summary>
        _720P = 0x06,
        /// <summary>
        /// 1080P
        /// </summary>
        _1080P = 0x07,
        /// <summary>
        /// 不修改参数
        /// not_modify_parameters
        /// </summary>
        not_modify_parameters = 0xFF
    }
}
