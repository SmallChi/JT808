using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端控制
    /// </summary>
    public class JT808_0x8105 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8105>, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 标准命令参数数量
        /// </summary>
        public const int CommandParameterCount = 13;
        const char CommandParameterSeparator = ';';
        const byte CommandParameterSeparatorValue = (byte)';';
        /// <summary>
        /// 0x8105
        /// </summary>
        public override ushort MsgId { get; } = 0x8105;
        /// <summary>
        /// 终端控制
        /// </summary>
        public override string Description => "终端控制";
        /// <summary>
        /// 命令字
        /// </summary>
        public byte CommandWord { get; set; }
        /// <summary>
        /// 命令参数集合
        /// </summary>
        public List<ICommandParameter> CommandParameters { get; set; }
        /// <summary>
        /// 自定义命令参数集合
        /// </summary>
        public List<ICommandParameter> CustomCommandParameters { get; set; }
        /// <summary>
        /// 未知的命令参数集合
        /// key:order
        /// value:data
        /// </summary>
        public Dictionary<int,byte[]> UnknownCommandParameters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT808_0x8105 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8105 jT808_0x8105 = new JT808_0x8105
            {
                CommandWord = reader.ReadByte(),
                CommandParameters = new List<ICommandParameter>(),
                CustomCommandParameters = new List<ICommandParameter>(),
                UnknownCommandParameters = new Dictionary<int, byte[]>()
            };
            if (jT808_0x8105.CommandWord == 1 || jT808_0x8105.CommandWord == 2)
            {
                int remain = reader.ReadCurrentRemainContentLength();
                if (remain > 0)
                {
                    var commandValueBuffer = reader.ReadArray(remain);
                    List<byte[]> commandParameters = new List<byte[]>();
                    while (true)
                    {
                        var index = commandValueBuffer.IndexOf(CommandParameterSeparatorValue);
                        if (index <= 0) break;
                        if (index == 1)
                        {
                            commandParameters.Add(null);
                        }
                        else
                        {
                            var value = commandValueBuffer.Slice(0, index);
                            commandParameters.Add(value.ToArray());
                        }
                        commandValueBuffer = commandValueBuffer.Slice(index);
                    }
                    for (int i = 0; i < commandParameters.Count; i++)
                    {
                        //如果大于13个命令参数，说明有自定义命令参数再里面
                        var cmd = commandParameters[i];
                        if (i >= 13)
                        {
                            //读取自定义的命令参数
                            if(config.JT808_0x8105_Cusotm_Factory.Map.TryGetValue(i,out Type type))
                            {
                                var commandParameter = (ICommandParameter)Activator.CreateInstance(type);
                                commandParameter.ToValue(cmd);
                                jT808_0x8105.CustomCommandParameters.Add(commandParameter);
                            }
                            else
                            {
                                UnknownCommandParameters.Add(i, cmd);
                            }
                        }
                        else
                        {
                            //读取标准的命令参数
                            ICommandParameter commandParameter = CommandParameterFactory(i);
                            if (commandParameter != null)
                            {
                                commandParameter.ToValue(cmd);
                                jT808_0x8105.CommandParameters.Add(commandParameter);
                            }
                        }
                    }
                }
            }
            return jT808_0x8105;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8105 value, IJT808Config config)
        {
            writer.WriteByte(value.CommandWord);
            if (value.CommandWord == 1 || value.CommandWord == 2)
            {
                if (CommandParameters != null && CommandParameters.Count > 0)
                {
                    //由于标准的命令参数是有顺序的，所以先判断有几个标准的命令参数
                    for (int i = 0; i < CommandParameterCount; i++)
                    {
                        var cmd = CommandParameters.FirstOrDefault(f => f.Order == i);
                        if (cmd != null)
                        {
                            writer.WriteArray(cmd.ToBytes());
                            writer.WriteChar(CommandParameterSeparator);
                        }
                        else
                        {
                            writer.WriteChar(CommandParameterSeparator);
                        }
                    }
                }
                if (CustomCommandParameters != null && CustomCommandParameters.Count > 0)
                {
                    //自定义命令参数扩展
                    foreach (var cmd in CustomCommandParameters.OrderBy(o => o.Order))
                    {
                        var bytes = cmd.ToBytes();
                        if (bytes != null && bytes.Length > 0)
                        {
                            writer.WriteArray(bytes);
                            writer.WriteChar(CommandParameterSeparator);
                        }
                        else
                        {
                            writer.WriteChar(CommandParameterSeparator);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8105 jT808_0x8105 = new JT808_0x8105
            {
                CommandWord = reader.ReadByte(),
                CommandParameters = new List<ICommandParameter>(),
                CustomCommandParameters = new List<ICommandParameter>(),
                UnknownCommandParameters = new Dictionary<int, byte[]>()
            };
            writer.WriteNumber($"[{ jT808_0x8105.CommandWord.ReadNumber()}]命令字", jT808_0x8105.CommandWord);
            if (jT808_0x8105.CommandWord == 1 || jT808_0x8105.CommandWord == 2)
            {
                int remain = reader.ReadCurrentRemainContentLength();
                if (remain > 0)
                {
                    var commandValueBuffer = reader.ReadArray(remain);
                    List<byte[]> commandParameters = new List<byte[]>();
                    while (true)
                    {
                        var index = commandValueBuffer.IndexOf(CommandParameterSeparatorValue);
                        if (index <= 0) break;
                        if (index == 1)
                        {
                            commandParameters.Add(null);
                        }
                        else
                        {
                            var value = commandValueBuffer.Slice(0, index);
                            commandParameters.Add(value.ToArray());
                        }
                        commandValueBuffer = commandValueBuffer.Slice(index);
                    }
                    writer.WriteStartObject("命令参数对象");
                    for (int i = 0; i < commandParameters.Count; i++)
                    {
                        //如果大于13个命令参数，说明有自定义命令参数再里面
                        var cmd = commandParameters[i];
                        if (i >= 13)
                        {
                            //读取自定义的命令参数
                            if (config.JT808_0x8105_Cusotm_Factory.Map.TryGetValue(i, out Type type))
                            {
                                var commandParameter = (ICommandParameter)Activator.CreateInstance(type);
                                commandParameter.ToValue(cmd);
                                writer.WriteString($"[{cmd.ToHexString()}]{commandParameter.CommandName}", commandParameter.ToDescription());
                            }
                            else
                            {
                                writer.WriteString($"[{cmd.ToHexString()}]未知命令参数{i}","");
                            }
                        }
                        else
                        {
                            //读取标准的命令参数
                            ICommandParameter commandParameter = CommandParameterFactory(i);
                            if (commandParameter != null)
                            {
                                commandParameter.ToValue(cmd);
                                writer.WriteString($"[{cmd.ToHexString()}]{commandParameter.CommandName}", commandParameter.ToDescription());
                            }
                        }
                    }
                    writer.WriteEndObject();
                }
            }


            //if (value.CommandWord == 1 || value.CommandWord == 2)
            //{
            //    value.CommandValue = new CommandParams();
            //    var commandValueBuffer = reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength()).ToArray();
            //    string commandValue=reader.ReadRemainStringContent();
            //    writer.WriteString($"[{ commandValueBuffer.ToHexString()}]命令参数", commandValue);
            //    writer.WriteStartObject("命令参数对象");
            //    var values = commandValue.Split(';');
            //    if (!string.IsNullOrEmpty(values[0]))
            //    {
            //        var connectionControl = byte.Parse(values[0]);
            //        writer.WriteNumber("连接控制", connectionControl);
            //    }
            //    else
            //    {
            //        writer.WriteNull("连接控制");
            //    }
            //    if (!string.IsNullOrEmpty(values[1]))
            //    {
            //        var dialPointName = values[1];
            //        writer.WriteString("拨号点名称", dialPointName);
            //    }
            //    else
            //    {
            //        writer.WriteNull("拨号点名称");
            //    }
            //    if (!string.IsNullOrEmpty(values[2]))
            //    {
            //        var dialUserName = values[2];
            //        writer.WriteString("拨号用户名", dialUserName);
            //    }
            //    else
            //    {
            //        writer.WriteNull("拨号用户名");
            //    }
            //    if (!string.IsNullOrEmpty(values[3]))
            //    {
            //        var dialPwd = values[3];
            //        writer.WriteString("拨号密码", dialPwd);
            //    }
            //    else
            //    {
            //        writer.WriteNull("拨号密码");
            //    }
            //    if (!string.IsNullOrEmpty(values[4]))
            //    {
            //        var serverUrl = values[4];
            //        writer.WriteString("服务器地址", serverUrl);
            //    }
            //    else
            //    {
            //        writer.WriteNull("服务器地址");
            //    }
            //    if (!string.IsNullOrEmpty(values[5]))
            //    {
            //        var tcpPort = ushort.Parse(values[5]);
            //        writer.WriteNumber("TCP端口", tcpPort);
            //    }
            //    else
            //    {
            //        writer.WriteNull("TCP端口");
            //    }
            //    if (!string.IsNullOrEmpty(values[6]))
            //    {
            //        var udpPort = ushort.Parse(values[6]);
            //        writer.WriteNumber("UDP端口", udpPort);
            //    }
            //    else
            //    {
            //        writer.WriteNull("UDP端口");
            //    }
            //    if (!string.IsNullOrEmpty(values[7]))
            //    {
            //        var manufacturerCode = long.Parse(values[7]);
            //        writer.WriteNumber("制造商ID", manufacturerCode);
            //    }
            //    else
            //    {
            //        writer.WriteNull("制造商ID");
            //    }
            //    if (!string.IsNullOrEmpty(values[8]))
            //    {
            //        var monitoringPlatformAuthenticationCode = values[8];
            //        writer.WriteString("监管平台鉴权码", monitoringPlatformAuthenticationCode);
            //    }
            //    else
            //    {
            //        writer.WriteNull("监管平台鉴权码");
            //    }
            //    if (!string.IsNullOrEmpty(values[9]))
            //    {
            //        var hardwareVersion = values[9];
            //        writer.WriteString("硬件版本号", hardwareVersion);
            //    }
            //    else
            //    {
            //        writer.WriteNull("硬件版本号");
            //    }
            //    if (!string.IsNullOrEmpty(values[10]))
            //    {
            //       var firmwareVersion = values[10];
            //       writer.WriteString("固件版本号", firmwareVersion);
            //    }
            //    else
            //    {
            //        writer.WriteNull("固件版本号");
            //    }
            //    if (!string.IsNullOrEmpty(values[11]))
            //    {
            //        var url = values[11];
            //        writer.WriteString("URL地址", url);
            //    }
            //    else
            //    {
            //        writer.WriteNull("URL地址");
            //    }
            //    if (!string.IsNullOrEmpty(values[12]))
            //    {
            //        var connectTimeLimit = ushort.Parse(values[12]);
            //        writer.WriteNumber("连接到指定服务器时限", connectTimeLimit);
            //    }
            //    else
            //    {
            //        writer.WriteNull("连接到指定服务器时限");
            //    }
            //    writer.WriteEndObject();
            //}
        }

        private ICommandParameter CommandParameterFactory(in int order)
        {
            ICommandParameter commandParameter=default;
            switch (order)
            {
                case 0:
                    commandParameter = new ConnectionControlCommandParameter();
                    break;
                case 1:
                    commandParameter = new DialPointNameCommandParameter();
                    break;
                case 2:
                    commandParameter = new DialUserNameCommandParameter();
                    break;
                case 3:
                    commandParameter = new DialPwdCommandParameter();
                    break;
                case 4:
                    commandParameter = new ServerUrlCommandParameter();
                    break;
                case 5:
                    commandParameter = new TcpPortCommandParameter();
                    break;
                case 6:
                    commandParameter = new UdpPortCommandParameter();
                    break;
                case 7:
                    commandParameter = new MakerIdCommandParameter();
                    break;
                case 8:
                    commandParameter = new MonitorPlatformAuthCodeCommandParameter();
                    break;
                case 9:
                    commandParameter = new HardwareVersionCommandParameter();
                    break;
                case 10:
                    commandParameter = new FirmwareVersionCommandParameter();
                    break;
                case 11:
                    commandParameter = new UrlCommandParameter();
                    break;
                case 12:
                    commandParameter = new ConnectTimeLimitCommandParameter();
                    break;
            }
            return commandParameter;
        }

        #region 命令参数
        /// <summary>
        /// 命令参数接口
        /// </summary>
        public interface ICommandParameter
        {
            /// <summary>
            /// 排序
            /// </summary>
            int Order { get; }
            /// <summary>
            /// 命令名称
            /// </summary>
            string CommandName { get; }
            /// <summary>
            /// 转为byte数组
            /// </summary>
            /// <returns></returns>
            byte[] ToBytes();
            /// <summary>
            /// 将byte数组转为命令值
            /// </summary>
            /// <param name="bytes"></param>
            void ToValue(byte[] bytes);
            /// <summary>
            /// 将命令值转为描述
            /// </summary>
            string ToDescription();
        }
        /// <summary>
        /// 自定义命令参数接口
        /// </summary>
        public interface ICusotmCommandParameter: ICommandParameter
        {
           
        }
        /// <summary>
        /// 连接控制
        /// 0：切换到指定监管平台服务器，连接到该服务器后即进入应急状态，此状态下仅有下发控制指令的监管平台可发送包括短信在内的控制指令；
        /// 1：切换回原缺省监控平台服务器，并恢复正常状态。
        /// </summary>
        public class ConnectionControlCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 0
            /// </summary>
            public int Order { get; } = 0;
            /// <summary>
            /// 连接控制
            /// </summary>
            public string CommandName { get; } = "连接控制";
            /// <summary>
            /// 连接控制
            /// 0：切换到指定监管平台服务器，连接到该服务器后即进入应急状态，此状态下仅有下发控制指令的监管平台可发送包括短信在内的控制指令；
            /// 1：切换回原缺省监控平台服务器，并恢复正常状态。
            /// </summary>
            public byte? ConnectionControl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (ConnectionControl.HasValue) return default;
                return new byte[1] { ConnectionControl.Value };
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    ConnectionControl = bytes[0];
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{(ConnectionControl.HasValue ? "" : ConnectionControl.Value)}";
            }
        }
        /// <summary>
        /// 拨号点名称
        /// </summary>
        public class DialPointNameCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 1
            /// </summary>
            public int Order { get; } = 1;
            /// <summary>
            /// 拨号点名称
            /// </summary>
            public string CommandName { get; } = "拨号点名称";
            /// <summary>
            /// 拨号点名称
            /// 一般为服务器 APN，无线通信拨号访问点，若网络制式为 CDMA，则该值为 PPP 连接拨号号码
            /// </summary>
            public string DialPointName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(DialPointName)) return default;
                return JT808Constants.Encoding.GetBytes(DialPointName);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    DialPointName = JT808Constants.Encoding.GetString(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{DialPointName ?? ""}";
            }
        }
        /// <summary>
        /// 拨号用户名
        /// 服务器无线通信拨号用户名
        /// </summary>
        public class DialUserNameCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 2
            /// </summary>
            public int Order { get; } = 2;
            /// <summary>
            /// 拨号用户名
            /// </summary>
            public string CommandName { get; } = "拨号用户名";
            /// <summary>
            /// 拨号用户名
            /// 服务器无线通信拨号用户名
            /// </summary>
            public string DialUserName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(DialUserName)) return default;
                return JT808Constants.Encoding.GetBytes(DialUserName);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    DialUserName = JT808Constants.Encoding.GetString(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{DialUserName ?? ""}";
            }
        }
        /// <summary>
        /// 拨号密码
        /// 服务器无线通信拨号密码
        /// </summary>
        public class DialPwdCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 3
            /// </summary>
            public int Order { get; } = 3;
            /// <summary>
            /// 拨号密码
            /// </summary>
            public string CommandName { get; } = "拨号密码";
            /// <summary>
            /// 拨号密码
            /// 服务器无线通信拨号密码
            /// </summary>
            public string DialPwd { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(DialPwd)) return default;
                return JT808Constants.Encoding.GetBytes(DialPwd);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    DialPwd = JT808Constants.Encoding.GetString(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{DialPwd ?? ""}";
            }
        }
        /// <summary>
        /// 服务器地址;IP 或域名
        /// </summary>
        public class ServerUrlCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 4
            /// </summary>
            public int Order { get; } = 4;
            /// <summary>
            /// 服务器地址
            /// </summary>
            public string CommandName { get; } = "服务器地址";
            /// <summary>
            /// 服务器地址 IP或域名
            /// </summary>
            public string ServerUrl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(ServerUrl)) return default;
                return JT808Constants.Encoding.GetBytes(ServerUrl);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    ServerUrl = JT808Constants.Encoding.GetString(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{ServerUrl ?? ""}";
            }
        }
        /// <summary>
        /// Tcp端口
        /// </summary>
        public class TcpPortCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 5
            /// </summary>
            public int Order { get; } = 5;
            /// <summary>
            /// 连接控制
            /// </summary>
            public string CommandName { get; } = "Tcp端口";
            /// <summary>
            /// Tcp端口
            /// </summary>
            public ushort? TcpPort { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (TcpPort.HasValue) return default;
                var value = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(value, TcpPort.Value);
                return value;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    TcpPort = BinaryPrimitives.ReadUInt16BigEndian(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{(TcpPort.HasValue ? "" : TcpPort.Value)}";
            }
        }
        /// <summary>
        /// Udp端口
        /// </summary>
        public class UdpPortCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 6
            /// </summary>
            public int Order { get; } = 6;
            /// <summary>
            /// 连接控制
            /// </summary>
            public string CommandName { get; } = "Udp端口";
            /// <summary>
            /// Udp端口
            /// </summary>
            public ushort? UdpPort { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (UdpPort.HasValue) return default;
                var value = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(value, UdpPort.Value);
                return value;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    UdpPort = BinaryPrimitives.ReadUInt16BigEndian(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{(UdpPort.HasValue ? "" : UdpPort.Value)}";
            }
        }
        /// <summary>
        /// 制造商ID
        /// </summary>
        public class MakerIdCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 7
            /// </summary>
            public int Order { get; } = 7;
            /// <summary>
            /// 服务器地址
            /// </summary>
            public string CommandName { get; } = "制造商ID";
            /// <summary>
            /// 制造商ID
            /// </summary>
            public string MakerId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(MakerId)) return default;
                return JT808Constants.Encoding.GetBytes(MakerId);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    MakerId = JT808Constants.Encoding.GetString(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{MakerId ?? ""}";
            }
        }
        /// <summary>
        /// 监管平台鉴权码
        /// 监管平台下发的鉴权码，仅用于终端连接到监管平台之后的鉴权，终端连接回原监控平台还用原鉴权码
        /// </summary>
        public class MonitorPlatformAuthCodeCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 8
            /// </summary>
            public int Order { get; } = 8;
            /// <summary>
            /// 监管平台鉴权码
            /// </summary>
            public string CommandName { get; } = "监管平台鉴权码";
            /// <summary>
            /// 监管平台鉴权码
            /// </summary>
            public string MonitorPlatformAuthCode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(MonitorPlatformAuthCode)) return default;
                return JT808Constants.Encoding.GetBytes(MonitorPlatformAuthCode);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    MonitorPlatformAuthCode = JT808Constants.Encoding.GetString(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{MonitorPlatformAuthCode ?? ""}";
            }
        }
        /// <summary>
        /// 硬件版本
        /// 终端的硬件版本号，由制造商自定
        /// </summary>
        public class HardwareVersionCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 9
            /// </summary>
            public int Order { get; } = 9;
            /// <summary>
            /// 硬件版本
            /// </summary>
            public string CommandName { get; } = "硬件版本";
            /// <summary>
            /// 硬件版本
            /// 终端的硬件版本号，由制造商自定
            /// </summary>
            public string HardwareVersion { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(HardwareVersion)) return default;
                return JT808Constants.Encoding.GetBytes(HardwareVersion);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    HardwareVersion = JT808Constants.Encoding.GetString(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{HardwareVersion ?? ""}";
            }
        }
        /// <summary>
        /// 固件版本
        /// 终端的固件版本号，由制造商自定
        /// </summary>
        public class FirmwareVersionCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 10
            /// </summary>
            public int Order { get; } = 10;
            /// <summary>
            /// 固件版本
            /// </summary>
            public string CommandName { get; } = "固件版本";
            /// <summary>
            /// 固件版本
            /// 终端的硬件版本号，由制造商自定
            /// </summary>
            public string FirmwareVersion { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(FirmwareVersion)) return default;
                return JT808Constants.Encoding.GetBytes(FirmwareVersion);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    FirmwareVersion = JT808Constants.Encoding.GetString(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{FirmwareVersion ?? ""}";
            }
        }
        /// <summary>
        /// URL地址完整URL地址
        /// </summary>
        public class UrlCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 11
            /// </summary>
            public int Order { get; } = 11;
            /// <summary>
            /// URL地址完整URL地址
            /// </summary>
            public string CommandName { get; } = "URL地址完整URL地址";
            /// <summary>
            /// URL地址完整URL地址
            /// </summary>
            public string Url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Url)) return default;
                return JT808Constants.Encoding.GetBytes(Url);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Url = JT808Constants.Encoding.GetString(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{Url ?? ""}";
            }
        }
        /// <summary>
        /// 连接到指定服务器时限
        /// </summary>
        public class ConnectTimeLimitCommandParameter : ICommandParameter
        {
            /// <summary>
            /// 排序 12
            /// </summary>
            public int Order { get; } = 12;
            /// <summary>
            /// 连接控制
            /// </summary>
            public string CommandName { get; } = "连接到指定服务器时限";
            /// <summary>
            /// 连接到指定服务器时限
            /// 单位：分（min），值非 0 后的有效期截止前，终端应连回原地址。
            /// 若值为 0，则表示一直连接指 定服务器
            /// </summary>
            public ushort? ConnectTimeLimit { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (ConnectTimeLimit.HasValue) return default;
                var value = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(value, ConnectTimeLimit.Value);
                return value;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    ConnectTimeLimit = BinaryPrimitives.ReadUInt16BigEndian(bytes);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToDescription()
            {
                return $"{CommandName}:{(ConnectTimeLimit.HasValue ? "" : ConnectTimeLimit.Value)}";
            }
        }
        #endregion
    }
}
