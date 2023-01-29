
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
    /// 设置终端参数
    /// </summary>
    public class JT808_0x8103 : JT808MessagePackFormatter<JT808_0x8103>, JT808Bodies,  IJT808Analyze
    {
        /// <summary>
        /// 0x8103
        /// </summary>
        public ushort MsgId  => 0x8103;
        /// <summary>
        /// 设置终端参数
        /// </summary>
        public string Description => "设置终端参数";
        /// <summary>
        /// 参数总数
        /// </summary>
        internal byte ParamCount
        {
            get
            {
                if (CustomParamList != null)
                {
                    return (byte)(ParamList.Count + CustomParamList.Count);
                }
                return (byte)(ParamList.Count);
            }
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        public List<JT808_0x8103_BodyBase> ParamList { get; set; }
        /// <summary>
        /// 自定义参数列表
        /// </summary>
        public List<JT808_0x8103_CustomBodyBase> CustomParamList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103 value = new JT808_0x8103
            {
                ParamList = new List<JT808_0x8103_BodyBase>(),
                CustomParamList = new List<JT808_0x8103_CustomBodyBase>()
            };
            var paramCount = reader.ReadByte();//参数总数
            try
            {
                for (int i = 0; i < paramCount; i++)
                {
                    var paramId = reader.ReadVirtualUInt32();//参数ID         
                    if (config.JT808_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                    {
                        var bodyValue = instance.DeserializeExt<JT808_0x8103_BodyBase>(ref reader, config);
                        if (bodyValue != null)
                        {
                            value.ParamList.Add(bodyValue);
                        }
                    }
                    else if (config.JT808_0X8103_Custom_Factory.Map.TryGetValue(paramId, out object customInstance))
                    {
                        var bodyValue = customInstance.DeserializeExt<JT808_0x8103_CustomBodyBase>(ref reader, config);
                        if (bodyValue != null)
                        {
                            value.CustomParamList.Add(bodyValue);
                        }
                    }
                }
            }
            catch
            {

            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103 value, IJT808Config config)
        {
            writer.WriteByte(value.ParamCount);
            try
            {
                foreach (var item in value.ParamList)
                {
                    item.SerializeExt(ref writer, item, config);
                }
                if (value.CustomParamList != null)
                {
                    foreach (var item in value.CustomParamList)
                    {
                        item.SerializeExt(ref writer, item, config);
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            var paramCount = reader.ReadByte();//参数总数
            writer.WriteNumber($"[{paramCount.ReadNumber()}]参数总数", paramCount);
            try
            {
                writer.WriteStartArray("参数项");
                for (int i = 0; i < paramCount; i++)
                {
                    var paramId = reader.ReadVirtualUInt32();//参数ID 
                    if (config.JT808_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                    {
                        writer.WriteStartObject();
                        if(instance is IJT808Description description)
                        {
                            writer.WriteString("参数名称", description.Description);
                        }
                        instance.Analyze(ref reader, writer, config);
                        writer.WriteEndObject();
                    }
                    else if (config.JT808_0X8103_Custom_Factory.Map.TryGetValue(paramId, out object customInstance))
                    {
                        writer.WriteStartObject();
                        if (customInstance is IJT808Description description)
                        {
                            writer.WriteString("自定义参数名称", description.Description);
                        }
                        customInstance.Analyze(ref reader, writer, config);
                        writer.WriteEndObject();
                    }
                }
                writer.WriteEndArray();
            }
            catch (Exception ex)
            {
                writer.WriteString($"异常信息", ex.StackTrace);
            }
        }
    }
}
