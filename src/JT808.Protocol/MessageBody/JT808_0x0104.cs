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
    public class JT808_0x0104 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0104>, IJT808Analyze
    {
        public override ushort MsgId { get; } = 0x0104;
        public override string Description => "查询终端参数应答";
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
        public IList<JT808_0x8103_BodyBase> ParamList { get; set; }

        public JT808_0x0104 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0104 jT808_0x0104 = new JT808_0x0104();
            jT808_0x0104.MsgNum = reader.ReadUInt16();
            jT808_0x0104.AnswerParamsCount = reader.ReadByte();
            for (int i = 0; i < jT808_0x0104.AnswerParamsCount; i++)
            {
                var paramId = reader.ReadVirtualUInt32();//参数ID         
                if (config.JT808_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                {
                    if (jT808_0x0104.ParamList != null)
                    {
                        jT808_0x0104.ParamList.Add(JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(instance, ref reader, config));
                    }
                    else
                    {
                        jT808_0x0104.ParamList = new List<JT808_0x8103_BodyBase> { JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(instance,  ref reader,  config) };
                    }
                }
            }
            return jT808_0x0104;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0104 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteByte(value.AnswerParamsCount);
            foreach (var item in value.ParamList)
            {
                object obj = config.GetMessagePackFormatterByType(item.GetType());
                JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(obj, ref writer, item, config);
            }
        }

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
