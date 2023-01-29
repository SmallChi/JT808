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
    public class JT808_CarDVR_Down_0xC4 : JT808MessagePackFormatter<JT808_CarDVR_Down_0xC4>, JT808CarDVRDownBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0xC4
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.set_init_mileage.ToByteValue();
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
        /// <summary>
        /// 车辆识别代号、机动车号牌号码和机动车号牌分类
        /// </summary>
        public string Description => "车辆识别代号、机动车号牌号码和机动车号牌分类";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_CarDVR_Down_0xC4 value = new JT808_CarDVR_Down_0xC4();
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.RealTime:yyMMddHHmmss}]当前时间", value.RealTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.FirstInstallTime = reader.ReadDateTime_yyMMddHHmmss();
            writer.WriteString($"[{value.FirstInstallTime:yyMMddHHmmss}]初次安装时间", value.FirstInstallTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.FirstMileage = reader.ReadBCD(8);
            writer.WriteString($"[{value.FirstMileage}]初始里程", value.FirstMileage);
            value.TotalMilage = reader.ReadBCD(8);
            writer.WriteString($"[{value.TotalMilage}]累计里程", value.TotalMilage);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Down_0xC4 value, IJT808Config config)
        {
            writer.WriteDateTime_yyMMddHHmmss(value.RealTime);
            writer.WriteDateTime_yyMMddHHmmss(value.FirstInstallTime);
            writer.WriteBCD(value.FirstMileage, 8);
            writer.WriteBCD(value.TotalMilage, 8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Down_0xC4 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Down_0xC4 value = new JT808_CarDVR_Down_0xC4();
            value.RealTime = reader.ReadDateTime_yyMMddHHmmss();
            value.FirstInstallTime = reader.ReadDateTime_yyMMddHHmmss();
            value.FirstMileage = reader.ReadBCD(8);
            value.TotalMilage = reader.ReadBCD(8);
            return value;
        }
    }
}
