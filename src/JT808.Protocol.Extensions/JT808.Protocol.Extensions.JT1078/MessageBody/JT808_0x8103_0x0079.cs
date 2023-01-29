using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 特殊报警录像参数设置
    /// 0x8103_0x0079
    /// </summary>
    public class JT808_0x8103_0x0079 : JT808MessagePackFormatter<JT808_0x8103_0x0079>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 
        /// </summary>
        public uint ParamId { get; set; } = 0x0079;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public byte ParamLength { get; set; } = 3;
        /// <summary>
        /// 特殊报警录像存储阈值
        /// </summary>
        public byte StorageThresholds  { get; set; }
        /// <summary>
        /// 特殊报警录像持续时间
        /// </summary>
        public byte Duration { get; set; }
        /// <summary>
        /// 特殊报警标识起始时间
        /// 分钟min
        /// </summary>
        public byte BeginMinute { get; set; }
        /// <summary>
        /// 特殊报警录像参数设置
        /// </summary>
        public string Description => "特殊报警录像参数设置";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0079 value = new JT808_0x8103_0x0079();
            value.ParamId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ParamId.ReadNumber()}]参数 ID", value.ParamId);
            value.ParamLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]数据长度", value.ParamLength);
            value.StorageThresholds = reader.ReadByte();
            writer.WriteNumber($"[{value.StorageThresholds.ReadNumber()}]特殊报警录像存储阈值（百分比）", value.StorageThresholds);
            value.Duration = reader.ReadByte();
            writer.WriteNumber($"[{value.Duration.ReadNumber()}]特殊报警录像持续时间（分钟）", value.Duration);
            value.BeginMinute = reader.ReadByte();
            writer.WriteNumber($"[{value.BeginMinute.ReadNumber()}]特殊报警标识起始时间（分钟）", value.BeginMinute);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0079 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0079 jT808_0x8103_0x0079 = new JT808_0x8103_0x0079();
            jT808_0x8103_0x0079.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0079.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0079.StorageThresholds = reader.ReadByte();
            jT808_0x8103_0x0079.Duration = reader.ReadByte();
            jT808_0x8103_0x0079.BeginMinute = reader.ReadByte();
            return jT808_0x8103_0x0079;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0079 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.StorageThresholds);
            writer.WriteByte(value.Duration);
            writer.WriteByte(value.BeginMinute);
        }
    }
}
