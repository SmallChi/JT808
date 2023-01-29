using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆控制
    /// </summary>
    public class JT808_0x8500 : JT808MessagePackFormatter<JT808_0x8500>, JT808Bodies, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8500
        /// </summary>
        public ushort MsgId => 0x8500;
        /// <summary>
        /// 车辆控制
        /// </summary>
        public string Description => "车辆控制";
        /// <summary>
        /// 控制标志 
        /// 控制指令标志位数据格式
        /// 0：车门解锁；1：车门加锁
        /// 1-7 保留
        /// </summary>
        public byte ControlFlag { get; set; }
        /// <summary>
        /// 控制类型数量
        /// </summary>
        public ushort ControlTypeCount { get; set; }
        /// <summary>
        /// 用于反序列化的时候,由于厂家自定义类型比较多，所以直接用byte数组存储
        /// </summary>
        public byte[] ControlTypeBuffer { get; set; }
        /// <summary>
        /// 用于序列化的时候,由于厂家自定义类型比较多，所以直接用JT808_0x8500_ControlType
        /// </summary>
        public List<JT808_0x8500_ControlType> ControlTypes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8500 value = new JT808_0x8500();
            if (reader.Version == JT808Version.JTT2019)
            {
                value.ControlTypeCount = reader.ReadUInt16();   
                writer.WriteNumber($"[{ value.ControlTypeCount.ReadNumber()}]控制类型数量", value.ControlTypeCount);
                writer.WriteStartArray($"控制类型集合");
                while (reader.ReadCurrentRemainContentLength() > 0)
                {
                    writer.WriteStartObject();
                    var controlTypeId = reader.ReadVirtualUInt16();
                    if (config.JT808_0x8500_2019_Factory.Map.TryGetValue(controlTypeId, out object instance))
                    {
                        instance.Analyze(ref reader, writer, config);
                    }
                    else
                    {
                        value.ControlTypeBuffer = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                        writer.WriteString($"控制类型", value.ControlTypeBuffer.ToHexString());
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
            else
            {
                value.ControlFlag = reader.ReadByte();
                writer.WriteNumber($"[{ value.ControlFlag.ReadNumber()}]控制标志", value.ControlFlag);
                ReadOnlySpan<char> controlFlagBits =string.Join("", Convert.ToString(value.ControlFlag, 2).PadLeft(8, '0').Reverse()).AsSpan();
                writer.WriteStartObject($"控制标志对象[{controlFlagBits.ToString()}]");
                writer.WriteString("[bit1~bit7]保留", controlFlagBits.Slice(1, 7).ToString());
                writer.WriteString("[bit0]", controlFlagBits[0]=='0'? "车门解锁" : "车门加锁");
                writer.WriteEndObject();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8500 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8500 value = new JT808_0x8500();
            if(reader.Version== JT808Version.JTT2019)
            {
                value.ControlTypeCount = reader.ReadUInt16();
                value.ControlTypes = new List<JT808_0x8500_ControlType>();
                while (reader.ReadCurrentRemainContentLength() > 0)
                {
                    var controlTypeId = reader.ReadVirtualUInt16();
                    if (config.JT808_0x8500_2019_Factory.Map.TryGetValue(controlTypeId, out object instance))
                    {
                        var bodyValue = instance.DeserializeExt<JT808_0x8500_ControlType>(ref reader, config);
                        if(bodyValue != null)
                        {
                            value.ControlTypes.Add(bodyValue);
                        }
                    }
                    else
                    {
                        value.ControlTypeBuffer = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                    }
                }
            }
            else
            {
                value.ControlFlag = reader.ReadByte();
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8500 value, IJT808Config config)
        {
            if (writer.Version == JT808Version.JTT2019)
            {
                if(value.ControlTypes!=null && value.ControlTypes.Count > 0)
                {
                    writer.WriteUInt16((ushort)value.ControlTypes.Count);
                    foreach (var item in value.ControlTypes)
                    {
                        item.SerializeExt(ref writer, item, config);
                    }
                }
                else
                {
                    writer.WriteUInt16(value.ControlTypeCount);
                    if(value.ControlTypeBuffer!=null && value.ControlTypeBuffer.Length > 0)
                    {
                        writer.WriteArray(value.ControlTypeBuffer);
                    }
                }
            }
            else
            {
                writer.WriteByte(value.ControlFlag);
            }
        }
    }
}
