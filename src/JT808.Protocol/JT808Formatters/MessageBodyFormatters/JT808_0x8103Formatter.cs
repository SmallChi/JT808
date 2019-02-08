using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8103Formatter : IJT808Formatter<JT808_0x8103>
    {
        public JT808_0x8103 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8103 jT808_0x8103 = new JT808_0x8103
            {
                ParamList = new List<JT808_0x8103_BodyBase>()
            };
            var paramCount = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);//参数总数
            for (int i = 0; i < paramCount; i++)
            {
                var paramId = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset);//参数ID         
                int readSubBodySize = 0;
                if (JT808_0x8103_BodyBase.JT808_0x8103Method.TryGetValue(paramId, out Type type))
                {
                    jT808_0x8103.ParamList.Add(JT808FormatterResolverExtensions.JT808DynamicDeserialize(JT808FormatterExtensions.GetFormatter(type), bytes.Slice(offset), out readSubBodySize));
                }
                offset = offset + readSubBodySize;
            }
            readSize = offset;
            return jT808_0x8103;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8103 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.ParamCount);
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
