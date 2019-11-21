using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x25 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x25>
    {
        /// <summary>
        /// 扩展车辆信号状态位
        /// </summary>
        public int CarSignalStatus { get; set; }
        public override byte AttachInfoId { get; set; } = 0x25;
        public override byte AttachInfoLength { get; set; } = 4;
        public JT808_0x0200_0x25 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x25 jT808LocationAttachImpl0x13 = new JT808_0x0200_0x25();
            jT808LocationAttachImpl0x13.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x13.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x13.CarSignalStatus = reader.ReadInt32();
            return jT808LocationAttachImpl0x13;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x25 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.CarSignalStatus);
        }
    }
}
