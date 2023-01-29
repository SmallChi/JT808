using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 查询终端参数应答
    /// </summary>
    public class JT808_0x0104 : JT808MessagePackFormatter<JT808_0x0104>, JT808Bodies, IJT808Analyze
    {
        /// <summary>
        /// 0x0104
        /// </summary>
        public ushort MsgId  => 0x0104;
        /// <summary>
        /// 查询终端参数应答
        /// </summary>
        public string Description => "查询终端参数应答";
        /// <summary>
        /// 应答流水号
        /// 查询指定终端参数的流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 应答参数个数
        /// </summary>
        public byte AnswerParamsCount { get; set; }
        /// <summary>
        /// 参数列表
        /// </summary>
        public List<JT808_0x8103_BodyBase> ParamList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0104 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0104 jT808_0x0104 = new JT808_0x0104();
            jT808_0x0104.MsgNum = reader.ReadUInt16();
            jT808_0x0104.AnswerParamsCount = reader.ReadByte();
            for (int i = 0; i < jT808_0x0104.AnswerParamsCount; i++)
            {
                var paramId = reader.ReadVirtualUInt32();//参数ID         
                if (config.JT808_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                {
                    JT808_0x8103_BodyBase value = instance.DeserializeExt<JT808_0x8103_BodyBase>(ref reader, config);
                    if (jT808_0x0104.ParamList == null)
                    {
                        jT808_0x0104.ParamList = new ();
                    }
                    if (value != null)
                    {
                        jT808_0x0104.ParamList.Add(value);
                    }
                }
                else 
                {
                    //对于未能解析的自定义项，过滤其长度，以保证后续解析正常
                    reader.Skip(4);//跳过参数id长度
                    var len = reader.ReadByte();//获取协议长度
                    reader.Skip(len);//跳过协议内容
                }
            }
            return jT808_0x0104;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0104 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteByte(value.AnswerParamsCount);
            foreach (var item in value.ParamList)
            {
                IJT808MessagePackFormatter formatter = config.GetMessagePackFormatterByType(item.GetType());
                formatter.Serialize(ref writer, item, config);
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
            JT808_0x0104 jT808_0x0104 = new JT808_0x0104();
            jT808_0x0104.MsgNum = reader.ReadUInt16();
            jT808_0x0104.AnswerParamsCount = reader.ReadByte();
            writer.WriteNumber($"[{jT808_0x0104.MsgNum.ReadNumber()}]应答流水号", jT808_0x0104.MsgNum);
            writer.WriteNumber($"[{ jT808_0x0104.AnswerParamsCount.ReadNumber()}]应答参数个数", jT808_0x0104.AnswerParamsCount);
            writer.WriteStartArray($"参数列表");
            for (int i = 0; i < jT808_0x0104.AnswerParamsCount; i++)
            {
                writer.WriteStartObject();
                var paramId = reader.ReadVirtualUInt32();//参数ID         
                if (config.JT808_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                {
                    if (instance is IJT808Analyze  analyze) {
                        analyze.Analyze(ref reader, writer, config);
                    }
                }
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
