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
    /// 采集指定的参数修改记录
    /// 返回：符合条件的参数修改记录
    /// </summary>
    public class JT808_CarDVR_Up_0x14 : JT808MessagePackFormatter<JT808_CarDVR_Up_0x14>, JT808CarDVRUpBodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x14
        /// </summary>
        public byte CommandId =>  JT808CarDVRCommandID.collect_specified_modify_parameters_records.ToByteValue();
        /// <summary>
        /// 请求发送指定的时间范围内 N 个单位数据块的数据（N≥1）
        /// </summary>
        public List<JT808_CarDVR_Up_0x14_ParameterModify> JT808_CarDVR_Up_0x14_ParameterModifys { get; set; }
        /// <summary>
        /// 符合条件的参数修改记录
        /// </summary>
        public string Description => "符合条件的参数修改记录";
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
                JT808_CarDVR_Up_0x14_ParameterModify jT808_CarDVR_Up_0x14_ParameterModify = new JT808_CarDVR_Up_0x14_ParameterModify();
                writer.WriteStartObject();
                writer.WriteStartObject($"指定的结束时间之前最近的第{i+1}条参数修改记录");
                var hex = reader.ReadVirtualArray(6);
                jT808_CarDVR_Up_0x14_ParameterModify.EventTime = reader.ReadDateTime_yyMMddHHmmss();
                writer.WriteString($"[{hex.ToArray().ToHexString()}]事件发生时间", jT808_CarDVR_Up_0x14_ParameterModify.EventTime);
                jT808_CarDVR_Up_0x14_ParameterModify.EventType = reader.ReadByte();
                writer.WriteString($"[{  jT808_CarDVR_Up_0x14_ParameterModify.EventType.ReadNumber()}]事件类型", ((JT808CarDVRCommandID)jT808_CarDVR_Up_0x14_ParameterModify.EventType).ToString());
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_CarDVR_Up_0x14 value, IJT808Config config)
        {
            foreach (var parameterModify in value.JT808_CarDVR_Up_0x14_ParameterModifys)
            {
                writer.WriteDateTime_yyMMddHHmmss(parameterModify.EventTime);
                writer.WriteByte(parameterModify.EventType);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_CarDVR_Up_0x14 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_CarDVR_Up_0x14 value = new JT808_CarDVR_Up_0x14();
            value.JT808_CarDVR_Up_0x14_ParameterModifys = new List<JT808_CarDVR_Up_0x14_ParameterModify>();
            var count = (reader.ReadCurrentRemainContentLength() - 1) / 7;//记录块个数, -1 去掉校验位
            for (int i = 0; i < count; i++)
            {
                JT808_CarDVR_Up_0x14_ParameterModify jT808_CarDVR_Up_0x14_ParameterModify = new JT808_CarDVR_Up_0x14_ParameterModify();
                jT808_CarDVR_Up_0x14_ParameterModify.EventTime = reader.ReadDateTime_yyMMddHHmmss();
                jT808_CarDVR_Up_0x14_ParameterModify.EventType = reader.ReadByte();
                value.JT808_CarDVR_Up_0x14_ParameterModifys.Add(jT808_CarDVR_Up_0x14_ParameterModify);
            }
            return value;
        }
    }
    /// <summary>
    /// 单位参数修改记录数据块格式
    /// </summary>
    public class JT808_CarDVR_Up_0x14_ParameterModify
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
