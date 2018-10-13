using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8804Formatter : IJT808Formatter<JT808_0x8804>
    {
        public JT808_0x8804 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8804 jT808_0X8804 = new JT808_0x8804();
            jT808_0X8804.RecordCmd = (JT808RecordCmd)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8804.RecordTime = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8804.RecordSave = (JT808RecordSave)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8804.AudioSampleRate = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X8804;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8804 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, (byte)value.RecordCmd);
            offset += JT808BinaryExtensions.WriteUInt16Little(memoryOwner, offset, value.RecordTime);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, (byte)value.RecordSave);
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.AudioSampleRate);
            return offset;
        }
    }
}
