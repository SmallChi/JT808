using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol.JT808Formatters
{
    /// <summary>
    /// JT808头部序列化器
    /// </summary>
    public class JT808HeaderFormatter : IJT808Formatter<JT808Header>
    {
        public JT808Header Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808Header jT808Header = new JT808Header
            {
                // 1.消息ID
                MsgId = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                // 2.消息体属性
                MessageBodyProperty = JT808FormatterExtensions.GetFormatter<JT808HeaderMessageBodyProperty>().Deserialize(bytes.Slice(offset), out readSize)
            };
            offset += readSize;
            // 3.终端手机号 (写死大陆手机号码)
            jT808Header.TerminalPhoneNo = JT808BinaryExtensions.ReadBCDLittle(bytes, ref offset, 12);
            // 4.消息流水号
            jT808Header.MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            readSize = offset;
            return jT808Header;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808Header value)
        {
            // 1.消息ID
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.MsgId);
            //2.消息体属性
            offset = JT808FormatterExtensions.GetFormatter<JT808HeaderMessageBodyProperty>().Serialize(ref bytes, offset, value.MessageBodyProperty);
            // 3.终端手机号 (写死大陆手机号码)
            offset += JT808BinaryExtensions.WriteBCDLittle(bytes, offset, value.TerminalPhoneNo, 12);
            //消息流水号
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.MsgNum);
            if (value.MessageBodyProperty.IsPackge)
            {
                //消息总包数2位+包序号2位=4位
                offset += 4;
            }
            return offset;
        }
    }
}
