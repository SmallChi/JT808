using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 删除圆形区域
    /// 0x8601
    /// </summary>
    public class JT808_0x8601 : JT808MessagePackFormatter<JT808_0x8601>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x8601
        /// </summary>
        public ushort MsgId => 0x8601;
        /// <summary>
        /// 删除圆形区域
        /// </summary>
        public string Description => "删除圆形区域";
        /// <summary>
        /// 区域数
        /// 本条消息中包含的区域数，不超过 125 个，多于 125个建议用多条消息，0 为删除所有圆形区域
        /// </summary>
        public byte AreaCount { get; set; }
        /// <summary>
        /// 区域ID集合
        /// </summary>
        public List<uint> AreaIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8601 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8601 jT808_0X8601 = new JT808_0x8601();
            jT808_0X8601.AreaCount = reader.ReadByte();
            jT808_0X8601.AreaIds = new List<uint>();
            for (var i = 0; i < jT808_0X8601.AreaCount; i++)
            {
                jT808_0X8601.AreaIds.Add(reader.ReadUInt32());
            }
            return jT808_0X8601;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8601 value, IJT808Config config)
        {
            if (value.AreaIds != null)
            {
                writer.WriteByte((byte)value.AreaIds.Count);
                foreach (var item in value.AreaIds)
                {
                    writer.WriteUInt32(item);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8601 value = new JT808_0x8601();
            value.AreaCount = reader.ReadByte();
            writer.WriteNumber($"[{ value.AreaCount.ReadNumber()}]区域数", value.AreaCount);
            writer.WriteStartArray("区域ID集合");
            for (var i = 0; i < value.AreaCount; i++)
            {
                writer.WriteStartObject();
                var areaId = reader.ReadUInt32();
                writer.WriteNumber($"[{areaId.ReadNumber()}]Id{i + 1}", areaId);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
