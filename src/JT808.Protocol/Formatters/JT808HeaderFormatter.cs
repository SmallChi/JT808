using JT808.Protocol.Extensions;
using System;
using System.Buffers.Binary;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters
{
    /// <summary>
    /// JT808头部序列化器
    /// </summary>
    public class JT808HeaderFormatter : IJT808MessagePackFormatter<JT808Header>
    {
        public static readonly JT808HeaderFormatter Instance = new JT808HeaderFormatter();
        public JT808Header Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808Header jT808Header = new JT808Header();
            // 1.消息ID
            jT808Header.MsgId = reader.ReadUInt16();
            // 2.消息体属性
            jT808Header.MessageBodyProperty = new JT808HeaderMessageBodyProperty(reader.ReadUInt16());
            // 3.终端手机号
            jT808Header.TerminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength, config.Trim);
            jT808Header.MsgNum = reader.ReadUInt16();
            // 4.判断有无分包
            if (jT808Header.MessageBodyProperty.IsPackage)
            {
                //5.读取消息包总数
                jT808Header.PackgeCount = reader.ReadUInt16();
                //6.读取消息包序号
                jT808Header.PackageIndex = reader.ReadUInt16();
            }
            return jT808Header;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808Header value, IJT808Config config)
        {
            // 1.消息ID
            writer.WriteUInt16(value.MsgId);
            // 2.消息体属性
            writer.WriteUInt16(value.MessageBodyProperty.Wrap());
            // 3.终端手机号
            writer.WriteBCD(value.TerminalPhoneNo, config.TerminalPhoneNoLength);
            // 4.消息流水号
            writer.WriteUInt16(value.MsgNum);
            // 5.判断是否分包
            if (value.MessageBodyProperty.IsPackage)
            {
                // 6.消息包总数
                writer.WriteUInt16(value.PackgeCount);
                // 7.消息包序号
                writer.WriteUInt16(value.PackageIndex);
            }
        }
    }
}
