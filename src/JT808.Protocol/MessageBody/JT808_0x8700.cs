using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 行驶记录数据采集命令
    /// </summary>
    public class JT808_0x8700 : JT808MessagePackFormatter<JT808_0x8700>, JT808Bodies,  IJT808_2019_Version, IJT808Analyze
    {
        /// <summary>
        /// 0x8700
        /// </summary>
        public ushort MsgId => 0x8700;
        /// <summary>
        /// 行驶记录数据采集命令
        /// </summary>
        public string Description => "行驶记录数据采集命令";
        /// <summary>
        /// 命令Id
        /// </summary>
        public byte CommandId { get; set; }
        /// <summary>
        /// 行车记录仪下行数据包
        /// </summary>
        public JT808CarDVRDownPackage JT808CarDVRDownPackage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8700 value = new JT808_0x8700();
            writer.WriteStartObject("行驶记录数据采集命令");
            value.CommandId = reader.ReadByte();
            writer.WriteString($"[{value.CommandId.ReadNumber()}]命令字", ((JT808CarDVRCommandID)value.CommandId).ToString());
            writer.WriteStartObject(((JT808CarDVRCommandID)value.CommandId).ToString());
            JT808CarDVRSerializer.JT808CarDVRDownPackage.Analyze(ref reader, writer, config);
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8700 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8700 value = new JT808_0x8700();
            value.CommandId = reader.ReadByte();
            if (reader.ReadCurrentRemainContentLength() > 0)
                value.JT808CarDVRDownPackage = JT808CarDVRSerializer.JT808CarDVRDownPackage.Deserialize(ref reader, config);
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8700 value, IJT808Config config)
        {
            writer.WriteByte(value.CommandId);
            if (value.JT808CarDVRDownPackage != default)
                JT808CarDVRSerializer.JT808CarDVRDownPackage.Serialize(ref writer, value.JT808CarDVRDownPackage, config);
        }
    }
}
