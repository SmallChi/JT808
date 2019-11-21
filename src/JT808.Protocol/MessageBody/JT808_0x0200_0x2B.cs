using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x2B : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x2B>
    {
        /// <summary>
        /// 模拟量 bit0-15，AD0；bit16-31，AD1
        /// </summary>
        public int Analog { get; set; }
        public override byte AttachInfoId { get; set; } = 0x2B;
        public override byte AttachInfoLength { get; set; } = 4;
        public JT808_0x0200_0x2B Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x2B jT808LocationAttachImpl0x2B = new JT808_0x0200_0x2B();
            jT808LocationAttachImpl0x2B.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x2B.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x2B.Analog = reader.ReadInt32();
            return jT808LocationAttachImpl0x2B;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x2B value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.Analog);
        }
    }
}
