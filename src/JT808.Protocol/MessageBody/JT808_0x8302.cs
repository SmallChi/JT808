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
    /// 提问下发
    /// 0x8302
    /// 2019版本已作删除
    /// </summary>
    public class JT808_0x8302 : JT808MessagePackFormatter<JT808_0x8302>, JT808Bodies,  IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 0x8302
        /// </summary>
        public ushort MsgId =>0x8302;
        /// <summary>
        /// 提问下发
        /// </summary>
        public string Description => "提问下发";
        /// <summary>
        /// 标志
        /// 提问下发标志位定义
        /// </summary>
        public byte Flag { get; set; }
        /// <summary>
        /// 问题内容长度
        /// </summary>
        public byte IssueContentLength { get; set; }
        /// <summary>
        /// 问题
        /// 问题文本，经 GBK 编码，长度为 N
        /// </summary>
        public string Issue { get; set; }
        /// <summary>
        /// 候选答案列表 
        /// </summary>
        public List<Answer> Answers { get; set; }
        /// <summary>
        /// 候选答案信息
        /// </summary>
        public class Answer
        {
            /// <summary>
            /// 答案 ID
            /// </summary>
            public byte Id { get; set; }
            /// <summary>
            /// 答案内容长度
            /// 答案内容字段字节长度
            /// </summary>
            public ushort ContentLength { get; set; }
            /// <summary>
            /// 答案内容 
            /// 答案内容，经 GBK 编码
            /// </summary>
            public string Content { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8302 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8302 jT808_0X8302 = new JT808_0x8302();
            jT808_0X8302.Flag = reader.ReadByte();
            jT808_0X8302.IssueContentLength = reader.ReadByte();
            jT808_0X8302.Issue = reader.ReadString(jT808_0X8302.IssueContentLength);
            jT808_0X8302.Answers = new List<JT808_0x8302.Answer>();
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                try
                {
                    JT808_0x8302.Answer answer = new JT808_0x8302.Answer();
                    answer.Id = reader.ReadByte();
                    answer.ContentLength = reader.ReadUInt16();
                    answer.Content = reader.ReadString(answer.ContentLength);
                    jT808_0X8302.Answers.Add(answer);
                }
                catch
                {
                    break;
                }
            }
            return jT808_0X8302;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8302 value, IJT808Config config)
        {
            writer.WriteByte(value.Flag);
            // 先计算内容长度（汉字为两个字节）
            writer.Skip(1, out int issuePosition);
            writer.WriteString(value.Issue);
            ushort issueLength = (ushort)(writer.GetCurrentPosition() - issuePosition - 1);
            writer.WriteByteReturn((byte)issueLength, issuePosition);
            if (value.Answers != null && value.Answers.Count > 0)
            {
                foreach (var item in value.Answers)
                {
                    writer.WriteByte(item.Id);
                    writer.Skip(2, out int answerPosition);
                    writer.WriteString(item.Content);
                    ushort answerLength = (ushort)(writer.GetCurrentPosition() - answerPosition - 2);
                    writer.WriteUInt16Return(answerLength, answerPosition);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8302 value = new JT808_0x8302();
            value.Flag = reader.ReadByte();
            writer.WriteNumber($"[{value.Flag.ReadNumber()}]标志", value.Flag);
            value.IssueContentLength = reader.ReadByte();
            writer.WriteNumber($"[{value.IssueContentLength.ReadNumber()}]问题内容长度", value.IssueContentLength);
            var issueBuffer= reader.ReadVirtualArray(value.IssueContentLength).ToArray();
            value.Issue = reader.ReadString(value.IssueContentLength);
            writer.WriteString($"[{issueBuffer.ToHexString()}]问题文本", value.Issue);
            writer.WriteStartArray("候选答案列表");
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                writer.WriteStartObject();
                JT808_0x8302.Answer answer = new JT808_0x8302.Answer();
                answer.Id = reader.ReadByte();
                writer.WriteNumber($"[{answer.Id.ReadNumber()}]答案ID", answer.Id);
                answer.ContentLength = reader.ReadUInt16();
                writer.WriteNumber($"[{answer.ContentLength.ReadNumber()}]答案内容长度", answer.ContentLength);
                var answerBuffer = reader.ReadVirtualArray(answer.ContentLength).ToArray();
                answer.Content = reader.ReadString(answer.ContentLength);
                writer.WriteString($"[{answerBuffer.ToHexString()}]答案内容", answer.Content);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
