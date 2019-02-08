using JT808.Protocol.Extensions;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.Test.JT808_0x0900_BodiesImpl;
using System;

namespace JT808.Protocol.Test.MessageBody.JT808Formatters
{
    public class JT808_0x0900_0x83Formatter : IJT808Formatter<JT808_0x0900_0x83>
    {
        public JT808_0x0900_0x83 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0900_0x83 jT808PassthroughType0x83 = new JT808_0x0900_0x83
            {
                PassthroughContent = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808PassthroughType0x83;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0900_0x83 value)
        {
            offset += JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.PassthroughContent);
            return offset;
        }
    }
}
