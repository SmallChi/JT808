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
    /// 设置初始里程
    /// 返回：记录仪初次安装时车辆已行驶的总里程
    /// </summary>
    public class JT808_CarDVR_Down_0xC4 : JT808CarDVRDownBodies, IJT808MessagePackFormatter<JT808_CarDVR_Down_0xC4>, IJT808Analyze
    {
        public override byte CommandId =>  JT808CarDVRCommandID.设置初始里程.ToByteValue();
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime RealTime { get; set; }
        /// <summary>
        /// 初次安装时间
        /// </summary>
        public DateTime FirstInstallTime { get; set; }
        /// <summary>
        /// 初始里程
        /// </summary>
        public string FirstMileage { get; set; }
        /// <summary>
        /// 累计里程
        /// </summary>
        public string TotalMilage { get; set; }
        public override string Description => "车辆识别代号、机动车号牌号码和机动车号牌分类";

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Down_0xC4 value = new JT808_CarDVR_Down_0xC4();
            value.RealTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.RealTime.ToString("yyMMddHHmmss")}]当前时间", value.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.FirstInstallTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.FirstInstallTime.ToString("yyMMddHHmmss")}]初次安装时间", value.FirstInstallTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.FirstMileage = reader.ReadBCD(8);
            writer.WriteString($"[{value.FirstMileage}]初始里程", value.FirstMileage);
            value.TotalMilage = reader.ReadBCD(8);
            writer.WriteString($"[{value.TotalMilage}]累计里程", value.TotalMilage);
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0xC4 value, IJT808Config config)
        {
            writer.WriteDateTime6(value.RealTime);
            writer.WriteDateTime6(value.FirstInstallTime);
            writer.WriteBCD(value.FirstMileage, 8);
            writer.WriteBCD(value.TotalMilage, 8);
        }

        public JT808_CarDVR_Down_0xC4 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0xC4 value = new JT808_CarDVR_Down_0xC4();
            value.RealTime = reader.ReadDateTime6();
            value.FirstInstallTime = reader.ReadDateTime6();
            value.FirstMileage = reader.ReadBCD(8);
            value.TotalMilage = reader.ReadBCD(8);
            return value;
        }
    }
}
