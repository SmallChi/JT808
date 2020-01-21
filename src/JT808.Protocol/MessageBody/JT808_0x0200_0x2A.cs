using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x2A : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x2A>, IJT808Analyze
    {
        /// <summary>
        /// IO状态位
        /// </summary>
        public ushort IOStatus { get; set; }
        public override byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x2A;
        public override byte AttachInfoLength { get; set; } = 2;

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x2A value = new JT808_0x0200_0x2A();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.IOStatus = reader.ReadUInt16();
            writer.WriteNumber($"[{value.IOStatus.ReadNumber()}]IO状态位", value.IOStatus);
        }

        public JT808_0x0200_0x2A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x2A value = new JT808_0x0200_0x2A();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.IOStatus = reader.ReadUInt16();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x2A value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.IOStatus);
        }
    }
}
