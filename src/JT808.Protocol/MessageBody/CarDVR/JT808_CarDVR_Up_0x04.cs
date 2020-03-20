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
    public class JT808_CarDVR_Up_0x04 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集记录仪脉冲系数.ToByteValue();
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime RealTime { get; set; }
        /// <summary>
        /// 脉冲系数高字节
        /// </summary>
        public byte PulseCoefficientHighByte { get; set; }
        /// <summary>
        /// 仪脉冲系数低字节
        /// </summary>
        public byte PulseCoefficientLowByte { get; set; }

        public override string Description => "实时时间及设定的脉冲系数";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x04 value = new JT808_CarDVR_Up_0x04();
            value.RealTime= reader.ReadDateTime6();
            value.PulseCoefficientHighByte = reader.ReadByte();
            value.PulseCoefficientLowByte = reader.ReadByte();
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x04 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x04;
            writer.WriteDateTime6(value.RealTime);
            writer.WriteByte(value.PulseCoefficientHighByte);
            writer.WriteByte(value.PulseCoefficientLowByte);
        }
    }
}
