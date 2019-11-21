using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端鉴权
    /// </summary>
    public class JT808_0x0102 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x0102>
    {
        /// <summary>
        /// 鉴权码
        /// </summary>
        public string Code { get; set; }

        public JT808_0x0102 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0102 jT808_0X0102 = new JT808_0x0102();
            jT808_0X0102.Code = reader.ReadRemainStringContent();
            return jT808_0X0102;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0102 value, IJT808Config config)
        {
            writer.WriteString(value.Code);
        }
    }
}
