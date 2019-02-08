using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8203Formatter : IJT808Formatter<JT808_0x8203>
    {
        public JT808_0x8203 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8203 jT808_0X8203 = new JT808_0x8203
            {
                AlarmMsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                ManualConfirmAlarmType = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset)
            };
            readSize = offset;
            return jT808_0X8203;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8203 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.AlarmMsgNum);
            offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, value.ManualConfirmAlarmType);
            return offset;
        }
    }
}
