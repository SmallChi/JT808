using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x0110Formatter : IJT808Formatter<JT808_0x8103_0x0110>
    {
        public JT808_0x8103_0x0110 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8103_0x0110 jT808_0x8103_0x0110 = new JT808_0x8103_0x0110
            {
                ParamLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            jT808_0x8103_0x0110.ParamValue = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset, jT808_0x8103_0x0110.ParamLength);
            readSize = offset;
            return jT808_0x8103_0x0110;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8103_0x0110 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.ParamValue.Length);
            offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, value.ParamValue);
            return offset;
        }
    }
}