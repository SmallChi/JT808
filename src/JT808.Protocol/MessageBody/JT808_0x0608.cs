using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询区域或线路数据应答
    /// </summary>
    public class JT808_0x0608 : JT808MessagePackFormatter<JT808_0x0608>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x0608
        /// </summary>
        public ushort MsgId  => 0x0608;
        /// <summary>
        /// 查询区域或线路数据应答
        /// </summary>
        public string Description => "查询区域或线路数据应答";
        /// <summary>
        /// 查询类型
        /// </summary>
        public byte QueryType { get; set; }
        /// <summary>
        /// 查询的区域或线路的ID数量
        /// </summary>
        public uint Count { get; set; }
        /// <summary>
        /// 查询的区域或线路的ID
        /// </summary>
        public List<uint> Ids { get; set; }
        /// <summary>
        /// 设置圆形区域 
        /// 查询类型为1
        /// </summary>
        public List<JT808_0x8600> JT808_0x8600s { get; set; }
        /// <summary>
        /// 设置矩形区域
        /// 查询类型为2
        /// </summary>
        public List<JT808_0x8602> JT808_0x8602s { get; set; }
        /// <summary>
        /// 设置多边形区域
        /// 查询类型为3
        /// </summary>
        public List<JT808_0x8604> JT808_0x8604s { get; set; }
        /// <summary>
        /// 设置路线
        /// 查询类型为4
        /// </summary>
        public List<JT808_0x8606> JT808_0x8606s { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0608 value = new JT808_0x0608();
            value.QueryType = reader.ReadByte();
            writer.WriteNumber($"[{value.QueryType.ReadNumber()}]查询类型", value.QueryType);
            value.Count = reader.ReadUInt32();
            writer.WriteNumber($"[{value.Count.ReadNumber()}]查询的区域或线路的ID数量", value.Count);
            if (value.Count > 0)
            {
                switch (value.QueryType)
                {
                    case 1:
                        writer.WriteStartArray("设置圆形区域");
                        for (int i = 0; i < value.Count; i++)
                        {
                            if (config.FormatterFactory.FormatterDict.TryGetValue(typeof(JT808_0x8600).GUID, out IJT808MessagePackFormatter instance))
                            {
                                writer.WriteStartObject();
                                instance.Analyze(ref reader, writer, config);
                                writer.WriteEndObject();
                            }
                        }
                        writer.WriteEndArray();
                        break;
                    case 2:
                        writer.WriteStartArray("设置矩形区域");
                        for (int i = 0; i < value.Count; i++)
                        {
                            if (config.FormatterFactory.FormatterDict.TryGetValue(typeof(JT808_0x8602).GUID, out IJT808MessagePackFormatter instance))
                            {
                                writer.WriteStartObject();
                                instance.Analyze(ref reader, writer, config);
                                writer.WriteEndObject();
                            }
                        }
                        writer.WriteEndArray();
                        break;
                    case 3:
                        writer.WriteStartArray("设置多边形区域");
                        for (int i = 0; i < value.Count; i++)
                        {
                            if (config.FormatterFactory.FormatterDict.TryGetValue(typeof(JT808_0x8604).GUID, out IJT808MessagePackFormatter instance))
                            {
                                writer.WriteStartObject();
                                instance.Analyze(ref reader, writer, config);
                                writer.WriteEndObject();
                            }
                        }
                        writer.WriteEndArray();
                        break;
                    case 4:
                        writer.WriteStartArray("设置路线");
                        for (int i = 0; i < value.Count; i++)
                        {
                            if (config.FormatterFactory.FormatterDict.TryGetValue(typeof(JT808_0x8606).GUID, out IJT808MessagePackFormatter instance))
                            {
                                writer.WriteStartObject();
                                instance.Analyze(ref reader, writer, config);
                                writer.WriteEndObject();
                            }
                        }
                        writer.WriteEndArray();
                        break;
                    default:
                        writer.WriteStartArray("线路Id集合");
                        for (int i = 0; i < value.Count; i++)
                        {
                            writer.WriteStartObject();
                            var routeId = reader.ReadUInt32();
                            writer.WriteNumber($"[{routeId.ReadNumber()}]Id{i+1}", routeId);
                            writer.WriteEndObject();
                        }
                        writer.WriteEndArray();
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0608 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0608 value = new JT808_0x0608();
            value.QueryType = reader.ReadByte();
            value.Count = reader.ReadUInt32();
            if (value.Count > 0)
            {
                switch (value.QueryType)
                {
                    case 1:
                        value.JT808_0x8600s = new List<JT808_0x8600>();
                        for (int i = 0; i < value.Count; i++)
                        {
                            if (config.FormatterFactory.FormatterDict.TryGetValue(typeof(JT808_0x8600).GUID, out IJT808MessagePackFormatter instance))
                            {
                                object attachImpl = instance.Deserialize(ref reader, config);
                                if (attachImpl != null)
                                {
                                    value.JT808_0x8600s.Add((JT808_0x8600)attachImpl);
                                }
                            }         
                        }
                        break;
                    case 2:
                        value.JT808_0x8602s = new List<JT808_0x8602>();
                        for (int i = 0; i < value.Count; i++)
                        {
                            if (config.FormatterFactory.FormatterDict.TryGetValue(typeof(JT808_0x8602).GUID, out IJT808MessagePackFormatter instance))
                            {
                                object attachImpl = instance.Deserialize(ref reader, config);
                                if (attachImpl != null)
                                {
                                    value.JT808_0x8602s.Add((JT808_0x8602)attachImpl);
                                }
                            }
                        }
                        break;
                    case 3:
                        value.JT808_0x8604s = new List<JT808_0x8604>();
                        for (int i = 0; i < value.Count; i++)
                        {
                            if (config.FormatterFactory.FormatterDict.TryGetValue(typeof(JT808_0x8604).GUID, out IJT808MessagePackFormatter instance))
                            {
                                object attachImpl = instance.Deserialize(ref reader, config);
                                if (attachImpl != null)
                                {
                                    value.JT808_0x8604s.Add((JT808_0x8604)attachImpl);
                                }
                            }
                        }
                        break;
                    case 4:
                        value.JT808_0x8606s = new List<JT808_0x8606>();
                        for (int i = 0; i < value.Count; i++)
                        {
                            if (config.FormatterFactory.FormatterDict.TryGetValue(typeof(JT808_0x8606).GUID, out IJT808MessagePackFormatter instance))
                            {
                                object attachImpl = instance.Deserialize(ref reader, config);
                                if (attachImpl != null)
                                {
                                    value.JT808_0x8606s.Add((JT808_0x8606)attachImpl);
                                }
                            }
                        }
                        break;
                    default:
                        value.Ids = new List<uint>();
                        for (int i = 0; i < value.Count; i++)
                        {
                            value.Ids.Add(reader.ReadUInt32());
                        }
                        break;
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
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0608 value, IJT808Config config)
        {
            writer.WriteByte(value.QueryType);
            switch (value.QueryType)
            {
                case 1:
                    if (value.JT808_0x8600s != null && value.JT808_0x8600s.Count > 0)
                    {
                        writer.WriteUInt32((uint)value.JT808_0x8600s.Count);
                        foreach (var item in value.JT808_0x8600s)
                        {
                            item.SerializeExt(ref writer, item, config);
                        }
                    }
                    break;
                case 2:
                    if (value.JT808_0x8602s != null && value.JT808_0x8602s.Count > 0)
                    {
                        writer.WriteUInt32((uint)value.JT808_0x8602s.Count);
                        foreach (var item in value.JT808_0x8602s)
                        {
                            item.SerializeExt(ref writer, item, config);
                        }
                    }
                    break;
                case 3:
                    if (value.JT808_0x8604s != null && value.JT808_0x8604s.Count > 0)
                    {
                        writer.WriteUInt32((uint)value.JT808_0x8604s.Count);
                        foreach (var item in value.JT808_0x8604s)
                        {
                            item.SerializeExt(ref writer, item, config);
                        }
                    }
                    break;
                case 4:
                    if(value.JT808_0x8606s != null && value.JT808_0x8606s.Count > 0)
                    {
                        writer.WriteUInt32((uint)value.JT808_0x8606s.Count);
                        foreach (var item in value.JT808_0x8606s)
                        {
                            item.SerializeExt(ref writer, item, config);
                        }
                    }
                    break;
                default:
                    if (value.Ids != null && value.Ids.Count > 0)
                    {
                        writer.WriteUInt32((uint)value.Ids.Count);
                        foreach (var item in value.Ids)
                        {
                            writer.WriteUInt32(item);
                        }
                    }
                    else
                    {
                        writer.WriteUInt32(0);
                    }
                    break;
            }
        }
    }
}
