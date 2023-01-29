using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询区域或线路数据
    /// 0x8608
    /// 2019版本
    /// </summary>
    public class JT808_0x8608 : JT808MessagePackFormatter<JT808_0x8608>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8608
        /// </summary>
        public ushort MsgId => 0x8608;
        /// <summary>
        /// 查询区域或线路数据
        /// </summary>
        public string Description => "查询区域或线路数据";
        /// <summary>
        /// 查询类型
        /// </summary>
        public byte QueryType { get; set; }
        /// <summary>
        /// 查询的区域或线路的ID数量
        /// 0表示查询所有区域或线路数据，大于0表示后面跟随要查询的区域或线路的ID数量
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// 查询的区域或线路的ID
        /// </summary>
        public List<uint> Ids { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8608 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8608 value = new JT808_0x8608();
            value.QueryType = reader.ReadByte();
            value.Count = reader.ReadUInt32();
            if (value.Count > 0)
            {
                value.Ids = new List<uint>();
                for(int i = 0; i < value.Count; i++)
                {
                    value.Ids.Add(reader.ReadUInt32());
                }
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8608 value, IJT808Config config)
        {
            writer.WriteByte(value.QueryType);
            if(value.Ids!=null && value.Ids.Count > 0)
            {
                writer.WriteUInt32((uint)value.Ids.Count);
                foreach(var item in value.Ids)
                {
                    writer.WriteUInt32(item);
                }
            }
            else
            {
                writer.WriteUInt32(0);
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
            JT808_0x8608 value = new JT808_0x8608();
            value.QueryType = reader.ReadByte();
            value.Count = reader.ReadUInt32();
            writer.WriteNumber($"[{ value.QueryType.ReadNumber()}]查询类型", value.QueryType);
            writer.WriteNumber($"[{ value.Count.ReadNumber()}]查询的区域或线路的ID数量", value.Count);
            if (value.Count > 0)
            {
                writer.WriteStartArray("Id列表");
                for (int i = 0; i < value.Count; i++)
                {
                    writer.WriteStartObject();
                    uint id = reader.ReadUInt32();
                    writer.WriteNumber($"[{id.ReadNumber()}]Id{i+1}", id);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
        }
    }
}
