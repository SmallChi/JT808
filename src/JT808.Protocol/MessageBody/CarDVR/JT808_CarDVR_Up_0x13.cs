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
    /// 采集指定的外部供电记录
    /// 返回：符合条件的供电记录
    /// </summary>
    public class JT808_CarDVR_Up_0x13 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x13>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x13
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.collect_specified_external_power_supply_records.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x13_ExternalPowerSupply> JT808_CarDVR_Up_0x13_ExternalPowerSupplys { get; set; }
        /// <summary>
        /// 符合条件的供电记录
        /// </summary>
        public string Description => "符合条件的供电记录";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            writer.WriteStartArray("请求发送指定的时间范围内 N 个单位数据块的数据");
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 7;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x13_ExternalPowerSupply jT808_CarDVR_Up_0x13_ExternalPowerSupply = new JT808_CarDVR_Up_0x13_ExternalPowerSupply();
                writer.WriteStartObject();
                writer.WriteStartObject($"从指定的结束时间之前最近的第{i+1}条外部电源记录");
                var hex = reader.ReadVirtualArray(6);
                jT808_CarDVR_Up_0x13_ExternalPowerSupply.EventTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{hex.ToArray().ToHexString()}]事件发生时间", jT808_CarDVR_Up_0x13_ExternalPowerSupply.EventTime);
                jT808_CarDVR_Up_0x13_ExternalPowerSupply.EventType = reader.ReadByte();
                writer.WriteString($"[{  jT808_CarDVR_Up_0x13_ExternalPowerSupply.EventType.ReadNumber()}]事件类型", EventTypeDisplay(jT808_CarDVR_Up_0x13_ExternalPowerSupply.EventType));
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            static string EventTypeDisplay(byte eventType) {
                if (eventType == 1)
                {
                    return "供电";
                }
                else {
                    return "断电";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x13 value, IJT808Config config)
        {
            foreach (var externalPowerSupply in value.JT808_CarDVR_Up_0x13_ExternalPowerSupplys)
            {
                writer.WriteDateTime_yyMMddHHmmss(externalPowerSupply.EventTime);
                writer.WriteByte(externalPowerSupply.EventType);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x13 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x13 value = new JT808_CarDVR_Up_0x13();
            value.JT808_CarDVR_Up_0x13_ExternalPowerSupplys = new List<JT808_CarDVR_Up_0x13_ExternalPowerSupply>();
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 7;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x13_ExternalPowerSupply jT808_CarDVR_Up_0x13_ExternalPowerSupply = new JT808_CarDVR_Up_0x13_ExternalPowerSupply();
                jT808_CarDVR_Up_0x13_ExternalPowerSupply.EventTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_CarDVR_Up_0x13_ExternalPowerSupply.EventType = reader.ReadByte();
                value.JT808_CarDVR_Up_0x13_ExternalPowerSupplys.Add(jT808_CarDVR_Up_0x13_ExternalPowerSupply);
            }
            return value;
        }
    }
    /// <summary>
    /// 单位记录仪外部供电记录数据块格式
    /// </summary>
    public class JT808_CarDVR_Up_0x13_ExternalPowerSupply
    {
        /// <summary>
        ///  事件发生时间
        /// </summary>
        public DateTime EventTime { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public byte EventType { get; set; }
    }
}
