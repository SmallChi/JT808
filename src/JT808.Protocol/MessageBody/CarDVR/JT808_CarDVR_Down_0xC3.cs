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
    public class JT808_CarDVR_Down_0xC3 : JT808CarDVRDownBodies, IJT808MessagePackFormatter<JT808_CarDVR_Down_0xC3>, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.设置记录仪脉冲系数.ToByteValue();
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
        public override string Description => "记录仪脉冲系数";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }


        public void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0xC3 value, IJT808Config config)
        {
            writer.WriteDateTime6(value.RealTime);
            writer.WriteByte(value.PulseCoefficientHighByte);
            writer.WriteByte(value.PulseCoefficientLowByte);
        }

        public JT808_CarDVR_Down_0xC3 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0xC3 value = new JT808_CarDVR_Down_0xC3();
            value.RealTime = reader.ReadDateTime6();
            value.PulseCoefficientHighByte = reader.ReadByte();
            value.PulseCoefficientLowByte = reader.ReadByte();
            return value;
        }
    }
}
