using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 摄像头分辨率
    /// </summary>
    public enum JT808CameraResolutionType:byte
    {
        x320_240 = 0x01,
        x640_480 = 0x02,
        x800_600 = 0x03,
        x1020_768 = 0x04,
        x176_144_Qcif = 0x05,
        x352_288_Cif = 0x06,
        x704_288_HALF_D1 = 0x07,
        x704_576_D1 = 0x07,
    }
}
