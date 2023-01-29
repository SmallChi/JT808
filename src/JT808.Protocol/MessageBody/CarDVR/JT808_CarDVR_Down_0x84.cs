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
    /// 设置状态量配置信息
    /// 返回：状态量配置信息
    /// </summary>
    public class JT808_CarDVR_Down_0x84 : JT808MessagePackFormatter<JT808_CarDVR_Down_0x84>, JT808CarDVRDownBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x84
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.set_state_quantity_configuration_information.ToByteValue();
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime RealTime { get; set; }
        /// <summary>
        /// 信号字节个数
        /// 单位字节的 D7～D0（由高到低）分别对应 8 个状态信号， 1 表示有操作，0表示无操作
        /// </summary>
        public byte SignalOperate { get; set; }
        /// <summary>
        /// D0
        /// </summary>
        public string D0 { get; set; }
        /// <summary>
        /// D1
        /// </summary>
        public string D1 { get; set; }
        /// <summary>
        /// D2
        /// </summary>
        public string D2 { get; set; }
        /// <summary>
        /// 近光 D3
        /// 10个字节，未使用或不足时，补0
        /// </summary>
        public string NearLight { get; set; }
        /// <summary>
        /// 远光 D4
        /// 10个字节，未使用或不足时，补0
        /// </summary>
        public string FarLight { get; set; }
        /// <summary>
        /// 右转向 D5
        /// 10个字节，未使用或不足时，补0
        /// </summary>
        public string RightTurn { get; set; }
        /// <summary>
        /// 左转向 D6
        /// 10个字节，未使用或不足时，补0
        /// </summary>
        public string LeftTurn { get; set; }
        /// <summary>
        /// 制动 D7
        /// 10个字节，未使用或不足时，补0
        /// </summary>
        public string Brake { get; set; }
        /// <summary>
        /// 状态量配置信息
        /// </summary>
        public string Description => "状态量配置信息";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Down_0x84 value = new JT808_CarDVR_Down_0x84();
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.RealTime:yyMMddHHmmss}]实时时间", value.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.SignalOperate = reader.ReadByte();
            writer.WriteNumber($"[{value.SignalOperate.ReadNumber()}]信号个数", value.SignalOperate);
            var display = reader.ReadVirtualArray(10);
            value.D0 = reader.ReadString(10);
            writer.WriteString($"[{display.ToArray().ToHexString()}]D0", value.D0);
            display = reader.ReadVirtualArray(10);
            value.D1 = reader.ReadString(10);
            writer.WriteString($"[{display.ToArray().ToHexString()}]D1", value.D1);
            display = reader.ReadVirtualArray(10);
            value.D2 = reader.ReadString(10);
            writer.WriteString($"[{display.ToArray().ToHexString()}]D2", value.D2);
            display = reader.ReadVirtualArray(10);
            value.NearLight = reader.ReadString(10);
            writer.WriteString($"[{display.ToArray().ToHexString()}]近光灯", value.NearLight);
            display = reader.ReadVirtualArray(10);
            value.FarLight = reader.ReadString(10);
            writer.WriteString($"[{display.ToArray().ToHexString()}]远光灯", value.FarLight);
            display = reader.ReadVirtualArray(10);
            value.RightTurn = reader.ReadString(10);
            writer.WriteString($"[{display.ToArray().ToHexString()}]右转向", value.RightTurn);
            display = reader.ReadVirtualArray(10);
            value.LeftTurn = reader.ReadString(10);
            writer.WriteString($"[{display.ToArray().ToHexString()}]左转向", value.LeftTurn);
            display = reader.ReadVirtualArray(10);
            value.Brake = reader.ReadString(10);
            writer.WriteString($"[{display.ToArray().ToHexString()}]制动", value.Brake);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0x84 value, IJT808Config config)
        {
            writer.WriteDateTime_yyMMddHHmmss(value.RealTime);
            writer.WriteByte(value.SignalOperate);
            var currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.D0);
            writer.Skip(10 - (writer.GetCurrentPosition() - currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.D1);
            writer.Skip(10 - (writer.GetCurrentPosition() - currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.D2);
            writer.Skip(10 - (writer.GetCurrentPosition() - currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.NearLight);
            writer.Skip(10 - (writer.GetCurrentPosition() - currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.FarLight);
            writer.Skip(10 - (writer.GetCurrentPosition() - currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.RightTurn);
            writer.Skip(10 - (writer.GetCurrentPosition() - currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.LeftTurn);
            writer.Skip(10 - (writer.GetCurrentPosition() - currentPosition), out var _);
            currentPosition = writer.GetCurrentPosition();
            writer.WriteString(value.Brake);
            writer.Skip(10 - (writer.GetCurrentPosition() - currentPosition), out var _);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Down_0x84 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0x84 value = new JT808_CarDVR_Down_0x84();
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            value.SignalOperate = reader.ReadByte();
            value.D0 = reader.ReadString(10);
            value.D1 = reader.ReadString(10);
            value.D2 = reader.ReadString(10);
            value.NearLight = reader.ReadString(10);
            value.FarLight = reader.ReadString(10);
            value.RightTurn = reader.ReadString(10);
            value.LeftTurn = reader.ReadString(10);
            value.Brake = reader.ReadString(10);
            return value;
        }
    }
}
