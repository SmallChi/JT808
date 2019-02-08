using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0A00Formatter : IJT808Formatter<JT808_0x0A00>
    {
        public JT808_0x0A00 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0A00 jT808_0X0A00 = new JT808_0x0A00
            {
                E = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset),
                N = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, 128)
            };
            if (jT808_0X0A00.N.Length != 128)
            {
                throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(jT808_0X0A00.N)}->128");
            }
            readSize = offset;
            return jT808_0X0A00;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0A00 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.E);
            if (value.N.Length != 128)
            {
                throw new JT808Exception(Enums.JT808ErrorCode.NotEnoughLength, $"{nameof(value.N)}->128");
            }
            offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, value.N);
            return offset;
        }
    }
}
