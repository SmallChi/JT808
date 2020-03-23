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
    /// 进入里程误差测量
    /// 返回：通过 DB9 的 7 脚接收标准速度脉冲测量信号（TTL 电平）
    /// </summary>
    public class JT808_CarDVR_Down_0xE1 : JT808CarDVRDownBodies, IJT808MessagePackFormatter<JT808_CarDVR_Down_0xE1>, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.进入里程误差测量.ToByteValue();
        /// <summary>
        /// 记录仪唯一性编号
        /// </summary>
        public string UniqueNumber { get; set; }
        /// <summary>
        /// 脉冲系数
        /// </summary>
        public ushort PulseCoefficient { get; set; }
        /// <summary>
        /// 当前速度
        /// </summary>
        public ushort Speed { get; set; }
        /// <summary>
        /// 累计里程  单位为米
        /// 单位为 0.1 千米每小时
        /// </summary>
        public uint TotalMileage { get; set; }
        /// <summary>
        /// 状态信号
        /// </summary>
        public byte StatusSignal { get; set; }
        public override string Description => "通过 DB9 的 7 脚接收标准速度脉冲测量信号（TTL 电平）";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0xE1 value, IJT808Config config)
        {
            writer.WriteASCII(value.UniqueNumber.PadRight(35, '0'));
            writer.WriteUInt16(value.PulseCoefficient);
            writer.WriteUInt16(value.Speed);
            writer.WriteUInt32(value.TotalMileage);
            writer.WriteByte(value.StatusSignal);
        }

        public JT808_CarDVR_Down_0xE1 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0xE1 value = new JT808_CarDVR_Down_0xE1();
            value.UniqueNumber = reader.ReadASCII(35);
            value.PulseCoefficient = reader.ReadUInt16();
            value.Speed = reader.ReadUInt16();
            value.TotalMileage = reader.ReadUInt32();
            value.StatusSignal = reader.ReadByte();
            return value;
        }
    }
}
