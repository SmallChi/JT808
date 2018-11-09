using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8A00Formatter : IJT808Formatter<JT808_0x8A00>
    {
        public JT808_0x8A00 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8A00 jT808_0X8A00 = new JT808_0x8A00();
            jT808_0X8A00.E = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
            jT808_0X8A00.N = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset,128);
            readSize = offset;
            return jT808_0X8A00;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8A00 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.E);
            offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, value.N);
            return offset;
        }
    }
}
