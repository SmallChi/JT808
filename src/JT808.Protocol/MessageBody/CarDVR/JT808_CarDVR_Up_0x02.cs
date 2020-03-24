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
    /// 采集记录仪实时时间
    /// 返回：实时时间
    /// </summary>
    public class JT808_CarDVR_Up_0x02 : JT808CarDVRUpBodies, IJT808MessagePackFormatter<JT808_CarDVR_Up_0x02>, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集记录仪实时时间.ToByteValue();
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime RealTime { get; set; }
        public override string Description => "实时时间";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Up_0x02 value = new JT808_CarDVR_Up_0x02();
            var hex = reader.ReadVirtualArray(6);
            value.RealTime = reader.ReadDateTime6();
            writer.WriteString($"[{hex.ToArray().ToHexString()}]实时时间", value.RealTime);
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x02 value, IJT808Config config)
        {
            writer.WriteDateTime6(value.RealTime);
        }

        public JT808_CarDVR_Up_0x02 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x02 value = new JT808_CarDVR_Up_0x02();
            value.RealTime = reader.ReadDateTime6();
            return value;
        }
    }
}
