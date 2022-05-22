using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao.Enums
{
    /// <summary>
    /// 升级状态
    /// Upgrade Status
    /// </summary>
    public enum JT808UpgradeStatus:byte
    {
        /// <summary>
        /// 固件下载中
        /// Firmware download
        /// </summary>
        firmware_download = 0x01,
        /// <summary>
        /// 固件下载成功
        /// Firmware downloaded successfully
        /// </summary>
        firmware_download_success= 0x02,
        /// <summary>
        /// 固件下载失败
        /// Firmware download failure
        /// </summary>
        firmware_download_failure = 0x03,
        /// <summary>
        /// 固件安装中
        /// Firmware Install
        /// </summary>
        firmware_install = 0x04,
        /// <summary>
        /// 安装成功
        /// Install Success
        /// </summary>
        install_success = 0x05,
        /// <summary>
        /// 安装失败
        /// Install Failed
        /// </summary>
        install_failed = 0x06,
        /// <summary>
        /// 未找到目标设备
        /// Target device not found
        /// </summary>
        target_device_not_found = 0x07,
        /// <summary>
        /// 硬件型号不支持
        /// The hardware model is not supported
        /// </summary>
        hardware_model_not_supported = 0x08,
        /// <summary>
        /// 软件版本相同
        /// Same Software Version
        /// </summary>
        same_software_version = 0x09,
        /// <summary>
        /// 软件版本不支持
        /// The software version is not supported
        /// </summary>
        software_version_not_supported = 0x0a,
        /// <summary>
        /// 其他
        /// Other
        /// </summary>
        Other = 0x0b,
    }
}
