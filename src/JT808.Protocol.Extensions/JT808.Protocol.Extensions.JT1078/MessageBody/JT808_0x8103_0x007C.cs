using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    ///终端休眠模式唤醒设置
    /// 0x8103_0x007C
    /// </summary>
    public class JT808_0x8103_0x007C : JT808MessagePackFormatter<JT808_0x8103_0x007C>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public uint ParamId { get; set; } = 0x007C;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte ParamLength { get; set; } = 20;
        /// <summary>
        /// 休眠唤醒模式
        /// </summary>
        public byte SleepWakeMode { get; set; }
        /// <summary>
        /// 唤醒条件类型
        /// </summary>
        public byte WakeConditionType  { get; set; }
        /// <summary>
        /// 定时唤醒日设置
        /// </summary>
        public byte TimerWakeDaySet { get; set; }
        /// <summary>
        /// 日定时唤醒参数列表
        /// </summary>
        public JT808_0x8103_0x007C_TimerWakeDayParamter TimerWakeDayParamter { get; set; }
        /// <summary>
        /// 终端休眠模式唤醒设置
        /// </summary>
        public string Description => "终端休眠模式唤醒设置";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x007C value = new JT808_0x8103_0x007C();
            value.ParamId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ParamId.ReadNumber()}]参数 ID", value.ParamId);
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]数据长度", value.ParamLength);
            value.SleepWakeMode = reader.ReadByte();
            writer.WriteString($"[{value.SleepWakeMode.ReadNumber()}]休眠唤醒模式", SleepWakeModeDisplay(value.SleepWakeMode));
            value.WakeConditionType = reader.ReadByte();
            writer.WriteString($"[{value.WakeConditionType.ReadNumber()}]唤醒条件类型", WakeConditionTypeDisplay(value.WakeConditionType));
            value.TimerWakeDaySet = reader.ReadByte();
            writer.WriteString($"[{value.TimerWakeDaySet.ReadNumber()}]定时唤醒日设置", TimerWakeDaySetDisplay(value.TimerWakeDaySet));
            writer.WriteStartObject("日定时唤醒参数列表");
            config.GetMessagePackFormatter<JT808_0x8103_0x007C_TimerWakeDayParamter>().Analyze(ref reader,writer, config);
            writer.WriteEndObject();
            string SleepWakeModeDisplay(byte SleepWakeMode) {
                string sleepWakeModeDisplay = string.Empty;
                sleepWakeModeDisplay += (SleepWakeMode & 0x01) == 1 ? ",条件唤醒" : "";
                sleepWakeModeDisplay += (SleepWakeMode>>1 & 0x01) == 1 ? ",定时唤醒" : "";
                sleepWakeModeDisplay += (SleepWakeMode>>2 & 0x01) == 1 ? ",手动唤醒" : "";
                return sleepWakeModeDisplay.Length>0? sleepWakeModeDisplay.Substring(1):"";
            }
            string WakeConditionTypeDisplay(byte WakeConditionType)
            {
                string wakeConditionTypeDisplay = string.Empty;
                wakeConditionTypeDisplay += (WakeConditionType & 0x01) == 1 ? ",紧急报警" : "";
                wakeConditionTypeDisplay += (WakeConditionType >> 1 & 0x01) == 1 ? ",碰撞侧翻报警" : "";
                wakeConditionTypeDisplay += (WakeConditionType >> 2 & 0x01) == 1 ? ",车辆开门" : "";
                return wakeConditionTypeDisplay.Length>0? wakeConditionTypeDisplay.Substring(1):"";
            }
            string TimerWakeDaySetDisplay(byte TimerWakeDaySet)
            {
                string timerWakeDaySetDisplay = string.Empty;
                timerWakeDaySetDisplay+= (TimerWakeDaySet & 0x01) == 1 ? ",周一":"";
                timerWakeDaySetDisplay+= (TimerWakeDaySet >> 1 & 0x01) == 1 ? ",周二" :"";
                timerWakeDaySetDisplay+= (TimerWakeDaySet >> 1 & 0x02) == 1 ? ",周三" :"";
                timerWakeDaySetDisplay+= (TimerWakeDaySet >> 1 & 0x03) == 1 ? ",周四" :"";
                timerWakeDaySetDisplay+= (TimerWakeDaySet >> 1 & 0x04) == 1 ? ",周五" :"";
                timerWakeDaySetDisplay+= (TimerWakeDaySet >> 1 & 0x05) == 1 ? ",周六" :"";
                timerWakeDaySetDisplay += (TimerWakeDaySet >> 1 & 0x06) == 1 ? ",周日" : "";
                return timerWakeDaySetDisplay.Length>0? timerWakeDaySetDisplay.Substring(1):"";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x007C Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007C jT808_0x8103_0x007C = new JT808_0x8103_0x007C();
            jT808_0x8103_0x007C.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x007C.ParamLength = reader.ReadByte();
            jT808_0x8103_0x007C.SleepWakeMode = reader.ReadByte();
            jT808_0x8103_0x007C.WakeConditionType = reader.ReadByte();
            jT808_0x8103_0x007C.TimerWakeDaySet = reader.ReadByte();
            jT808_0x8103_0x007C.TimerWakeDayParamter = config.GetMessagePackFormatter<JT808_0x8103_0x007C_TimerWakeDayParamter>().Deserialize(ref reader, config);
            return jT808_0x8103_0x007C;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007C value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out var position);
            writer.WriteByte(value.SleepWakeMode);
            writer.WriteByte(value.WakeConditionType);
            writer.WriteByte(value.TimerWakeDaySet);
            config.GetMessagePackFormatter<JT808_0x8103_0x007C_TimerWakeDayParamter>().Serialize(ref writer, value.TimerWakeDayParamter, config);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);
        }
    }
}
