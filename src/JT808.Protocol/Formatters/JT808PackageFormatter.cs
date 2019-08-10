using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Formatters
{
    /// <summary>
    /// JT808包序列化器
    /// </summary>
    public class JT808PackageFormatter : IJT808MessagePackFormatter<JT808Package>
    {
        public static readonly JT808PackageFormatter Instance = new JT808PackageFormatter();
        public JT808Package Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
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
            jT808Package.Header.MessageBodyProperty=new JT808HeaderMessageBodyProperty(reader.ReadUInt16());
            // 3.3.读取终端手机号 
            jT808Package.Header.TerminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength);
            // 3.4.读取消息流水号
            jT808Package.Header.MsgNum= reader.ReadUInt16();
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
                Type jT808BodiesImplType = config.MsgIdFactory.GetBodiesImplTypeByMsgId(jT808Package.Header.MsgId, jT808Package.Header.TerminalPhoneNo);
                if (jT808BodiesImplType != null)
                {
                    if (jT808Package.Header.MessageBodyProperty.IsPackage)
                    {
                        if (jT808Package.Header.PackageIndex > 1)
                        {
                            try
                            {
                                //4.2处理第二包之后的分包数据消息体
                                jT808Package.Bodies = JT808SplitPackageBodiesFormatter.Instance.Deserialize(ref reader, config);
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
                                jT808Package.Bodies = JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(
                                    config.GetMessagePackFormatterByType(jT808BodiesImplType),
                                    ref reader, config);
                            }
                            catch (Exception ex)
                            {
                                throw new JT808Exception(JT808ErrorCode.BodiesParseError, ex);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            //4.2.处理消息体
                            jT808Package.Bodies = JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(
                                config.GetMessagePackFormatterByType(jT808BodiesImplType),
                                ref reader, config);
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

        public void Serialize(ref JT808MessagePackWriter writer, JT808Package value, IJT808Config config)
        {
            // ---------------开始组包--------------
            // 1.起始符
            writer.WriteStart();
            // 2.写入头部 //部分有带数据体的长度，那么先跳过写入头部部分
            //  2.1.消息ID
            writer.WriteUInt16(value.Header.MsgId);
            //  2.2.消息体属性(包含消息体长度所以先跳过)
            writer.Skip(2,out int msgBodiesPropertyPosition);
            //  2.3.终端手机号 (写死大陆手机号码)
            writer.WriteBCD(value.Header.TerminalPhoneNo, config.TerminalPhoneNoLength);
            value.Header.MsgNum = value.Header.MsgNum > 0 ? value.Header.MsgNum : config.MsgSNDistributed.Increment();
            //  2.4.消息流水号
            writer.WriteUInt16(value.Header.MsgNum);
            //  2.5.判断是否分包
            if (value.Header.MessageBodyProperty.IsPackage)
            {
                // 2.5.1.消息包总数
                writer.WriteUInt16(value.Header.PackgeCount);
                // 2.5.2.消息包序号
                writer.WriteUInt16(value.Header.PackageIndex);
            }
            int headerLength = writer.GetCurrentPosition();
            // 3.处理数据体部分
            if (value.Bodies != null)
            {
                if (!value.Bodies.SkipSerialization)
                {
                    JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(
                        config.GetMessagePackFormatterByType(value.Bodies.GetType()),
                        ref writer, value.Bodies,
                        config);
                }
            }
            //  3.1.处理数据体长度
            value.Header.MessageBodyProperty=new JT808HeaderMessageBodyProperty((ushort)(writer.GetCurrentPosition() - headerLength));
            // 2.2.回写消息体属性
            writer.WriteUInt16Return(value.Header.MessageBodyProperty.Wrap(), msgBodiesPropertyPosition);
            // 4.校验码
            writer.WriteXor();
            // 5.终止符
            writer.WriteEnd();
            // 6.编码
            writer.WriteEncode();
            // ---------------组包结束--------------
        }
    }
}
