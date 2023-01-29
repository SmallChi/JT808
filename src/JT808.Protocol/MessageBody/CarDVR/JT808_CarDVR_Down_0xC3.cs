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
    /// 设置记录仪脉冲系数
    /// 返回：记录仪脉冲系数
    /// </summary>
    public class JT808_CarDVR_Down_0xC3 : JT808MessagePackFormatter<JT808_CarDVR_Down_0xC3>, JT808CarDVRDownBodies, IJT808Analyze
    {
        /// <summary>
        /// 0xC3
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.set_pulse_coefficient_recorder.ToByteValue();
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime RealTime { get; set; }
        /// <summary>
        /// 脉冲系数
        /// </summary>
        public ushort PulseCoefficient { get; set; }
        /// <summary>
        /// 记录仪脉冲系数
        /// </summary>
        public string Description => "记录仪脉冲系数";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Down_0xC3 value = new JT808_CarDVR_Down_0xC3();
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.RealTime:yyMMddHHmmss}]当前时间", value.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.PulseCoefficient = reader.ReadUInt16();
            writer.WriteNumber($"[{value.PulseCoefficient.ReadNumber()}]脉冲系数", value.PulseCoefficient);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0xC3 value, IJT808Config config)
        {
            writer.WriteDateTime_yyMMddHHmmss(value.RealTime);
            writer.WriteUInt16(value.PulseCoefficient);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Down_0xC3 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0xC3 value = new JT808_CarDVR_Down_0xC3();
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            value.PulseCoefficient = reader.ReadUInt16();
            return value;
        }
    }
}
