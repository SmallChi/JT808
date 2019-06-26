using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters.MessageBodyFormatters;
using System;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端控制
    /// </summary>
    [JT808Formatter(typeof(JT808_0x8105_Formatter))]
    public class JT808_0x8105 : JT808Bodies
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public byte CommandWord { get; set; }
        /// <summary>
        /// 命令参数
        /// </summary>
        public CommandParams CommandValue { get; set; }
    }
    /// <summary>
    /// 命令参数
    /// </summary>
    public class CommandParams
    {
        /// <summary>
        /// 连接控制
        /// 0：切换到指定监管平台服务器，连接到该服务器后即进入应急状态，
        ///此状态下仅有下发控制指令的监管平台可发送包括短信在内的控制指令；
        ///1：切换回原缺省监控平台服务器，并恢复正常状态。
        /// </summary>
        public byte? ConnectionControl { get; set; }
        /// <summary>
        /// 拨号点名称
        /// 一般为服务器 APN，无线通信拨号访问点，若网络制式为 CDMA，则该值为 PPP 连接拨号号码
        /// </summary>
        public string DialPointName { get; set; }
        /// <summary>
        /// 拨号用户名
        /// 服务器无线通信拨号用户名
        /// </summary>
        public string DialUserName { get; set; }
        /// <summary>
        /// 拨号密码
        /// 服务器无线通信拨号密码
        /// </summary>
        public string DialPwd { get; set; }
        /// <summary>
        /// 服务器地址
        /// 服务器地址;IP 或域名
        /// </summary>
        public string ServerUrl { get; set; }
        /// <summary>
        /// TCP端口
        /// </summary>
        public UInt16? TCPPort { get; set; }
        /// <summary>
        /// UDP端口
        /// </summary>
        public UInt16? UDPPort { get; set; }
        /// <summary>
        /// 制造商 ID  BYTE[5] 
        /// 终端制造商编码
        /// </summary>
        public long? ManufacturerCode { get; set; }
        /// <summary>
        /// 监管平台鉴权码
        /// 监管平台下发的鉴权码，仅用于终端连接到监管平台之后的鉴权，终端连接回原监控平台还用原鉴权码
        /// </summary>
        public string MonitoringPlatformAuthenticationCode { get; set; }
        /// <summary>
        /// 硬件版本
        /// 终端的硬件版本号，由制造商自定
        /// </summary>
        public string HardwareVersion { get; set; }
        /// <summary>
        /// 固件版本
        ///  终端的固件版本号，由制造商自定
        /// </summary>
        public string FirmwareVersion { get; set; }
        /// <summary>
        /// URL 地址 完整 URL 地址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 连接到指定服务器时限
        /// 单位：分（min），值非 0 后的有效期截止前，终端应连回原地址。
        ///                            若值为 0，则表示一直连接指 定服务器
        /// </summary>
        public UInt16? ConnectTimeLimit { get; set; }
        public override string ToString()
        {
            return $"{ConnectionControl};{DialPointName};{DialUserName};{DialPwd};{ServerUrl};{TCPPort};{UDPPort};{ManufacturerCode};{MonitoringPlatformAuthenticationCode};{HardwareVersion};{FirmwareVersion};{URL};{ConnectTimeLimit}";
        }
        public void SetCommandParams(string commandValue)
        {
            var values = commandValue.Split(';');
            if (!string.IsNullOrEmpty(values[0]))
            {
                ConnectionControl = byte.Parse(values[0]);
            }
            if (!string.IsNullOrEmpty(values[1]))
            {
                DialPointName = values[1];
            }
            if (!string.IsNullOrEmpty(values[2]))
            {
                DialUserName = values[2];
            }
            if (!string.IsNullOrEmpty(values[3]))
            {
                DialPwd = values[3];
            }
            if (!string.IsNullOrEmpty(values[4]))
            {
                ServerUrl = values[4];
            }
            if (!string.IsNullOrEmpty(values[5]))
            {
                TCPPort = UInt16.Parse(values[5]);
            }
            if (!string.IsNullOrEmpty(values[6]))
            {
                UDPPort = UInt16.Parse(values[6]);
            }
            if (!string.IsNullOrEmpty(values[7]))
            {
                ManufacturerCode = long.Parse(values[7]);
            }
            if (!string.IsNullOrEmpty(values[8]))
            {
                MonitoringPlatformAuthenticationCode = values[8];
            }
            if (!string.IsNullOrEmpty(values[9]))
            {
                HardwareVersion = values[9];
            }
            if (!string.IsNullOrEmpty(values[10]))
            {
                FirmwareVersion = values[10];
            }
            if (!string.IsNullOrEmpty(values[11]))
            {
                URL = values[11];
            }
            if (!string.IsNullOrEmpty(values[12]))
            {
                ConnectTimeLimit = UInt16.Parse(values[12]);
            }
        }
    }
}
