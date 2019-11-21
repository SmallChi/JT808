using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x31 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x31>
    {
        /// <summary>
        /// GNSS 定位卫星数
        /// </summary>
        public byte GNSSCount { get; set; }
        public override byte AttachInfoId { get; set; } = 0x31;
        public override byte AttachInfoLength { get; set; } = 1;
        public JT808_0x0200_0x31 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x31 jT808LocationAttachImpl0x31 = new JT808_0x0200_0x31();
            jT808LocationAttachImpl0x31.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x31.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x31.GNSSCount = reader.ReadByte();
            return jT808LocationAttachImpl0x31;
        }
        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x31 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte(value.GNSSCount);
        }
    }
}
