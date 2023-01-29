using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.CarDVR
{
    /// <summary>
    /// 采集指定的速度状态日志
    /// 返回：符合条件的速度状态日志
    /// </summary>
    public class JT808_CarDVR_Up_0x15 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x15>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x15
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.collect_specified_speed_status_logs.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x15_SpeedStatusLog> JT808_CarDVR_Up_0x15_SpeedStatusLogs { get; set; }
        /// <summary>
        /// 符合条件的速度状态日志
        /// </summary>
        public string Description => "符合条件的速度状态日志";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            writer.WriteStartArray("请求发送指定的时间范围内 N 个单位数据块的数据");
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 133;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x15_SpeedStatusLog jT808_CarDVR_Up_0x15_SpeedStatusLog = new JT808_CarDVR_Up_0x15_SpeedStatusLog();
                writer.WriteStartObject();
                writer.WriteStartObject($"指定的结束时间之前最近的第{i+1} 条速度状态日志");
                jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatus = reader.ReadByte();
                writer.WriteString($"[{ jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatus.ReadNumber()}]速度状态", SpeedStatusDisplay(jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatus));
                var hex = reader.ReadVirtualArray(6);
                jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatusStartTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{ hex.ToArray().ToHexString()}]速度状态判定的开始时间", jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatusStartTime);
                hex = reader.ReadVirtualArray(6);
                jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatusEndTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{ hex.ToArray().ToHexString()}]速度状态判定的结束时间", jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatusEndTime);
                writer.WriteStartArray("前60s速度状态日志");
                for (int j = 0; j < 60; j++)//60组
                {
                    writer.WriteStartObject();
                    JT808_CarDVR_Up_0x15_SpeedPerSecond jT808_CarDVR_Up_0X15_SpeedPerSecond = new JT808_CarDVR_Up_0x15_SpeedPerSecond();
                    if (j == 0)
                    {
                        jT808_CarDVR_Up_0X15_SpeedPerSecond.RecordSpeed = reader.ReadByte();
                        writer.WriteNumber($"[{ jT808_CarDVR_Up_0X15_SpeedPerSecond.RecordSpeed.ReadNumber()}]开始时间对应的记录速度", jT808_CarDVR_Up_0X15_SpeedPerSecond.RecordSpeed);
                        jT808_CarDVR_Up_0X15_SpeedPerSecond.ReferenceSpeed = reader.ReadByte();
                        writer.WriteNumber($"[{ jT808_CarDVR_Up_0X15_SpeedPerSecond.ReferenceSpeed.ReadNumber()}]开始时间对应的参考速度", jT808_CarDVR_Up_0X15_SpeedPerSecond.ReferenceSpeed);
                    }
                    else
                    {
                        jT808_CarDVR_Up_0X15_SpeedPerSecond.RecordSpeed = reader.ReadByte();
                        writer.WriteNumber($"[{ jT808_CarDVR_Up_0X15_SpeedPerSecond.RecordSpeed.ReadNumber()}]开始时间后第{j}秒对应的记录速度", jT808_CarDVR_Up_0X15_SpeedPerSecond.RecordSpeed);
                        jT808_CarDVR_Up_0X15_SpeedPerSecond.ReferenceSpeed = reader.ReadByte();
                        writer.WriteNumber($"[{ jT808_CarDVR_Up_0X15_SpeedPerSecond.ReferenceSpeed.ReadNumber()}]开始时间后第{j}秒对应的参考速度", jT808_CarDVR_Up_0X15_SpeedPerSecond.ReferenceSpeed);
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            static string SpeedStatusDisplay(byte speedStatus) {
                if (speedStatus == 0x01)
                {
                    return "正常";
                }
                else if (speedStatus == 0x02)
                {
                    return "异常";
                }
                else {
                    return "未知";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x15 value, IJT808Config config)
        {
            foreach (var speedStatusLog in value.JT808_CarDVR_Up_0x15_SpeedStatusLogs)
            {
                writer.WriteByte(speedStatusLog.SpeedStatus);
                writer.WriteDateTime_yyMMddHHmmss(speedStatusLog.SpeedStatusStartTime);
                writer.WriteDateTime_yyMMddHHmmss(speedStatusLog.SpeedStatusEndTime);
                for (int i = 0; i < 60; i++)
                {
                    if (i < speedStatusLog.JT808_CarDVR_Up_0x15_SpeedPerSeconds.Count)
                    {
                        writer.WriteByte(speedStatusLog.JT808_CarDVR_Up_0x15_SpeedPerSeconds[i].RecordSpeed);
                        writer.WriteByte(speedStatusLog.JT808_CarDVR_Up_0x15_SpeedPerSeconds[i].ReferenceSpeed);
                    }
                    else {
                        writer.WriteByte(0xFF);
                        writer.WriteByte(0xFF);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x15 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x15 value = new JT808_CarDVR_Up_0x15();
            value.JT808_CarDVR_Up_0x15_SpeedStatusLogs = new List<JT808_CarDVR_Up_0x15_SpeedStatusLog>();
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 133;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x15_SpeedStatusLog jT808_CarDVR_Up_0x15_SpeedStatusLog = new JT808_CarDVR_Up_0x15_SpeedStatusLog();
                jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatus = reader.ReadByte();
                jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatusStartTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatusEndTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_CarDVR_Up_0x15_SpeedStatusLog.JT808_CarDVR_Up_0x15_SpeedPerSeconds = new List<JT808_CarDVR_Up_0x15_SpeedPerSecond>();
                for (int j = 0; j < 60; j++)//60组
                {
                    JT808_CarDVR_Up_0x15_SpeedPerSecond jT808_CarDVR_Up_0X15_SpeedPerSecond = new JT808_CarDVR_Up_0x15_SpeedPerSecond();
                    jT808_CarDVR_Up_0X15_SpeedPerSecond.RecordSpeed = reader.ReadByte();
                    jT808_CarDVR_Up_0X15_SpeedPerSecond.ReferenceSpeed = reader.ReadByte();
                    jT808_CarDVR_Up_0x15_SpeedStatusLog.JT808_CarDVR_Up_0x15_SpeedPerSeconds.Add(jT808_CarDVR_Up_0X15_SpeedPerSecond);
                }
                value.JT808_CarDVR_Up_0x15_SpeedStatusLogs.Add(jT808_CarDVR_Up_0x15_SpeedStatusLog);
            }
            return value;
        }
    }
    /// <summary>
    ///  单位速度状态日志数据块格式
    /// </summary>
    public class JT808_CarDVR_Up_0x15_SpeedStatusLog
    {
        /// <summary>
        ///  记录仪的速度状态
        /// </summary>
        public byte SpeedStatus { get; set; }
        /// <summary>
        /// 速度状态判定的开始时间
        /// </summary>
        public DateTime SpeedStatusStartTime { get; set; }
        /// <summary>
        /// 速度状态判定的结束时间
        /// </summary>
        public DateTime SpeedStatusEndTime { get; set; }
        /// <summary>
        /// 60组
        /// </summary>
        public List<JT808_CarDVR_Up_0x15_SpeedPerSecond> JT808_CarDVR_Up_0x15_SpeedPerSeconds { get; set; }
    }
    /// <summary>
    /// 每秒速度
    /// </summary>
    public class JT808_CarDVR_Up_0x15_SpeedPerSecond
    {
        /// <summary>
        ///  记录速度
        /// </summary>
        public byte RecordSpeed { get; set; }
        /// <summary>
        /// 参考速度
        /// </summary>
        public byte ReferenceSpeed { get; set; }
    }
}
