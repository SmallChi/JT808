using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Interfaces;
using System;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Formatters.MessageBodyFormatters
{
    public class JT808_0x8302_Formatter : IJT808MessagePackFormatter<JT808_0x8302>
    {
        public JT808_0x8302 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8302 jT808_0X8302 = new JT808_0x8302();
            jT808_0X8302.Flag = reader.ReadByte();
            jT808_0X8302.IssueContentLength = reader.ReadByte();
            jT808_0X8302.Issue = reader.ReadString(jT808_0X8302.IssueContentLength);
            jT808_0X8302.AnswerId = reader.ReadByte();
            jT808_0X8302.AnswerContentLength = reader.ReadUInt16();
            jT808_0X8302.AnswerContent = reader.ReadString(jT808_0X8302.AnswerContentLength);
            return jT808_0X8302;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8302 value, IJT808Config config)
        {
            writer.WriteByte(value.Flag);
            // 先计算内容长度（汉字为两个字节）
            writer.Skip(1, out int issuePosition);
            writer.WriteString(value.Issue);
            ushort issueLength = (ushort)(writer.GetCurrentPosition() - issuePosition - 1);
            writer.WriteByteReturn((byte)issueLength, issuePosition);
            writer.WriteByte(value.AnswerId);
            writer.Skip(2, out int answerPosition);
            writer.WriteString(value.AnswerContent);
            ushort answerLength = (ushort)(writer.GetCurrentPosition() - answerPosition - 2);
            writer.WriteUInt16Return(answerLength, answerPosition);
        }
    }
}
