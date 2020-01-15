using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 提问应答
    /// 0x0302
    /// </summary>
    [Obsolete("2019版本已作删除")]
    public class JT808_0x0302 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0302>,IJT808_2019_Version, IJT808Analyze
    {
        public override ushort MsgId { get; } = 0x0302;
        public override string Description => "提问应答";
        /// <summary>
        /// 应答流水号
        /// 对应的提问下发消息的流水号
        /// </summary>
        public ushort ReplySNo { get; set; }
        /// <summary>
        /// 答案 ID 
        /// 提问下发中附带的答案 ID
        /// </summary>
        public byte AnswerId { get; set; }

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0302 jT808_0X0302 = new JT808_0x0302();
            jT808_0X0302.ReplySNo = reader.ReadUInt16();
            jT808_0X0302.AnswerId = reader.ReadByte();
            writer.WriteNumber($"[{jT808_0X0302.ReplySNo.ReadNumber()}]应答流水号", jT808_0X0302.ReplySNo);
            writer.WriteNumber($"[{jT808_0X0302.AnswerId.ReadNumber()}]答案ID", jT808_0X0302.AnswerId);
        }

        public JT808_0x0302 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0302 jT808_0X0302 = new JT808_0x0302();
            jT808_0X0302.ReplySNo = reader.ReadUInt16();
            jT808_0X0302.AnswerId = reader.ReadByte();
            return jT808_0X0302;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0302 value, IJT808Config config)
        {
            writer.WriteUInt16(value.ReplySNo);
            writer.WriteByte(value.AnswerId);
        }
    }
}
