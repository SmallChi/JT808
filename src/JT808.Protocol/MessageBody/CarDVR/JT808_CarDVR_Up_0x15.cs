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
    public class JT808_CarDVR_Up_0x15 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集指定的速度状态日志.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x15_SpeedStatusLog> JT808_CarDVR_Up_0x15_SpeedStatusLogs { get; set; }
        public override string Description => "符合条件的速度状态日志";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x15 value = new JT808_CarDVR_Up_0x15();
            value.JT808_CarDVR_Up_0x15_SpeedStatusLogs = new List<JT808_CarDVR_Up_0x15_SpeedStatusLog>();
            var count = (reader.ReadCurrentRemainContentLength() - 1 - 1) / 133;//记录块个数, -1 去掉808校验位，-1去掉808尾部标志
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x15_SpeedStatusLog jT808_CarDVR_Up_0x15_SpeedStatusLog = new JT808_CarDVR_Up_0x15_SpeedStatusLog();
                jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatus = reader.ReadByte();
                jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatusStartTime = reader.ReadDateTime6();
                jT808_CarDVR_Up_0x15_SpeedStatusLog.SpeedStatusEndTime = reader.ReadDateTime6();
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

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x15 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x15;
            foreach (var speedStatusLog in value.JT808_CarDVR_Up_0x15_SpeedStatusLogs)
            {
                writer.WriteByte(speedStatusLog.SpeedStatus);
                writer.WriteDateTime6(speedStatusLog.SpeedStatusStartTime);
                writer.WriteDateTime6(speedStatusLog.SpeedStatusEndTime);
                foreach (var speedPerSecond in speedStatusLog.JT808_CarDVR_Up_0x15_SpeedPerSeconds)
                {
                    writer.WriteByte(speedPerSecond.RecordSpeed);
                    writer.WriteByte(speedPerSecond.ReferenceSpeed);
                }
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
            public byte ReferenceSpeed  { get; set; }
        }
    }
}
