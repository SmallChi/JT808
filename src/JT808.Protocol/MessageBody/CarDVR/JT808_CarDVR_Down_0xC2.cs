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
    /// 设置记录仪时间
    /// 返回：北京时间的日期、时钟
    /// </summary>
    public class JT808_CarDVR_Down_0xC2 : JT808MessagePackFormatter<JT808_CarDVR_Down_0xC2>, JT808CarDVRDownBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0xC2
        /// </summary>
        public byte CommandId => JT808CarDVRCommandID.set_recorder_time.ToByteValue();
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime RealTime { get; set; }
        /// <summary>
        /// 北京时间的日期、时钟
        /// </summary>
        public string Description => "北京时间的日期、时钟";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Down_0xC2 value = new JT808_CarDVR_Down_0xC2();
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.RealTime:yyMMddHHmmss}]实时时间", value.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 北京时间的日期、时钟
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0xC2 value, IJT808Config config)
        {
            writer.WriteDateTime_yyMMddHHmmss(value.RealTime);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Down_0xC2 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0xC2 value = new JT808_CarDVR_Down_0xC2();
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            return value;
        }
    }
}
