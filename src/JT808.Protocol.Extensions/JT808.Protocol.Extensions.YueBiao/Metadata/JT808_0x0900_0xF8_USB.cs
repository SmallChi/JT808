using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.YueBiao.Metadata
{
    /// <summary>
    /// 
    /// </summary>
    public class JT808_0x0900_0xF8_USB
    {
        /// <summary>
        /// 外设ID
        /// <see cref="JT808.Protocol.Extensions.YueBiao.Enums.USBIDType"/>
        /// </summary>
        public byte USBID { get; set; }
        /// <summary>
        /// 消息长度
        /// </summary>
        public byte MessageLength { get; set; }
        /// <summary>
        /// 公司名称长度
        /// </summary>
        public byte CompantNameLength { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompantName { get; set; }
        /// <summary>
        /// 产品型号长度
        /// </summary>
        public byte ProductModelLength { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductModel { get; set; }
        /// <summary>
        /// 硬件版本号长度
        /// </summary>
        public byte HardwareVersionNumberLength { get; set; }
        /// <summary>
        /// 硬件版本号
        /// ASCII
        /// </summary>
        public string HardwareVersionNumber { get; set; }
        /// <summary>
        /// 软件版本号长度
        /// </summary>
        public byte SoftwareVersionNumberLength { get; set; }
        /// <summary>
        /// 软件版本号
        /// ASCII
        /// </summary>
        public string SoftwareVersionNumber { get; set; }
        /// <summary>
        /// 设备ID长度
        /// </summary>
        public byte DevicesIDLength { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public string DevicesID { get; set; }
        /// <summary>
        /// 客户代码长度
        /// </summary>
        public byte CustomerCodeLength { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerCode { get; set; }
    }
}
