using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8100Formatter : IJT808Formatter<JT808_0x8100>
    {
        public JT808_0x8100 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8100 jT808_0X8100 = new JT808_0x8100
            {
                MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                JT808TerminalRegisterResult = (JT808TerminalRegisterResult)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            // 只有在成功后才有该字段
            if (jT808_0X8100.JT808TerminalRegisterResult == JT808TerminalRegisterResult.成功)
            {
                jT808_0X8100.Code = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset);
            }
            readSize = offset;
            return jT808_0X8100;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8100 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.MsgNum);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.JT808TerminalRegisterResult);
            // 只有在成功后才有该字段
            if (value.JT808TerminalRegisterResult == JT808TerminalRegisterResult.成功)
            {
                offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.Code);
            }
            return offset;
        }
    }
}
