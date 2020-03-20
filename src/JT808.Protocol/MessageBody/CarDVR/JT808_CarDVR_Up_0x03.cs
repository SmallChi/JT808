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
    /// 采集累计行驶里程
    /// 返回：实时时间、安装时的初始里程及安装后的累计行驶里程
    /// </summary>
    public class JT808_CarDVR_Up_0x03 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId => JT808CarDVRCommandID.采集累计行驶里程.ToByteValue();
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime RealTime { get; set; }
        /// <summary>
        /// 初次安装时间
        /// </summary>
        public DateTime FirstInstallTime { get; set; }
        /// <summary>
        /// 初始里程
        /// </summary>
        public string FirstMileage { get; set; }
        /// <summary>
        /// 累计里程
        /// </summary>
        public string TotalMilage { get; set; }
        public override string Description => "实时时间、安装时的初始里程及安装后的累计行驶里程";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x03 value = new JT808_CarDVR_Up_0x03();
            value.RealTime = reader.ReadDateTime6();
            value.FirstInstallTime = reader.ReadDateTime6();
            value.FirstMileage = reader.ReadBCD(8);
            value.TotalMilage = reader.ReadBCD(8);
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x03 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x03;
            writer.WriteDateTime6(value.RealTime);
            writer.WriteDateTime6(value.FirstInstallTime);
            writer.WriteBCD(value.FirstMileage, 8);
            writer.WriteBCD(value.TotalMilage, 8);
        }
    }
}
