using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端控制
    /// </summary>
    public class JT808_0x8105 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8105>, IJT808Analyze, IJT808_2019_Version
    {
        public override ushort MsgId { get; } = 0x8105;
        public override string Description => "终端控制";
        /// <summary>
        /// 命令字
        /// </summary>
        public byte CommandWord { get; set; }
        /// <summary>
        /// 命令参数
        /// </summary>
        public CommandParams CommandValue { get; set; }

        public JT808_0x8105 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8105 jT808_0x8105 = new JT808_0x8105
            {
                CommandWord = reader.ReadByte()
            };
            if (jT808_0x8105.CommandWord == 1 || jT808_0x8105.CommandWord == 2)
            {
                jT808_0x8105.CommandValue = new CommandParams();
                jT808_0x8105.CommandValue.SetCommandParams(reader.ReadRemainStringContent());
            }
            return jT808_0x8105;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8105 value, IJT808Config config)
        {
            writer.WriteByte(value.CommandWord);
            if (value.CommandWord == 1 || value.CommandWord == 2)
            {
                writer.WriteString(value.CommandValue.ToString());
            }
        }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8105 value = new JT808_0x8105
            {
                CommandWord = reader.ReadByte()
            };
            writer.WriteNumber($"[{ value.CommandWord.ReadNumber()}]命令字", value.CommandWord);
            if (value.CommandWord == 1 || value.CommandWord == 2)
            {
                value.CommandValue = new CommandParams();
                var commandValueBuffer = reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength()).ToArray();
                string commandValue=reader.ReadRemainStringContent();
                writer.WriteString($"[{ commandValueBuffer.ToHexString()}]命令参数", commandValue);
                writer.WriteStartObject("命令参数对象");
                var values = commandValue.Split(';');
                if (!string.IsNullOrEmpty(values[0]))
                {
                    var connectionControl = byte.Parse(values[0]);
                    writer.WriteNumber("连接控制", connectionControl);
                }
                else
                {
                    writer.WriteNull("连接控制");
                }
                if (!string.IsNullOrEmpty(values[1]))
                {
                    var dialPointName = values[1];
                    writer.WriteString("拨号点名称", dialPointName);
                }
                else
                {
                    writer.WriteNull("拨号点名称");
                }
                if (!string.IsNullOrEmpty(values[2]))
                {
                    var dialUserName = values[2];
                    writer.WriteString("拨号用户名", dialUserName);
                }
                else
                {
                    writer.WriteNull("拨号用户名");
                }
                if (!string.IsNullOrEmpty(values[3]))
                {
                    var dialPwd = values[3];
                    writer.WriteString("拨号密码", dialPwd);
                }
                else
                {
                    writer.WriteNull("拨号密码");
                }
                if (!string.IsNullOrEmpty(values[4]))
                {
                    var serverUrl = values[4];
                    writer.WriteString("服务器地址", serverUrl);
                }
                else
                {
                    writer.WriteNull("服务器地址");
                }
                if (!string.IsNullOrEmpty(values[5]))
                {
                    var tcpPort = ushort.Parse(values[5]);
                    writer.WriteNumber("TCP端口", tcpPort);
                }
                else
                {
                    writer.WriteNull("TCP端口");
                }
                if (!string.IsNullOrEmpty(values[6]))
                {
                    var udpPort = ushort.Parse(values[6]);
                    writer.WriteNumber("UDP端口", udpPort);
                }
                else
                {
                    writer.WriteNull("UDP端口");
                }
                if (!string.IsNullOrEmpty(values[7]))
                {
                    var manufacturerCode = long.Parse(values[7]);
                    writer.WriteNumber("制造商ID", manufacturerCode);
                }
                else
                {
                    writer.WriteNull("制造商ID");
                }
                if (!string.IsNullOrEmpty(values[8]))
                {
                    var monitoringPlatformAuthenticationCode = values[8];
                    writer.WriteString("监管平台鉴权码", monitoringPlatformAuthenticationCode);
                }
                else
                {
                    writer.WriteNull("监管平台鉴权码");
                }
                if (!string.IsNullOrEmpty(values[9]))
                {
                    var hardwareVersion = values[9];
                    writer.WriteString("硬件版本号", hardwareVersion);
                }
                else
                {
                    writer.WriteNull("硬件版本号");
                }
                if (!string.IsNullOrEmpty(values[10]))
                {
                   var firmwareVersion = values[10];
                   writer.WriteString("固件版本号", firmwareVersion);
                }
                else
                {
                    writer.WriteNull("固件版本号");
                }
                if (!string.IsNullOrEmpty(values[11]))
                {
                    var url = values[11];
                    writer.WriteString("URL地址", url);
                }
                else
                {
                    writer.WriteNull("URL地址");
                }
                if (!string.IsNullOrEmpty(values[12]))
                {
                    var connectTimeLimit = ushort.Parse(values[12]);
                    writer.WriteNumber("连接到指定服务器时限", connectTimeLimit);
                }
                else
                {
                    writer.WriteNull("连接到指定服务器时限");
                }
                writer.WriteEndObject();
            }
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
            public ushort? TCPPort { get; set; }
            /// <summary>
            /// UDP端口
            /// </summary>
            public ushort? UDPPort { get; set; }
            /// <summary>
            /// 制造商ID
            /// 终端制造商编码
            /// </summary>
            public string MakerId { get; set; }
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
            public ushort? ConnectTimeLimit { get; set; }
            public override string ToString()
            {
                return $"{ConnectionControl};{DialPointName};{DialUserName};{DialPwd};{ServerUrl};{TCPPort};{UDPPort};{MakerId};{MonitoringPlatformAuthenticationCode};{HardwareVersion};{FirmwareVersion};{URL};{ConnectTimeLimit}";
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
                    TCPPort = ushort.Parse(values[5]);
                }
                if (!string.IsNullOrEmpty(values[6]))
                {
                    UDPPort = ushort.Parse(values[6]);
                }
                if (!string.IsNullOrEmpty(values[7]))
                {
                    MakerId = values[7];
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
                    ConnectTimeLimit = ushort.Parse(values[12]);
                }
            }
        }
    }
}
