using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8203_Formatter : IJT808MessagePackFormatter<JT808_0x8203>
    {
        public JT808_0x8203 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8203 jT808_0X8203 = new JT808_0x8203();
            jT808_0X8203.AlarmMsgNum = reader.ReadUInt16();
            jT808_0X8203.ManualConfirmAlarmType = reader.ReadUInt32();
            return jT808_0X8203;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8203 value, IJT808Config config)
        {
            writer.WriteUInt16(value.AlarmMsgNum);
            writer.WriteUInt32(value.ManualConfirmAlarmType);
        }
    }
}
