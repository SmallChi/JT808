using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Test.JT808_0x0900_BodiesImpl
{
    public class JT808_0x0900_0x83 : JT808_0x0900_BodyBase,IJT808MessagePackFormatter<JT808_0x0900_0x83>
    {
        /// <summary>
        /// 透传内容
        /// </summary>
        public string PassthroughContent { get; set; }

        public JT808_0x0900_0x83 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0900_0x83 jT808PassthroughType0x83 = new JT808_0x0900_0x83();
            jT808PassthroughType0x83.PassthroughContent = reader.ReadRemainStringContent();
            return jT808PassthroughType0x83;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0900_0x83 value, IJT808Config config)
        {
            writer.WriteString(value.PassthroughContent);
        }
    }
}
