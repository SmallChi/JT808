using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8304Formatter : IJT808Formatter<JT808_0x8304>
    {
        public JT808_0x8304 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8304 jT808_0X8304 = new JT808_0x8304();
            jT808_0X8304.InformationType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8304.InformationLength = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8304.InformationContent = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X8304.InformationLength);
            readSize = offset;
            return jT808_0X8304;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8304 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.InformationType);
            // 先计算内容长度（汉字为两个字节）
            offset += 2;
            int byteLength = JT808BinaryExtensions.WriteStringLittle(memoryOwner, offset, value.InformationContent);
            JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset - 2, (ushort)byteLength);
            offset += byteLength;
            return offset;
        }
    }
}
