using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0800Formatter : IJT808Formatter<JT808_0x0800>
    {
        public JT808_0x0800 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0800 jT808_0X0800 = new JT808_0x0800();
            jT808_0X0800.MultiMediaId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);
            jT808_0X0800.MultiMediaType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0800.MultiMediaCodingFormat = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0800.EventItemCoding = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0800.ChannelId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X0800;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x0800 value)
        {
            offset += JT808BinaryExtensions.WriteUInt32Little(memoryOwner, offset, value.MultiMediaId);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.MultiMediaType);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.MultiMediaCodingFormat);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.EventItemCoding);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.ChannelId);
            return offset;
        }
    }
}
