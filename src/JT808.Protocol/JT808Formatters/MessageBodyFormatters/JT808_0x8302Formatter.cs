using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8302Formatter : IJT808Formatter<JT808_0x8302>
    {
        public JT808_0x8302 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808_0x8302 jT808_0X8302 = new JT808_0x8302
            {
                Flag = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset),
                IssueContentLength = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset)
            };
            jT808_0X8302.Issue = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X8302.IssueContentLength);
            jT808_0X8302.AnswerId = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8302.AnswerContentLength = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8302.AnswerContent = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X8302.AnswerContentLength);
            readSize = offset;
            return jT808_0X8302;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8302 value)
        {
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.Flag);
            // 先计算内容长度（汉字为两个字节）
            offset += 1;
            int byteLength1 = JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.Issue);
            JT808BinaryExtensions.WriteByteLittle(bytes, offset - 1, (byte)byteLength1);
            offset += byteLength1;
            offset += JT808BinaryExtensions.WriteByteLittle(bytes, offset, value.AnswerId);
            offset += 2;
            int byteLength2 = JT808BinaryExtensions.WriteStringLittle(bytes, offset, value.AnswerContent);
            JT808BinaryExtensions.WriteUInt16Little(bytes, offset - 2, (ushort)byteLength2);
            offset += byteLength2;
            return offset;
        }
    }
}
