using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_0x001AFormatter : IJT808Formatter<JT808_0x8103_0x001A>
    {
        public JT808_0x8103_0x001A Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8103_0x001A jT808_0x8103_0x001A = new JT808_0x8103_0x001A
            {
                ParamLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            jT808_0x8103_0x001A.ParamValue = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0x8103_0x001A.ParamLength);
            readSize = offset;
            return jT808_0x8103_0x001A;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8103_0x001A value)
        {
            offset += 1;
            var lenth = JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.ParamValue);
            JT808BinaryExtensions.WriteByteLittle(bytes, offset - 1, (byte)lenth);
            offset += lenth;
            return offset;
        }
    }
}