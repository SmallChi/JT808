using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x04 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x04>, IJT808Analyze
    {
        /// <summary>
        /// 需要人工确认报警事件的 ID，从 1 开始计数
        /// </summary>
        public ushort EventId { get; set; }
        public override byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x04;
        public override byte AttachInfoLength { get; set; } = 2;

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x04 value = new JT808_0x0200_0x04();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.EventId = reader.ReadUInt16();
            writer.WriteNumber($"[{value.EventId.ReadNumber()}]报警事件ID", value.EventId);
        }

        public JT808_0x0200_0x04 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x04 value = new JT808_0x0200_0x04();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.EventId = reader.ReadUInt16();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x04 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.EventId);
        }
    }
}
