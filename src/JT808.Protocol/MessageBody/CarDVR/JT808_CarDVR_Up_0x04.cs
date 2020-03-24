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
    /// 采集记录仪脉冲系数
    /// 返回：实时时间及设定的脉冲系数
    /// </summary>
    public class JT808_CarDVR_Up_0x04 : JT808CarDVRUpBodies, IJT808MessagePackFormatter<JT808_CarDVR_Up_0x04>, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集记录仪脉冲系数.ToByteValue();
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime RealTime { get; set; }
        /// <summary>
        /// 脉冲系数
        /// </summary>
        public ushort PulseCoefficient { get; set; }

        public override string Description => "实时时间及设定的脉冲系数";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Up_0x04 value = new JT808_CarDVR_Up_0x04();
            var hex = reader.ReadVirtualArray(6);
            value.RealTime = reader.ReadDateTime6();
            writer.WriteString($"[{hex.ToArray().ToHexString()}]当前时间", value.RealTime);
            value.PulseCoefficient = reader.ReadUInt16();
            writer.WriteNumber($"[{value.PulseCoefficient.ReadNumber()}]脉冲系数",value.PulseCoefficient);
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x04 value, IJT808Config config)
        {
            writer.WriteDateTime6(value.RealTime);
            writer.WriteUInt16(value.PulseCoefficient);
        }

        public JT808_CarDVR_Up_0x04 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x04 value = new JT808_CarDVR_Up_0x04();
            value.RealTime = reader.ReadDateTime6();
            value.PulseCoefficient = reader.ReadUInt16();
            return value;
        }
    }
}
