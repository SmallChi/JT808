using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0701Formatter : IJT808Formatter<JT808_0x0701>
    {
        public JT808_0x0701 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            if (JT808_0x0701.JT808_0x0701Body.BodyImpl == null) throw new JT808Exception(JT808ErrorCode.NotImplType, $"Not Impl {nameof(JT808_0x0701.JT808_0x0701Body)} class");
            int offset = 0;
            JT808_0x0701 jT808_0X0701 = new JT808_0x0701
            {
                ElectronicWaybillLength = JT808BinaryExtensions.ReadUInt32Little(bytes, ref offset)
            };
            jT808_0X0701.ElectronicContent = JT808FormatterResolverExtensions.JT808DynamicDeserialize(JT808FormatterExtensions.GetFormatter(JT808_0x0701.JT808_0x0701Body.BodyImpl), bytes.Slice(offset, (int)jT808_0X0701.ElectronicWaybillLength), out int readSubBodySize);
            readSize = readSubBodySize;
            return jT808_0X0701;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0701 value)
        {
            if (JT808_0x0701.JT808_0x0701Body.BodyImpl == null) throw new JT808Exception(JT808ErrorCode.NotImplType, $"Not Impl {nameof(JT808_0x0701.JT808_0x0701Body)} class");
            object obj = JT808FormatterExtensions.GetFormatter(JT808_0x0701.JT808_0x0701Body.BodyImpl);
            // 需要反着来，先序列化数据体（由于位置汇报数据体长度为4个字节，所以先偏移4个字节），再根据数据体的长度设置回去
            int tmpOffset = JT808FormatterResolverExtensions.JT808DynamicSerialize(obj, ref bytes, offset + 4, value.ElectronicContent);
            JT808BinaryExtensions.WriteUInt32Little(bytes, offset, (ushort)(tmpOffset - offset - 4));
            offset = tmpOffset;
            return offset;
        }
    }
}
