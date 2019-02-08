using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0801Formatter : IJT808Formatter<JT808_0x0801>
    {
        public JT808_0x0801 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0801 jT808_0X0801 = new JT808_0x0801
            {
                MultimediaId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset),
                MultimediaType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                MultimediaCodingFormat = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                EventItemCoding = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                ChannelId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                Position = JT808FormatterExtensions.GetFormatter<JT808_0x0200>().Deserialize(bytes.Slice(offset, 28), out int bufReadSize)
            };
            offset += 28;
            jT808_0X0801.MultimediaDataPackage = JT808BinaryExtensions.ReadBytesLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X0801;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0801 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.MultimediaId);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.MultimediaType);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.MultimediaCodingFormat);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.EventItemCoding);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.ChannelId);
            int positionOffset = JT808FormatterExtensions.GetFormatter<JT808_0x0200>().Serialize(ref bytes, offset, value.Position);
            offset += 28;
            offset += JT808BinaryExtensions.WriteBytesLittle(bytes, offset, value.MultimediaDataPackage);
            return offset;
        }
    }
}
