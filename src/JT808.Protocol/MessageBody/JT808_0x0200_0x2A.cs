using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x2A : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x2A>
    {
        /// <summary>
        /// IO状态位
        /// </summary>
        public ushort IOStatus { get; set; }
        public override byte AttachInfoId { get; set; } = 0x2A;
        public override byte AttachInfoLength { get; set; } = 2;

        public JT808_0x0200_0x2A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x2A jT808LocationAttachImpl0X2A = new JT808_0x0200_0x2A();
            jT808LocationAttachImpl0X2A.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0X2A.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0X2A.IOStatus = reader.ReadUInt16();
            return jT808LocationAttachImpl0X2A;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x2A value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.IOStatus);
        }
    }
}
