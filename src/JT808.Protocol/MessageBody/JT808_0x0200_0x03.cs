using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System.Runtime.Serialization;

namespace JT808.Protocol.MessageBody
{

    public class JT808_0x0200_0x03 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x03>
    {
        /// <summary>
        /// 行驶记录功能获取的速度
        /// </summary>
        public ushort Speed { get; set; }
        /// <summary>
        /// 行驶记录功能获取的速度 1/10km/h
        /// </summary>
        [IgnoreDataMember]
        public double ConvertSpeed => Speed / 10.0;
        public override byte AttachInfoId { get; set; } = 0x03;
        public override byte AttachInfoLength { get; set; } = 2;
        public JT808_0x0200_0x03 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x03 jT808LocationAttachImpl0x03 = new JT808_0x0200_0x03();
            jT808LocationAttachImpl0x03.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x03.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x03.Speed = reader.ReadUInt16();
            return jT808LocationAttachImpl0x03;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x03 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.Speed);
        }
    }
}
