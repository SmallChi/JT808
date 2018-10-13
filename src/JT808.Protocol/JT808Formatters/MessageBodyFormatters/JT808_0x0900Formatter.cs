using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body;
using System;
using System.Buffers;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0900Formatter : IJT808Formatter<JT808_0x0900>
    {
        public JT808_0x0900 Deserialize(ReadOnlySpan<byte> bytes,  out int readSize)
        {
            int offset = 0;
            JT808_0x0900 jT808_0X0900 = new JT808_0x0900();
            jT808_0X0900.PassthroughType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            Type jT808PassthroughType;
            if (JT808_0x0900_BodyBase.JT808_0x0900Method.TryGetValue(jT808_0X0900.PassthroughType, out jT808PassthroughType))
            {
                object obj = JT808FormatterExtensions.GetFormatter(jT808PassthroughType);
                ReadOnlySpan<byte> passthroughTypeBuffer = bytes.Slice(offset, bytes.Length - offset);
                dynamic objImpl = JT808FormatterResolverExtensions.JT808DynamicDeserialize(obj, passthroughTypeBuffer, out readSize);
                jT808_0X0900.JT808_0x0900_BodyBase = objImpl;
            }
            readSize = bytes.Length;//读取整个数据体
            return jT808_0X0900;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT808_0x0900 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(memoryOwner, offset, value.PassthroughType);
            object obj = JT808FormatterExtensions.GetFormatter(value.JT808_0x0900_BodyBase.GetType());
            offset = JT808FormatterResolverExtensions.JT808DynamicSerialize(obj, memoryOwner, offset, value.JT808_0x0900_BodyBase);
            return offset;
        }
    }
}
