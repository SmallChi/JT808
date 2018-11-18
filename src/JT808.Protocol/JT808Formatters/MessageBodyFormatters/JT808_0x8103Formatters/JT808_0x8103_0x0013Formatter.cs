using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody.JT808_0x8103_Body;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808_0x8103Formatters
{
    public class JT808_0x8103_0x0013Formatter : IJT808Formatter<JT808_0x8103_0x0013>
    {
        public JT808_0x8103_0x0013 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8103_0x0013 jT808_0x8103_0x0013 = new JT808_0x8103_0x0013();
            jT808_0x8103_0x0013.ParamLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0x8103_0x0013.ParamValue = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0x8103_0x0013.ParamLength);
            readSize = offset;
            return jT808_0x8103_0x0013;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8103_0x0013 value)
        {
            offset += 1;
            var lenth = JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.ParamValue);         
            JT808BinaryExtensions.WriteByteLittle(bytes, offset-1, (byte)lenth);
            offset += lenth;
            return offset;
        }
    }
}