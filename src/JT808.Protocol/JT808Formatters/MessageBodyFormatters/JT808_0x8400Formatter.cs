using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8400Formatter : IJT808Formatter<JT808_0x8400>
    {
        public JT808_0x8400 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8400 jT808_0X8400 = new JT808_0x8400();
            jT808_0X8400.CallBack = (JT808CallBackType)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            // 最长为 20 字节
            jT808_0X8400.PhoneNumber = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X8400;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8400 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,(byte)value.CallBack);
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.PhoneNumber);
            return offset;
        }
    }
}
