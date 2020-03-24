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
    /// 设置记录仪初次安装日期
    /// 返回：初次安装日期
    /// </summary>
    public class JT808_CarDVR_Down_0x83 : JT808CarDVRDownBodies, IJT808MessagePackFormatter<JT808_CarDVR_Down_0x83>, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.设置记录仪初次安装日期.ToByteValue();
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime RealTime { get; set; }
        public override string Description => "初次安装日期";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Down_0x83 value = new JT808_CarDVR_Down_0x83();
            value.RealTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.RealTime.ToString("yyMMddHHmmss")}]初次安装日期", value.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0x83 value, IJT808Config config)
        {
            writer.WriteDateTime6(value.RealTime);
        }

        public JT808_CarDVR_Down_0x83 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0x83 value = new JT808_CarDVR_Down_0x83();
            value.RealTime = reader.ReadDateTime6();
            return value;
        }
    }
}
