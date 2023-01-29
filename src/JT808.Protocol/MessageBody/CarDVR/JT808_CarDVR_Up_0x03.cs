using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody.CarDVR
{
    /// <summary>
    /// 采集累计行驶里程
    /// 返回：实时时间、安装时的初始里程及安装后的累计行驶里程
    /// </summary>
    public class JT808_CarDVR_Up_0x03 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x03>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x03
        /// </summary>
        public byte CommandId => JT808CarDVRCommandID.collect_accumulated_mileage.ToByteValue();
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
        /// <summary>
        /// 实时时间、安装时的初始里程及安装后的累计行驶里程
        /// </summary>
        public string Description => "实时时间、安装时的初始里程及安装后的累计行驶里程";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Up_0x03 value = new JT808_CarDVR_Up_0x03();
            var hex = reader.ReadVirtualArray(6);
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{hex.ToArray().ToHexString()}]实时时间", value.RealTime);
            hex = reader.ReadVirtualArray(6);
            value.FirstInstallTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{hex.ToArray().ToHexString()}]初次安装时间", value.RealTime);
            hex = reader.ReadVirtualArray(4);
            value.FirstMileage = reader.ReadBCD(8);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]初始里程", value.FirstMileage);
            hex = reader.ReadVirtualArray(4);
            value.TotalMilage = reader.ReadBCD(8);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]累计里程", value.TotalMilage);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x03 value, IJT808Config config)
        {
            writer.WriteDateTime_yyMMddHHmmss(value.RealTime);
            writer.WriteDateTime_yyMMddHHmmss(value.FirstInstallTime);
            writer.WriteBCD(value.FirstMileage, 8);
            writer.WriteBCD(value.TotalMilage, 8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x03 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x03 value = new JT808_CarDVR_Up_0x03();
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            value.FirstInstallTime = reader.ReadDateTime_yyMMddHHmmss();
            value.FirstMileage = reader.ReadBCD(8);
            value.TotalMilage = reader.ReadBCD(8);
            return value;
        }
    }
}
