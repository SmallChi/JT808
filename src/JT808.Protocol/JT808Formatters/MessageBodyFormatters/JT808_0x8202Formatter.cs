using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8202Formatter : IJT808Formatter<JT808_0x8202>
    {
        public JT808_0x8202 Deserialize(ReadOnlySpan<byte> bytes,  out int readSize)
        {
            int offset = 0;
            JT808_0x8202 jT808_0X8202 = new JT808_0x8202();
            jT808_0X8202.Interval = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8202.LocationTrackingValidity = JT808BinaryExtensions.ReadInt32Little(bytes, ref offset);
            readSize = offset;
            return jT808_0X8202;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8202 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.Interval);
            offset += JT808BinaryExtensions.WriteInt32Little(memoryOwner, offset, value.LocationTrackingValidity);
            return offset;
        }
    }
}
