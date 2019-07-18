using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x0104_Formatter : IJT808MessagePackFormatter<JT808_0x0104>
    {
        public JT808_0x0104 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0104 jT808_0x0104 = new JT808_0x0104();
            jT808_0x0104.MsgNum = reader.ReadUInt16();
            jT808_0x0104.AnswerParamsCount = reader.ReadByte();
            for (int i = 0; i < jT808_0x0104.AnswerParamsCount; i++)
            {
                var paramId = reader.ReadVirtualUInt32();//参数ID         
                if (config.JT808_0X8103_Factory.ParamMethods.TryGetValue(paramId, out Type type))
                {
                    if (jT808_0x0104.ParamList != null)
                    {
                        jT808_0x0104.ParamList.Add(JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(
                            config.GetMessagePackFormatterByType(type), ref reader, config));
                    }
                    else
                    {
                        jT808_0x0104.ParamList = new List<JT808_0x8103_BodyBase> { JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(
                            config.GetMessagePackFormatterByType(type),  ref reader,  config) };
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
                JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(obj, ref writer,item, config);
            }
        }
    }
}
