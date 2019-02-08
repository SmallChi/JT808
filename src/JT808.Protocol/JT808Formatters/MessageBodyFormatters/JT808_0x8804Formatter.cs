using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8804Formatter : IJT808Formatter<JT808_0x8804>
    {
        public JT808_0x8804 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8804 jT808_0X8804 = new JT808_0x8804
            {
                RecordCmd = (JT808RecordCmd)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                RecordTime = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                RecordSave = (JT808RecordSave)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                AudioSampleRate = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            readSize = offset;
            return jT808_0X8804;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8804 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.RecordCmd);
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.RecordTime);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.RecordSave);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AudioSampleRate);
            return offset;
        }
    }
}
