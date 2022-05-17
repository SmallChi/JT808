using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 摄像头分辨率
    /// Camera resolution
    /// </summary>
    public enum JT808CameraResolutionType:byte
    {
        /// <summary>
        /// x320_240
        /// </summary>
        x320_240 = 0x01,
        /// <summary>
        /// x640_480
        /// </summary>
        x640_480 = 0x02,
        /// <summary>
        /// x800_600
        /// </summary>
        x800_600 = 0x03,
        /// <summary>
        /// x1020_768
        /// </summary>
        x1020_768 = 0x04,
        /// <summary>
        /// x176_144_Qcif
        /// </summary>
        x176_144_Qcif = 0x05,
        /// <summary>
        /// x352_288_Cif
        /// </summary>
        x352_288_Cif = 0x06,
        /// <summary>
        /// x704_288_HALF_D1
        /// </summary>
        x704_288_HALF_D1 = 0x07,
        /// <summary>
        /// x704_576_D1
        /// </summary>
        x704_576_D1 = 0x08,
    }
}
