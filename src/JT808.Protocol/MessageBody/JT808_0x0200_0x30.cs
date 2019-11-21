using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x30 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x30>
    {
        /// <summary>
        /// 无线通信网络信号强度
        /// </summary>
        public byte WiFiSignalStrength { get; set; }
        public override byte AttachInfoId { get; set; } = 0x30;
        public override byte AttachInfoLength { get; set; } = 1;
        public JT808_0x0200_0x30 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x30 jT808LocationAttachImpl0x30 = new JT808_0x0200_0x30();
            jT808LocationAttachImpl0x30.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x30.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x30.WiFiSignalStrength = reader.ReadByte();
            return jT808LocationAttachImpl0x30;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x30 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte(value.WiFiSignalStrength);
        }
    }
}
