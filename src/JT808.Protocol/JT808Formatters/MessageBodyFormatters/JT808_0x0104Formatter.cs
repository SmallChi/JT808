using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0104Formatter : IJT808Formatter<JT808_0x0104>
    {
        public JT808_0x0104 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x0104 jT808_0x0104 = new JT808_0x0104
            {
                MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset),
                AnswerParamsCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            for (int i = 0; i < jT808_0x0104.AnswerParamsCount; i++)
            {
                var paramId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);//参数ID         
                int readSubBodySize = 0;
                if (JT808_0x8103_BodyBase.JT808_0x8103Method.TryGetValue(paramId, out Type type))
                {
                    if (jT808_0x0104.ParamList != null)
                    {
                        jT808_0x0104.ParamList.Add(JT808FormatterResolverExtensions.JT808DynamicDeserialize(JT808FormatterExtensions.GetFormatter(type), bytes.Slice(offset), out readSubBodySize));
                    }
                    else
                    {
                        jT808_0x0104.ParamList = new List<JT808_0x8103_BodyBase> { JT808FormatterResolverExtensions.JT808DynamicDeserialize(JT808FormatterExtensions.GetFormatter(type), bytes.Slice(offset), out readSubBodySize) };
                    }
                }
                offset = offset + readSubBodySize;
            }
            readSize = offset;
            return jT808_0x0104;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0104 value)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, value.MsgNum);
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AnswerParamsCount);
            foreach (var item in value.ParamList)
            {
                offset += JT808BinaryExtensions.WriteUInt32Little(bytes, offset, item.ParamId);
                object obj = JT808FormatterExtensions.GetFormatter(item.GetType());
                offset = JT808FormatterResolverExtensions.JT808DynamicSerialize(obj, ref bytes, offset, item);
            }
            return offset;
        }
    }
}
