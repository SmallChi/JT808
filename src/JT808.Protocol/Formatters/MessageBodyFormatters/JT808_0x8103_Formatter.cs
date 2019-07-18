using JT808.Protocol.Extensions;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8103_Formatter : IJT808MessagePackFormatter<JT808_0x8103>
    {
        public JT808_0x8103 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103 jT808_0x8103 = new JT808_0x8103
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
                    if (config.JT808_0X8103_Factory.ParamMethods.TryGetValue(paramId, out Type type))
                    {
                        object attachImplObj = config.GetMessagePackFormatterByType(type);
                        dynamic attachImpl = JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(attachImplObj, ref reader, config);
                        jT808_0x8103.ParamList.Add(attachImpl);
                    }
                    else if (config.JT808_0X8103_Custom_Factory.ParamMethods.TryGetValue(paramId, out Type customType))
                    {
                        object attachImplObj = config.GetMessagePackFormatterByType(customType);
                        dynamic attachImpl = JT808MessagePackFormatterResolverExtensions.JT808DynamicDeserialize(attachImplObj, ref reader, config);
                        jT808_0x8103.CustomParamList.Add(attachImpl);
                    }
                }
            }
            catch (Exception ex) { }
            return jT808_0x8103;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103 value, IJT808Config config)
        {
            writer.WriteByte(value.ParamCount);
            try
            {
                foreach (var item in value.ParamList)
                {
                    object attachImplObj = config.GetMessagePackFormatterByType(item.GetType());
                    JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(attachImplObj, ref writer, item, config);
                }
                if (value.CustomParamList != null)
                {
                    foreach (var item in value.CustomParamList)
                    {
                        object attachImplObj = config.GetMessagePackFormatterByType(item.GetType());
                        JT808MessagePackFormatterResolverExtensions.JT808DynamicSerialize(attachImplObj, ref writer, item, config);
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}
