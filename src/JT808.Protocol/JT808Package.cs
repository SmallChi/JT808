using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808数据包
    /// </summary>
    public class JT808Package : JT808MessagePackFormatter<JT808Package>, IJT808Analyze
    {
        /// <summary>
        /// 起始符
        /// </summary>
        public const byte BeginFlag = 0x7e;
        /// <summary>
        /// 终止符
        /// </summary>
        public const byte EndFlag = 0x7e;
        /// <summary>
        /// 起始符
        /// </summary>
        public byte Begin { get; set; } = BeginFlag;
        /// <summary>
        /// 头数据
        /// </summary>
        public JT808Header Header { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public JT808Bodies Bodies { get; set; }
        /// <summary>
        /// 分包数据体
        /// </summary>
        public byte[] SubDataBodies { get; set; }
        /// <summary>
        /// 校验码
        /// 从消息头开始，同后一字节异或，直到校验码前一个字节，占用一个字节。
        /// </summary>
        public byte CheckCode { get; set; }
        /// <summary>
        /// 终止符
        /// </summary>
        public byte End { get; set; } = EndFlag;
        /// <summary>
        /// 808版本号
        /// </summary>
        public JT808Version Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808Package Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            // 1. 验证校验和
            if (!config.SkipCRCCode)
            {
                if (!reader.CheckXorCodeVali)
                {
                    throw new JT808Exception(JT808ErrorCode.CheckCodeNotEqual, $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
                }
            }
            JT808Package jT808Package = new JT808Package();
            // ---------------开始解包--------------
            // 2.读取起始位置        
            jT808Package.Begin = reader.ReadStart();
            // 3.读取头部信息
            jT808Package.Header = new JT808Header();
            //  3.1.读取消息Id
            jT808Package.Header.MsgId = reader.ReadUInt16();
            //  3.2.读取消息体属性
            jT808Package.Header.MessageBodyProperty = new JT808HeaderMessageBodyProperty(reader.ReadUInt16());
            if (reader.Version == JT808Version.JTT2013Force)
            {
                jT808Package.Header.TerminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength, config.Trim);
                reader.Version = JT808Version.JTT2013;
            }
            else {
                if (reader.Version == JT808Version.JTT2019 || jT808Package.Header.MessageBodyProperty.VersionFlag)
                {
                    //2019版本
                    jT808Package.Header.ProtocolVersion = reader.ReadByte();
                    //  3.4.读取终端手机号 
                    jT808Package.Header.TerminalPhoneNo = reader.ReadBCD(20, config.Trim);
                    reader.Version = JT808Version.JTT2019;
                }
                else
                {
                    //2013版本
                    //  3.3.读取终端手机号 
                    jT808Package.Header.TerminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength, config.Trim);
                }
            }
            jT808Package.Version = reader.Version;
            //  3.4.读取消息流水号
            jT808Package.Header.MsgNum = reader.ReadUInt16();
            //  3.5.判断有无分包
            if (jT808Package.Header.MessageBodyProperty.IsPackage)
            {
                //3.5.1.读取消息包总数
                jT808Package.Header.PackgeCount = reader.ReadUInt16();
                //3.5.2.读取消息包序号
                jT808Package.Header.PackageIndex = reader.ReadUInt16();
            }
            // 4.处理数据体
            //  4.1.判断有无数据体
            if (jT808Package.Header.MessageBodyProperty.DataLength > 0)
            {
                if (config.MsgIdFactory.TryGetValue(jT808Package.Header.MsgId, out object instance))
                {
                    if (jT808Package.Header.MessageBodyProperty.IsPackage)
                    {
                        //读取分包的数据体
                        try
                        {
                            jT808Package.SubDataBodies = reader.ReadArray(jT808Package.Header.MessageBodyProperty.DataLength).ToArray();
                        }
                        catch (Exception ex)
                        {
                            throw new JT808Exception(JT808ErrorCode.BodiesParseError, ex);
                        }
                    }
                    else
                    {
                        try
                        {
                            //4.2.处理消息体
                            jT808Package.Bodies = instance.DeserializeExt<JT808Bodies>(ref reader, config);
                        }
                        catch (Exception ex)
                        {
                            throw new JT808Exception(JT808ErrorCode.BodiesParseError, ex);
                        }
                    }
                }
            }
            // 5.读取校验码
            jT808Package.CheckCode = reader.ReadByte();
            // 6.读取终止位置
            jT808Package.End = reader.ReadEnd();
            // ---------------解包完成--------------
            return jT808Package;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808Package value, IJT808Config config)
        {
            // ---------------开始组包--------------
            // 1.起始符
            writer.WriteStart();
            // 2.写入头部 //部分有带数据体的长度，那么先跳过写入头部部分
            //  2.1.消息ID
            writer.WriteUInt16(value.Header.MsgId);
            //  2.2.消息体属性(包含消息体长度所以先跳过)
            writer.Skip(2, out int msgBodiesPropertyPosition);
            if (writer.Version == JT808Version.JTT2019 || value.Header.MessageBodyProperty.VersionFlag)
            {
                //2019版本
                //  2.3.协议版本号
                writer.WriteByte(value.Header.ProtocolVersion);
                //  2.4.终端手机号
                writer.WriteBCD(value.Header.TerminalPhoneNo, 20);
                writer.Version = JT808Version.JTT2019;
                value.Header.MessageBodyProperty.VersionFlag = true;
            }
            else
            {
                //2013版本
                //  2.3.终端手机号 (写死大陆手机号码)
                writer.WriteBCD(value.Header.TerminalPhoneNo, config.TerminalPhoneNoLength);
            }
            if (value.Header.ManualMsgNum.HasValue)
            {
                //  2.4.消息流水号
                writer.WriteUInt16(value.Header.ManualMsgNum.Value);
            }
            else
            {
                //  2.4.消息流水号
                value.Header.MsgNum = config.MsgSNDistributed.Increment(value.Header.TerminalPhoneNo);
                writer.WriteUInt16(value.Header.MsgNum);
            }
            //  2.5.判断是否分包
            if (value.Header.MessageBodyProperty.IsPackage)
            {
                // 2.5.1.消息包总数
                writer.WriteUInt16(value.Header.PackgeCount);
                // 2.5.2.消息包序号
                writer.WriteUInt16(value.Header.PackageIndex);
            }
            int headerLength = writer.GetCurrentPosition();
            if (value.Header.MessageBodyProperty.IsPackage)
            {
                if (value.SubDataBodies != null)
                {
                    //2.5.3.写入分包数据
                    writer.WriteArray(value.SubDataBodies);
                }
            }
            else
            {
                // 3.处理数据体部分
                if (value.Bodies != null)
                {
                    if (!value.Bodies.SkipSerialization)
                    {
                        value.Bodies.SerializeExt(ref writer, value.Bodies, config);
                    }
                }
            }
            //  3.1.处理数据体长度
            // 2.2.回写消息体属性
            value.Header.MessageBodyProperty.DataLength = (writer.GetCurrentPosition() - headerLength);
            writer.WriteUInt16Return(value.Header.MessageBodyProperty.Wrap(), msgBodiesPropertyPosition);
            // 4.校验码
            writer.WriteXor();
            // 5.终止符
            writer.WriteEnd();
            // 6.编码
            writer.WriteEncode();
            // ---------------组包结束--------------
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            // ---------------开始解析对象--------------
            writer.WriteStartObject();
            // 1. 验证校验和
            if (!reader.CheckXorCodeVali)
            {
                writer.WriteString("检验和错误", $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
            }
            // 2.读取起始位置
            byte start = reader.ReadEnd();
            writer.WriteNumber($"[{start.ReadNumber()}]开始", start);
            var msgid = reader.ReadUInt16();
            writer.WriteNumber($"[{msgid.ReadNumber()}]消息Id", msgid);
            ushort messageBodyPropertyValue = reader.ReadUInt16();
            var headerMessageBodyProperty = new JT808HeaderMessageBodyProperty(messageBodyPropertyValue);
            //消息体属性对象 开始
            writer.WriteStartObject("消息体属性对象");
            ReadOnlySpan<char> messageBodyPropertyReadOnlySpan = messageBodyPropertyValue.ReadBinary();
            writer.WriteNumber($"[{messageBodyPropertyReadOnlySpan.ToString()}]消息体属性", messageBodyPropertyValue);
            if (reader.Version == JT808Version.JTT2013Force)
            {
                reader.Version = JT808Version.JTT2013;
                writer.WriteString("版本号", JT808Version.JTT2013.ToString());
                writer.WriteNumber("[bit15]保留", 0);
                writer.WriteNumber("[bit14]保留", 0);
                writer.WriteBoolean("[bit13]是否分包", headerMessageBodyProperty.IsPackage);
                writer.WriteString("[bit10~bit12]数据加密", headerMessageBodyProperty.Encrypt.ToString());
                writer.WriteNumber("[bit0~bit9]消息体长度", headerMessageBodyProperty.DataLength);
                writer.WriteEndObject();
                //2013版本
                //  3.3.读取终端手机号 
                var terminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength, false);
                //消息体属性对象 结束
                writer.WriteString($"[{terminalPhoneNo.PadLeft(config.TerminalPhoneNoLength, '0')}]终端手机号", terminalPhoneNo);
            }
            else {
                if (reader.Version == JT808Version.JTT2019 || headerMessageBodyProperty.VersionFlag)
                {
                    reader.Version = JT808Version.JTT2019;
                    writer.WriteString("版本号", JT808Version.JTT2019.ToString());
                    writer.WriteNumber("[bit15]保留", 0);
                    writer.WriteBoolean("[bit14]协议版本标识", headerMessageBodyProperty.VersionFlag);
                    writer.WriteBoolean("[bit13]是否分包", headerMessageBodyProperty.IsPackage);
                    writer.WriteString("[bit10~bit12]数据加密", headerMessageBodyProperty.Encrypt.ToString());
                    writer.WriteNumber("[bit0~bit9]消息体长度", headerMessageBodyProperty.DataLength);
                    //消息体属性对象 结束
                    writer.WriteEndObject();
                    //2019版本
                    var protocolVersion = reader.ReadByte();
                    writer.WriteNumber($"[{protocolVersion.ReadNumber()}]协议版本号(2019)", protocolVersion);
                    //  3.4.读取终端手机号 
                    var terminalPhoneNo = reader.ReadBCD(20, config.Trim);
                    writer.WriteString($"[{terminalPhoneNo.PadLeft(20, '0')}]终端手机号", terminalPhoneNo);
                }
                else
                {
                    reader.Version = JT808Version.JTT2013;
                    writer.WriteString("版本号", JT808Version.JTT2013.ToString());
                    writer.WriteNumber("[bit15]保留", 0);
                    writer.WriteNumber("[bit14]保留", 0);
                    writer.WriteBoolean("[bit13]是否分包", headerMessageBodyProperty.IsPackage);
                    writer.WriteString("[bit10~bit12]数据加密", headerMessageBodyProperty.Encrypt.ToString());
                    writer.WriteNumber("[bit0~bit9]消息体长度", headerMessageBodyProperty.DataLength);
                    writer.WriteEndObject();
                    //2013版本
                    //  3.3.读取终端手机号 
                    var terminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength, false);
                    //消息体属性对象 结束
                    writer.WriteString($"[{terminalPhoneNo.PadLeft(config.TerminalPhoneNoLength, '0')}]终端手机号", terminalPhoneNo);
                }
            }
            //  3.4.读取消息流水号
            var msgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{msgNum.ReadNumber()}]消息流水号", msgNum);
            //  3.5.判断有无分包
            uint packgeCount = 0, packageIndex = 0;
            if (headerMessageBodyProperty.IsPackage)
            {
                //3.5.1.读取消息包总数
                packgeCount = reader.ReadUInt16();
                writer.WriteNumber($"[{packgeCount.ReadNumber()}]消息包总数", packgeCount);
                //3.5.2.读取消息包序号
                packageIndex = reader.ReadUInt16();
                writer.WriteNumber($"[{packageIndex.ReadNumber()}]消息包序号", packageIndex);
            }
            // 4.处理数据体
            //  4.1.判断有无数据体
            if (headerMessageBodyProperty.DataLength > 0)
            {
                //数据体属性对象 开始
                writer.WriteStartObject("数据体对象");
                string description = "数据体";
                if (headerMessageBodyProperty.IsPackage)
                {
                    //读取分包的数据体
                    try
                    {
                        writer.WriteString($"[分包]数据体", reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray().ToHexString());
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        writer.WriteString($"数据体解析异常,无可用数据体进行解析", ex.StackTrace);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        writer.WriteString($"[分包]数据体解析异常,无可用数据体进行解析", ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        writer.WriteString($"[分包]数据体异常", ex.StackTrace);
                    }
                }
                else
                {
                    if (config.MsgIdFactory.TryGetValue(msgid, out object instance))
                    {
                        if (instance is IJT808Description jT808Description)
                        {
                            //4.2.处理消息体
                            description = jT808Description.Description;
                        }
                        try
                        {
                            //数据体长度正常
                            writer.WriteString($"{description}", reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength()).ToArray().ToHexString());
                            if (instance is IJT808Analyze analyze)
                            {
                                //4.2.处理消息体
                                analyze.Analyze(ref reader, writer, config);
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            writer.WriteString($"数据体解析异常,无可用数据体进行解析", ex.StackTrace);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            writer.WriteString($"数据体解析异常,无可用数据体进行解析", ex.StackTrace);
                        }
                        catch (Exception ex)
                        {
                            writer.WriteString($"数据体异常", ex.StackTrace);
                        }
                    }
                    else
                    {
                        writer.WriteString($"[未知]数据体", reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray().ToHexString());
                    }
                }
                //数据体属性对象 结束
                writer.WriteEndObject();
            }
            else
            {
                if (config.MsgIdFactory.TryGetValue(msgid, out object instance))
                {
                    //数据体属性对象 开始
                    writer.WriteStartObject("数据体对象");
                    string description = "[Null]数据体";
                    if (instance is IJT808Description jT808Description)
                    {
                        //4.2.处理消息体
                        description = jT808Description.Description;
                    }
                    writer.WriteNull(description);
                    //数据体属性对象 结束
                    writer.WriteEndObject();
                }
                else
                {
                    writer.WriteNull($"[Null]数据体");
                }
            }
            try
            {
                // 5.读取校验码
                reader.ReadByte();
                writer.WriteNumber($"[{reader.RealCheckXorCode.ReadNumber()}]校验码", reader.RealCheckXorCode);
                // 6.读取终止位置
                byte end = reader.ReadEnd();
                writer.WriteNumber($"[{end.ReadNumber()}]结束", end);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                writer.WriteString($"数据解析异常,无可用数据进行解析", ex.StackTrace);
            }
            catch (Exception ex)
            {
                writer.WriteString($"数据解析异常", ex.StackTrace);
            }
            finally
            {
                writer.WriteEndObject();
            }
        }
    }
}
