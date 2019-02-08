using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0303Formatter : IJT808Formatter<JT808_0x0303>
    {
        public JT808_0x0303 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0303 jT808_0X0303 = new JT808_0x0303
            {
                InformationType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                Flag = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808_0X0303;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0303 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.InformationType);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Flag);
            return offset;
        }
    }
}
