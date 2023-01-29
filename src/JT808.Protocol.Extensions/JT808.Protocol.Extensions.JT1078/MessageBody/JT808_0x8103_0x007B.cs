using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 图像分析报警参数设置
    /// 0x8103_0x007B
    /// </summary>
    public class JT808_0x8103_0x007B : JT808MessagePackFormatter<JT808_0x8103_0x007B>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public uint ParamId { get; set; } = 0x007B;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 车辆核载人数
        /// </summary>
        public byte NuclearLoadNumber { get; set; }
        /// <summary>
        /// 疲劳程度阈值
        /// </summary>
        public byte FatigueThreshold  { get; set; }
        /// <summary>
        /// 图像分析报警参数设置
        /// </summary>
        public string Description => "图像分析报警参数设置";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x007B value = new JT808_0x8103_0x007B();
            value.ParamId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ParamId.ReadNumber()}]参数 ID", value.ParamId);
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]数据长度", value.ParamLength);
            value.NuclearLoadNumber = reader.ReadByte();
            writer.WriteNumber($"[{value.NuclearLoadNumber.ReadNumber()}]车辆核载人数", value.NuclearLoadNumber);
            value.FatigueThreshold = reader.ReadByte();
            writer.WriteNumber($"[{value.FatigueThreshold.ReadNumber()}]疲劳程度阈值", value.FatigueThreshold);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x007B Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007B jT808_0x8103_0x007B = new JT808_0x8103_0x007B();
            jT808_0x8103_0x007B.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x007B.ParamLength = reader.ReadByte();
            jT808_0x8103_0x007B.NuclearLoadNumber = reader.ReadByte();
            jT808_0x8103_0x007B.FatigueThreshold = reader.ReadByte();
            return jT808_0x8103_0x007B;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007B value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.NuclearLoadNumber);
            writer.WriteByte(value.FatigueThreshold);
        }
    }
}
