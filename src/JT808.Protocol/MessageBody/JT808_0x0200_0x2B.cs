using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x2B : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x2B>, IJT808Analyze
    {
        /// <summary>
        /// 模拟量 bit0-15，AD0；bit16-31，AD1
        /// </summary>
        public int Analog { get; set; }
        public override byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x2B;
        public override byte AttachInfoLength { get; set; } = 4;

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x2B value = new JT808_0x0200_0x2B();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Analog = reader.ReadInt32();
            writer.WriteNumber($"[{value.Analog.ReadNumber()}]模拟量", value.Analog);
        }

        public JT808_0x0200_0x2B Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x2B value = new JT808_0x0200_0x2B();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Analog = reader.ReadInt32();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x2B value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.Analog);
        }
    }
}
