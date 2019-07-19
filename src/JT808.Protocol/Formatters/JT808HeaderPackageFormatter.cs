using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;

namespace JT808.Protocol.Formatters
{
    public class JT808HeaderPackageFormatter : IJT808MessagePackFormatter<JT808HeaderPackage>
    {
        public static readonly JT808HeaderPackageFormatter Instance = new JT808HeaderPackageFormatter();
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
            jT808Package.Header.MessageBodyProperty = new JT808HeaderMessageBodyProperty();
            ushort messageBodyPropertyValue = reader.ReadUInt16();
            //    3.2.1.解包消息体属性
            jT808Package.Header.MessageBodyProperty.Unwrap(messageBodyPropertyValue, config);
            // 3.3.读取终端手机号 
            jT808Package.Header.TerminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength);
            // 3.4.读取消息流水号
            jT808Package.Header.MsgNum = reader.ReadUInt16();
            // 3.5.判断有无分包
            if (jT808Package.Header.MessageBodyProperty.IsPackge)
            {
                //3.5.1.读取消息包总数
                jT808Package.Header.MessageBodyProperty.PackgeCount = reader.ReadUInt16();
                //3.5.2.读取消息包序号
                jT808Package.Header.MessageBodyProperty.PackageIndex = reader.ReadUInt16();
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
