using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.Test.JT808_0x0900_BodiesImpl
{
    public class JT808_0x0900_0x83 : JT808MessagePackFormatter<JT808_0x0900_0x83>, JT808_0x0900_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 透传内容
        /// </summary>
        public string PassthroughContent { get; set; }
        public byte PassthroughType { get; set; } = 0x83;

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0900_0x83 value = new JT808_0x0900_0x83();
            value.PassthroughContent = reader.ReadRemainStringContent();
            writer.WriteString("透传内容", value.PassthroughContent);
        }

        public override JT808_0x0900_0x83 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0900_0x83 value = new JT808_0x0900_0x83();
            value.PassthroughContent = reader.ReadRemainStringContent();
            return value;
        }

        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0900_0x83 value, IJT808Config config)
        {
            writer.WriteString(value.PassthroughContent);
        }
    }
}
