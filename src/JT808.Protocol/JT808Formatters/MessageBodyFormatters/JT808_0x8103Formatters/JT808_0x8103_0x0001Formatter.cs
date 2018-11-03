using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody.JT808_0x8103_Body;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808_0x8103Formatters
{
    public class JT808_0x8103_0x0001Formatter : IJT808Formatter<JT808_0x8103_0x0001>
    {
        public JT808_0x8103_0x0001 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8103_0x0001 jT808_0x8103_0x0001 = new JT808_0x8103_0x0001();
            jT808_0x8103_0x0001.ParamLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0x8103_0x0001.ParamValue = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
            readSize = offset;
            return jT808_0x8103_0x0001;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8103_0x0001 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.ParamLength);
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.ParamValue);
            return offset;
        }
    }
}
