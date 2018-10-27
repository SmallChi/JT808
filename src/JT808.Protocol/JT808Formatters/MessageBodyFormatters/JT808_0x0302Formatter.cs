using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{

    public class JT808_0x0302Formatter : IJT808Formatter<JT808_0x0302>
    {
        public JT808_0x0302 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0302 jT808_0X0302 = new JT808_0x0302();
            jT808_0X0302.ReplySNo = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X0302.AnswerId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X0302;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x0302 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.ReplySNo);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.AnswerId);
            return offset;
        }
    }
}
