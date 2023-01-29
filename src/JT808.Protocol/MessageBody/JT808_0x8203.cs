using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Linq;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 人工确认报警消息
    /// 0x8203
    /// </summary>
    public class JT808_0x8203 : JT808MessagePackFormatter<JT808_0x8203>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x8203
        /// </summary>
        public ushort MsgId => 0x8203;
        /// <summary>
        /// 人工确认报警消息
        /// </summary>
        public string Description => "人工确认报警消息";
        /// <summary>
        /// 报警消息流水号
        /// 需人工确认的报警消息流水号，0 表示该报警类型所有消息
        /// </summary>
        public ushort AlarmMsgNum { get; set; }
        /// <summary>
        /// 人工确认报警类型
        /// </summary>
        public uint ManualConfirmAlarmType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8203 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8203 jT808_0X8203 = new JT808_0x8203();
            jT808_0X8203.AlarmMsgNum = reader.ReadUInt16();
            jT808_0X8203.ManualConfirmAlarmType = reader.ReadUInt32();
            return jT808_0X8203;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8203 value, IJT808Config config)
        {
            writer.WriteUInt16(value.AlarmMsgNum);
            writer.WriteUInt32(value.ManualConfirmAlarmType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8203 value = new JT808_0x8203();
            value.AlarmMsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.AlarmMsgNum.ReadNumber()}]报警消息流水号", value.AlarmMsgNum);
            value.ManualConfirmAlarmType = reader.ReadUInt32();
            writer.WriteNumber($"[{ value.ManualConfirmAlarmType.ReadNumber()}]人工确认报警类型", value.ManualConfirmAlarmType);
            ReadOnlySpan<char> manualConfirmAlarmTypeBits =string.Join("", Convert.ToString(value.ManualConfirmAlarmType, 2).PadLeft(32, '0').Reverse()).AsSpan();
            writer.WriteStartObject($"人工确认报警对象[{manualConfirmAlarmTypeBits.ToString()}]");
            writer.WriteString("[bit29~bit31]保留", manualConfirmAlarmTypeBits.Slice(29,3).ToString());
            writer.WriteString($"[bit28]{manualConfirmAlarmTypeBits[28]}","确认车辆非法位移报警");
            writer.WriteString($"[bit27]{manualConfirmAlarmTypeBits[27]}", "确认车辆非法点火报警");
            writer.WriteString($"[bit23~bit26]保留", manualConfirmAlarmTypeBits.Slice(23, 4).ToString());
            writer.WriteString($"[bit22]{manualConfirmAlarmTypeBits[22]}", "确认路段行驶时间不足/过长报警");
            writer.WriteString($"[bit21]{manualConfirmAlarmTypeBits[21]}", "确认进出路线报警");
            writer.WriteString($"[bit20]{manualConfirmAlarmTypeBits[20]}", "确认进出区域报警");
            writer.WriteString($"[bit4~bit19]保留", manualConfirmAlarmTypeBits.Slice(4, 16).ToString());
            writer.WriteString($"[bit3]{manualConfirmAlarmTypeBits[3]}", "确认危险预警");
            writer.WriteString($"[bit1~bit2]保留", manualConfirmAlarmTypeBits.Slice(1, 2).ToString());
            writer.WriteString($"[bit0]{manualConfirmAlarmTypeBits[0]}", "确认紧急报警");
            writer.WriteEndObject();
        }
    }
}
