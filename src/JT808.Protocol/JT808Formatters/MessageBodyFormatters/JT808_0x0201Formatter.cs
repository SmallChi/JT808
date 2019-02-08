using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0201Formatter : IJT808Formatter<JT808_0x0201>
    {
        public JT808_0x0201 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0201 jT808_0X0201 = new JT808_0x0201
            {
                MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                Position = JT808FormatterExtensions.GetFormatter<JT808_0x0200>().Deserialize(bytes.Slice(offset), out readSize)
            };
            readSize = offset;
            return jT808_0X0201;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0201 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.MsgNum);
            offset = JT808FormatterExtensions.GetFormatter<JT808_0x0200>().Serialize(ref bytes, offset, value.Position);
            return offset;
        }
    }
}
