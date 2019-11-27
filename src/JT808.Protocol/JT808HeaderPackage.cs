using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808头部数据包
    /// </summary>
    public class JT808HeaderPackage: IJT808MessagePackFormatter<JT808HeaderPackage>
    {
        /// <summary>
        /// 起始符
        /// </summary>
        public byte Begin { get; set; }
        /// <summary>
        /// 头数据
        /// </summary>
        public JT808Header Header { get;  set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] Bodies { get; set; }
        /// <summary>
        /// 校验码
        /// 从消息头开始，同后一字节异或，直到校验码前一个字节，占用一个字节。
        /// </summary>
        public byte CheckCode { get; set; }
        /// <summary>
        /// 终止符
        /// </summary>
        public byte End { get; set; }

        public JT808Version Version 
        { 
            get {
                if (Header != null)
                {
                    try
                    {
                        if (Header.MessageBodyProperty.VersionFlag)
                        {
                            return JT808Version.JTT2019;
                        }
                        else
                        {
                            return JT808Version.JTT2013;
                        }
                    }
                    catch
                    {
                        return JT808Version.JTT2013;
                    }
                }
                else
                {
                    return JT808Version.JTT2013;
                }
            } 
        }

        public JT808HeaderPackage Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            // 1. 验证校验和
            if (!config.SkipCRCCode)
            {
                if (!reader.CheckXorCodeVali)
                {
                    throw new JT808Exception(JT808ErrorCode.CheckCodeNotEqual, $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
                }
            }
            JT808HeaderPackage jT808Package = new JT808HeaderPackage();
            // ---------------开始解包--------------
            // 2.读取起始位置        
            jT808Package.Begin = reader.ReadStart();
            // 3.读取头部信息
            jT808Package.Header = new JT808Header();
            //  3.1.读取消息Id
            jT808Package.Header.MsgId = reader.ReadUInt16();
            //  3.2.读取消息体属性
            ushort messageBodyPropertyValue = reader.ReadUInt16();
            //    3.2.1.解包消息体属性
            jT808Package.Header.MessageBodyProperty = new JT808HeaderMessageBodyProperty(messageBodyPropertyValue);
            if (jT808Package.Header.MessageBodyProperty.VersionFlag)
            {
                //2019版本
                //  3.3.读取协议版本号 
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
            // 3.4.读取消息流水号
            jT808Package.Header.MsgNum = reader.ReadUInt16();
            // 3.5.判断有无分包
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
                jT808Package.Bodies = reader.ReadContent().ToArray();
            }
            // 5.读取校验码
            jT808Package.CheckCode = reader.ReadByte();
            // 6.读取终止位置
            jT808Package.End = reader.ReadEnd();
            // ---------------解包完成--------------
            return jT808Package;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808HeaderPackage value, IJT808Config config)
        {
            throw new NotImplementedException("只适用反序列化");
        }
    }
}
