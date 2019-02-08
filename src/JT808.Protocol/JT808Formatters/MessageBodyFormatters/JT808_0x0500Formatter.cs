using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0500Formatter : IJT808Formatter<JT808_0x0500>
    {
        public JT808_0x0500 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0500 jT808_0X0500 = new JT808_0x0500
            {
                MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                JT808_0x0200 = JT808FormatterExtensions.GetFormatter<JT808_0x0200>().Deserialize(bytes.Slice(offset), out readSize)
            };
            readSize = offset;
            return jT808_0X0500;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0500 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.MsgNum);
            offset = JT808FormatterExtensions.GetFormatter<JT808_0x0200>().Serialize(ref bytes, offset, value.JT808_0x0200);
            return offset;
        }
    }
}
