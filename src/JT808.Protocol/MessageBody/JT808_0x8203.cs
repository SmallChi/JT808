using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 人工确认报警消息
    /// 0x8203
    /// </summary>
    public class JT808_0x8203 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x8203>, IJT808Analyze
    {
        public override ushort MsgId { get; } = 0x8203;
        public override string Description => "人工确认报警消息";
        /// <summary>
        /// 报警消息流水号
        /// 需人工确认的报警消息流水号，0 表示该报警类型所有消息
        /// </summary>
        public ushort AlarmMsgNum { get; set; }
        /// <summary>
        /// 人工确认报警类型
        /// </summary>
        public uint ManualConfirmAlarmType { get; set; }

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

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8203 value = new JT808_0x8203();
            value.AlarmMsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.AlarmMsgNum.ReadNumber()}]报警消息流水号", value.AlarmMsgNum);
            value.ManualConfirmAlarmType = reader.ReadUInt32();
            writer.WriteNumber($"[{ value.ManualConfirmAlarmType.ReadNumber()}]人工确认报警类型", value.ManualConfirmAlarmType);
        }
    }
}
