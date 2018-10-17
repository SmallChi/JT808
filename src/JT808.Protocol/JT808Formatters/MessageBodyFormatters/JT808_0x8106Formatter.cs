using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8106Formatter : IJT808Formatter<JT808_0x8106>
    {
        public JT808_0x8106 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8106 jT808_0X8106 = new JT808_0x8106();
            jT808_0X8106.ParameterCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8106.Parameters = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, jT808_0X8106.ParameterCount*4);            
            readSize = offset;
            return jT808_0X8106;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8106 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.ParameterCount);
            offset += JT808BinaryExtensions.WriteBytesLittle(memoryOwner, offset, value.Parameters);
            return offset;
        }
    }
}
