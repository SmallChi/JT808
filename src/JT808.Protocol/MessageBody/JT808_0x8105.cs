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
    public class JT808_0x8105 : JT808MessagePackFormatter<JT808_0x8105>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
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
        public ushort MsgId  => 0x8105;
        /// <summary>
        /// 终端控制
        /// </summary>
        public string Description => "终端控制";
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
        public override JT808_0x8105 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
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
                        if (commandValueBuffer.Length <= 0) break;
                        if (index == 0)
                        {
                            commandParameters.Add(null);
                            commandValueBuffer = commandValueBuffer.Slice(index+1);
                        }
                        else
                        {
                            var value = commandValueBuffer.Slice(0, index);
                            commandParameters.Add(value.ToArray());
                            commandValueBuffer = commandValueBuffer.Slice(index+1);
                        }
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
                                jT808_0x8105.UnknownCommandParameters.Add(i, cmd);
                            }
                        }
                        else
                        {
                            //读取标准的命令参数
                            ICommandParameter commandParameter = JT808_0x8105_CommandParameterExtensions.CreateCommandParameter(i);
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
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8105 value, IJT808Config config)
        {
            writer.WriteByte(value.CommandWord);
            if (value.CommandWord == 1 || value.CommandWord == 2)
            {
                if (value.CommandParameters != null && value.CommandParameters.Count > 0)
                {
                    //由于标准的命令参数是有顺序的，所以先判断有几个标准的命令参数
                    for (int i = 0; i < CommandParameterCount; i++)
                    {
                        var cmd = value.CommandParameters.FirstOrDefault(f => f.Order == i);
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
                else
                {
                    for (int i = 0; i < CommandParameterCount; i++)
                    {
                        writer.WriteChar(CommandParameterSeparator);
                    }
                }
                if (value.CustomCommandParameters != null && value.CustomCommandParameters.Count > 0)
                {
                    //自定义命令参数扩展
                    foreach (var cmd in value.CustomCommandParameters.OrderBy(o => o.Order))
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
                        if (commandValueBuffer.Length <= 0) break;
                        if (index == 0)
                        {
                            commandParameters.Add(null);
                            commandValueBuffer = commandValueBuffer.Slice(index + 1);
                        }
                        else
                        {
                            var value = commandValueBuffer.Slice(0, index);
                            commandParameters.Add(value.ToArray());
                            commandValueBuffer = commandValueBuffer.Slice(index + 1);
                        }
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
                                writer.WriteString($"[{cmd.ToHexString()}]{commandParameter.CommandName}", commandParameter.ToValueString());
                            }
                            else
                            {
                                writer.WriteString($"[{cmd.ToHexString()}]未知命令参数{i}","");
                            }
                        }
                        else
                        {
                            //读取标准的命令参数
                            ICommandParameter commandParameter = JT808_0x8105_CommandParameterExtensions.CreateCommandParameter(i);
                            if (commandParameter != null)
                            {
                                commandParameter.ToValue(cmd);
                                writer.WriteString($"[{cmd?.ToHexString()}]{commandParameter.CommandName}", commandParameter.ToValueString());
                            }
                        }
                    }
                    writer.WriteEndObject();
                }
            }
        }

        #region 命令参数
        /// <summary>
        /// 命令参数接口
        /// </summary>
        public interface ICommandParameter:ICommandParameterConvert
        {
            /// <summary>
            /// 排序
            /// </summary>
            int Order { get; }
            /// <summary>
            /// 命令名称
            /// </summary>
            string CommandName { get; }

        }
        /// <summary>
        /// 命令参数值的转换
        /// </summary>
        public interface ICommandParameterConvert
        {
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
        }
        /// <summary>
        /// 命令参数值
        /// </summary>
        public interface ICommandParameterValue<TValue>
        {
            /// <summary>
            /// 对应参数值
            /// </summary>
            TValue Value { get; set; }
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
        public class ConnectionControlCommandParameter : ICommandParameter, ICommandParameterValue<byte?>
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
            public byte? Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (!Value.HasValue) return default;
                return new byte[1] { Value.Value };
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = bytes[0];
                }
            }
        }
        /// <summary>
        /// 拨号点名称
        /// </summary>
        public class DialPointNameCommandParameter : ICommandParameter, ICommandParameterValue<string>
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
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes);
                }
            }
        }
        /// <summary>
        /// 拨号用户名
        /// 服务器无线通信拨号用户名
        /// </summary>
        public class DialUserNameCommandParameter : ICommandParameter, ICommandParameterValue<string>
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
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes);
                }
            }
        }
        /// <summary>
        /// 拨号密码
        /// 服务器无线通信拨号密码
        /// </summary>
        public class DialPwdCommandParameter : ICommandParameter, ICommandParameterValue<string>
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
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes);
                }
            }
        }
        /// <summary>
        /// 服务器地址;IP 或域名
        /// </summary>
        public class ServerUrlCommandParameter : ICommandParameter, ICommandParameterValue<string>
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
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes);
                }
            }
        }
        /// <summary>
        /// Tcp端口
        /// </summary>
        public class TcpPortCommandParameter : ICommandParameter, ICommandParameterValue<ushort?>
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
            public ushort? Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (!Value.HasValue) return default;
                var value = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(value, Value.Value);
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
                    Value = BinaryPrimitives.ReadUInt16BigEndian(bytes);
                }
            }
        }
        /// <summary>
        /// Udp端口
        /// </summary>
        public class UdpPortCommandParameter : ICommandParameter, ICommandParameterValue<ushort?>
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
            public ushort? Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (!Value.HasValue) return default;
                var value = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(value, Value.Value);
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
                    Value = BinaryPrimitives.ReadUInt16BigEndian(bytes);
                }
            }
        }
        /// <summary>
        /// 制造商ID
        /// </summary>
        public class MakerIdCommandParameter : ICommandParameter, ICommandParameterValue<string>
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
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value.PadRight(5, '\0'));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes).Trim('\0');
                }
            }
        }
        /// <summary>
        /// 监管平台鉴权码
        /// 监管平台下发的鉴权码，仅用于终端连接到监管平台之后的鉴权，终端连接回原监控平台还用原鉴权码
        /// </summary>
        public class MonitorPlatformAuthCodeCommandParameter : ICommandParameter, ICommandParameterValue<string>
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
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes);
                }
            }
        }
        /// <summary>
        /// 硬件版本
        /// 终端的硬件版本号，由制造商自定
        /// </summary>
        public class HardwareVersionCommandParameter : ICommandParameter, ICommandParameterValue<string>
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
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes);
                }
            }
        }
        /// <summary>
        /// 固件版本
        /// 终端的固件版本号，由制造商自定
        /// </summary>
        public class FirmwareVersionCommandParameter : ICommandParameter, ICommandParameterValue<string>
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
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes);
                }
            }
        }
        /// <summary>
        /// URL的完整地址
        /// </summary>
        public class UrlCommandParameter : ICommandParameter, ICommandParameterValue<string>
        {
            /// <summary>
            /// 排序 11
            /// </summary>
            public int Order { get; } = 11;
            /// <summary>
            /// URL的完整地址
            /// </summary>
            public string CommandName { get; } = "URL的完整地址";
            /// <summary>
            /// URL的完整地址
            /// </summary>
            public string Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (string.IsNullOrEmpty(Value)) return default;
                return JT808Constants.Encoding.GetBytes(Value);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="bytes"></param>
            public void ToValue(byte[] bytes)
            {
                if (bytes != null && bytes.Length > 0)
                {
                    Value = JT808Constants.Encoding.GetString(bytes);
                }
            }
        }
        /// <summary>
        /// 连接到指定服务器时限
        /// </summary>
        public class ConnectTimeLimitCommandParameter : ICommandParameter, ICommandParameterValue<ushort?>
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
            public ushort? Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public byte[] ToBytes()
            {
                if (!Value.HasValue) return default;
                var value = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(value, Value.Value);
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
                    Value = BinaryPrimitives.ReadUInt16BigEndian(bytes);
                }
            }
        }
        #endregion
    }
}
