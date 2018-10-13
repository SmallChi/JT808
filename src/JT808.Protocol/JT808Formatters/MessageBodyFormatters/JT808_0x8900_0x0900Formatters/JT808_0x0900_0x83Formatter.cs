using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808_0x8900_0x0900Formatters
{
    public class JT808_0x0900_0x83Formatter : IJT808Formatter<JT808_0x0900_0x83>
    {
        public JT808_0x0900_0x83 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0900_0x83 jT808PassthroughType0x83 = new JT808_0x0900_0x83();
            jT808PassthroughType0x83.PassthroughContent = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset);
            readSize = offset;
            return jT808PassthroughType0x83;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x0900_0x83 value)
        {
            offset += JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.PassthroughContent);
            return offset;
        }
    }
}
