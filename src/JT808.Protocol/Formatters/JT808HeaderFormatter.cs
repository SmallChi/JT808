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
            jT808Header.MessageBodyProperty = JT808HeaderMessageBodyPropertyFormatter.Instance.Deserialize(ref reader, config);
            // 3.终端手机号
            jT808Header.TerminalPhoneNo = reader.ReadBCD(config.TerminalPhoneNoLength);
            jT808Header.MsgNum = reader.ReadUInt16();
            return jT808Header;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808Header value, IJT808Config config)
        {
            // 1.消息ID
            writer.WriteUInt16(value.MsgId);
            // 2.消息体属性
            JT808HeaderMessageBodyPropertyFormatter.Instance.Serialize(ref writer,value.MessageBodyProperty, config);
            // 3.终端手机号
            writer.WriteBCD(value.TerminalPhoneNo, config.TerminalPhoneNoLength);
            // 4.消息流水号
            writer.WriteUInt16(value.MsgNum);
        }
    }
}
