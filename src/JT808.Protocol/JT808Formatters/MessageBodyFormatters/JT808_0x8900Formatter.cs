using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body;
using System;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8900Formatter : IJT808Formatter<JT808_0x8900>
    {
        public JT808_0x8900 Deserialize(ReadOnlySpan<byte> bytes,  out int readSize)
        {
            int offset = 0;
            JT808_0x8900 jT808_0X8900 = new JT808_0x8900();
            jT808_0X8900.PassthroughType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            if(JT808_0x8900_BodyBase.JT808_0x8900Method.TryGetValue(jT808_0X8900.PassthroughType,out Type type))
            {
                jT808_0X8900.JT808_0X8900_BodyBase = JT808FormatterResolverExtensions.JT808DynamicDeserialize(JT808FormatterExtensions.GetFormatter(type), bytes.Slice(offset),  out readSize);
            }
            readSize = offset;
            return jT808_0X8900;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x8900 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.PassthroughType);
            object obj = JT808FormatterExtensions.GetFormatter(value.JT808_0X8900_BodyBase.GetType());
            offset = JT808FormatterResolverExtensions.JT808DynamicSerialize(obj, memoryOwner, offset, value);
            return offset;
        }
    }
}
