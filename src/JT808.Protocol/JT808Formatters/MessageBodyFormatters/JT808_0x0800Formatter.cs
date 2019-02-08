using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0800Formatter : IJT808Formatter<JT808_0x0800>
    {
        public JT808_0x0800 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0800 jT808_0X0800 = new JT808_0x0800
            {
                MultimediaId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset),
                MultimediaType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                MultimediaCodingFormat = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                EventItemCoding = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                ChannelId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808_0X0800;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0800 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.MultimediaId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.MultimediaType);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.MultimediaCodingFormat);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.EventItemCoding);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.ChannelId);
            return offset;
        }
    }
}
