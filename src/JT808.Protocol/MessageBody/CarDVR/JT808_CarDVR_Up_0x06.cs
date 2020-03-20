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
    /// 采集记录仪状态信号配置信息
    /// 返回：状态信号配置信息
    /// </summary>
    public class JT808_CarDVR_Up_0x06 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId => JT808CarDVRCommandID.采集记录仪状态信号配置信息.ToByteValue();
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
        /// 
        /// </summary>
        public string D0 { get; set; }

        public string D1 { get; set; }

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
        
        public override string Description => "状态信号配置信息";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x06 value = new JT808_CarDVR_Up_0x06();
            value.RealTime = reader.ReadDateTime6();
            value.SignalOperate = reader.ReadByte();
            value.D0 = reader.ReadASCII(10);
            value.D1 = reader.ReadASCII(10);
            value.D2 = reader.ReadASCII(10);
            value.NearLight = reader.ReadASCII(10);
            value.FarLight = reader.ReadASCII(10);
            value.RightTurn = reader.ReadASCII(10);
            value.LeftTurn = reader.ReadASCII(10);
            value.Brake = reader.ReadASCII(10);
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x06 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x06;
            writer.WriteDateTime6(value.RealTime);
            writer.WriteByte(value.SignalOperate);
            writer.WriteASCII(value.D0.PadRight(0));
            writer.WriteASCII(value.D1.PadRight(0));
            writer.WriteASCII(value.D2.PadRight(0));
            writer.WriteASCII(value.NearLight.PadRight(0));
            writer.WriteASCII(value.FarLight.PadRight(0));
            writer.WriteASCII(value.RightTurn.PadRight(0));
            writer.WriteASCII(value.LeftTurn.PadRight(0));
            writer.WriteASCII(value.Brake.PadRight(0));
        }
    }
}
