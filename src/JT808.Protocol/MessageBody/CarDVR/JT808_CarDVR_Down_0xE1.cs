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
    public class JT808_CarDVR_Down_0xE1 : JT808MessagePackFormatter<JT808_CarDVR_Down_0xE1>, JT808CarDVRDownBodies, IJT808Analyze
    {
        /// <summary>
        /// 0xE1
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.enter_mileage_error_measurement.ToByteValue();
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
        /// <summary>
        /// 通过DB9的7脚接收标准速度脉冲测量信号（TTL 电平）
        /// </summary>
        public string Description => "通过DB9的7脚接收标准速度脉冲测量信号（TTL 电平）";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Down_0xE1 value = new JT808_CarDVR_Down_0xE1();
            var hex = reader.ReadVirtualArray(35);
            value.UniqueNumber = reader.ReadASCII(35);
            writer.WriteString($"[{hex.ToArray().ToHexString()}]记录仪唯一性编号", value.UniqueNumber);
            value.PulseCoefficient = reader.ReadUInt16();
            writer.WriteNumber($"[{value.PulseCoefficient.ReadNumber()}]脉冲系数", value.PulseCoefficient);
            value.Speed = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Speed.ReadNumber()}]当前速度", value.Speed);
            value.TotalMileage = reader.ReadUInt32();
            writer.WriteNumber($"[{value.TotalMileage.ReadNumber()}]累计里程", value.TotalMileage);
            value.StatusSignal = reader.ReadByte();
            writer.WriteNumber($"[{value.StatusSignal.ReadNumber()}]状态信号", value.StatusSignal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override  void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0xE1 value, IJT808Config config)
        {
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteASCII(value.UniqueNumber);
            writer.Skip(35 - (writer.GetCurrentPosition() - currentPosition), out var _);
            writer.WriteUInt16(value.PulseCoefficient);
            writer.WriteUInt16(value.Speed);
            writer.WriteUInt32(value.TotalMileage);
            writer.WriteByte(value.StatusSignal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Down_0xE1 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
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
