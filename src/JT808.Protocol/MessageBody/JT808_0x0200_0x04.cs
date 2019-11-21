using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x04 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x04>
    {
        /// <summary>
        /// 需要人工确认报警事件的 ID，从 1 开始计数
        /// </summary>
        public ushort EventId { get; set; }
        public override byte AttachInfoId { get; set; } = 0x04;
        public override byte AttachInfoLength { get; set; } = 2;

        public JT808_0x0200_0x04 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x04 jT808LocationAttachImpl0x04 = new JT808_0x0200_0x04();
            jT808LocationAttachImpl0x04.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x04.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x04.EventId = reader.ReadUInt16();
            return jT808LocationAttachImpl0x04;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x04 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.EventId);
        }
    }
}
