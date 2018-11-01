using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8803Formatter : IJT808Formatter<JT808_0x8803>
    {
        public JT808_0x8803 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8803 jT808_0X8803 = new JT808_0x8803();
            jT808_0X8803.MultimediaType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8803.ChannelId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8803.EventItemCoding = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8803.StartTime = JT808BinaryExtensions.ReadDateTimeLittle(bytes, ref offset);
            jT808_0X8803.EndTime = JT808BinaryExtensions.ReadDateTimeLittle(bytes, ref offset);
            jT808_0X8803.MultimediaDeleted = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X8803;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8803 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset,value.MultimediaType);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.ChannelId);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.EventItemCoding);
            offset += JT808BinaryExtensions.WriteDateTime6Little(memoryOwner, offset, value.StartTime);
            offset += JT808BinaryExtensions.WriteDateTime6Little(memoryOwner, offset, value.EndTime);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.MultimediaDeleted);
            return offset;
        }
    }
}
