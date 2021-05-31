using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao.Enums
{
    /// <summary>
    /// 升级状态
    /// </summary>
    public enum JT808UpgradeStatus:byte
    {
        /// <summary>
        /// 固件下载中
        /// </summary>
        固件下载中 = 0x01,
        /// <summary>
        /// 固件下载成功
        /// </summary>
        固件下载成功 = 0x02,
        /// <summary>
        /// 固件下载失败
        /// </summary>
        固件下载失败 = 0x03,
        /// <summary>
        /// 固件安装中
        /// </summary>
        固件安装中 = 0x04,
        /// <summary>
        /// 安装成功
        /// </summary>
        安装成功 = 0x05,
        /// <summary>
        /// 安装失败
        /// </summary>
        安装失败 = 0x06,
        /// <summary>
        /// 未找到目标设备
        /// </summary>
        未找到目标设备 = 0x07,
        /// <summary>
        /// 硬件型号不支持
        /// </summary>
        硬件型号不支持 = 0x08,
        /// <summary>
        /// 软件版本相同
        /// </summary>
        软件版本相同 = 0x09,
        /// <summary>
        /// 软件版本不支持
        /// </summary>
        软件版本不支持 = 0x0a,
        /// <summary>
        /// 其他
        /// </summary>
        其他 = 0x0b,
    }
}
