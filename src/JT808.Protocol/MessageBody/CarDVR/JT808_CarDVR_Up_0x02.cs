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
    public class JT808_CarDVR_Up_0x02 : JT808CarDVRUpBodies, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.采集记录仪实时时间.ToByteValue();
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime RealTime { get; set; }
        public override string Description => "实时时间";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {

        }

        public override JT808CarDVRUpBodies Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x02 value = new JT808_CarDVR_Up_0x02();
            value.RealTime = reader.ReadDateTime6();
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808CarDVRUpBodies jT808CarDVRUpBodies, IJT808Config config)
        {
            JT808_CarDVR_Up_0x02 value = jT808CarDVRUpBodies as JT808_CarDVR_Up_0x02;
            writer.WriteDateTime6(value.RealTime);
        }
    }
}
