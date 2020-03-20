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
    /// 采集指定的行驶速度记录
    /// 返回：符合条件的行驶速度记录
    /// 如在指定的时间范围内无数据记录，则本数据块数据为空
    /// </summary>
    public class JT808_CarDVR_Up_0x08 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集指定的行驶速度记录.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x08_SpeedPerMinute> JT808_CarDVR_Up_0x08_SpeedPerMinutes { get; set; }
        public override string Description => "符合条件的行驶速度记录";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x08 value = new JT808_CarDVR_Up_0x08();
            var count = (reader.ReadCurrentRemainContentLength() - 1 - 1)/126;//记录块个数, -1 去掉808校验位，-1去掉808尾部标志
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x08_SpeedPerMinute jT808_CarDVR_Up_0X08_SpeedPerMinute = new JT808_CarDVR_Up_0x08_SpeedPerMinute()
                {
                    StartTime = reader.ReadDateTime6(),
                    JT808_CarDVR_Up_0x08_SpeedPerSeconds = new List<JT808_CarDVR_Up_0x08_SpeedPerSecond>()
                };
                for (int j = 0; j < 60; j++)//60秒
                {
                    jT808_CarDVR_Up_0X08_SpeedPerMinute.JT808_CarDVR_Up_0x08_SpeedPerSeconds.Add(new JT808_CarDVR_Up_0x08_SpeedPerSecond
                    {
                        AvgSpeedAfterStartTime = reader.ReadByte(),
                        StatusSignalAfterStartTime = reader.ReadByte()
                    });
                }
            }
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x08 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x08;
            foreach (var speedPerMinute in value.JT808_CarDVR_Up_0x08_SpeedPerMinutes)
            {
                writer.WriteDateTime6(speedPerMinute.StartTime);
                foreach (var speedPerSecond in speedPerMinute.JT808_CarDVR_Up_0x08_SpeedPerSeconds)
                {
                    writer.WriteByte(speedPerSecond.AvgSpeedAfterStartTime);
                    writer.WriteByte(speedPerSecond.StatusSignalAfterStartTime);
                }
            }
        }
        /// <summary>
        /// 单位分钟行驶速度记录数据块格式
        /// 1.本数据块总长度为 126 个字节，不足部分以 FFH补齐；
        /// 2.如单位分钟内无数据记录，则本数据块无效，数据长度为0，数据为空
        /// </summary>
        public class JT808_CarDVR_Up_0x08_SpeedPerMinute
        {
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime StartTime { get; set; }
            /// <summary>
            /// 60s钟，每秒的信息
            /// </summary>
            public List<JT808_CarDVR_Up_0x08_SpeedPerSecond> JT808_CarDVR_Up_0x08_SpeedPerSeconds { get; set; }

        }
        /// <summary>
        /// 开始时间之后每秒钟的平均速度和状态信号
        /// </summary>
        public class JT808_CarDVR_Up_0x08_SpeedPerSecond
        {
            /// <summary>
            /// 开始时间之后每秒钟的平均速度
            /// </summary>
            public byte AvgSpeedAfterStartTime { get; set; }
            /// <summary>
            /// 开始时间之后每秒钟的状态信号
            /// </summary>
            public byte StatusSignalAfterStartTime { get; set; }
        }
    }
}
