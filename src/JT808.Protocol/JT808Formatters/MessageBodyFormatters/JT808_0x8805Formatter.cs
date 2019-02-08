using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8805Formatter : IJT808Formatter<JT808_0x8805>
    {
        public JT808_0x8805 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8805 jT808_0X8805 = new JT808_0x8805
            {
                MultimediaId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset),
                MultimediaDeleted = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808_0X8805;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8805 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.MultimediaId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.MultimediaDeleted);
            return offset;
        }
    }
}
