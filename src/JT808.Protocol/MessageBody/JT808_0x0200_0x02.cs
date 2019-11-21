using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System.Runtime.Serialization;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x02 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x02>
    {
        /// <summary>
        /// 油量
        /// </summary>
        public ushort Oil { get; set; }
        /// <summary>
        /// 油量 1/10L，对应车上油量表读数
        /// </summary>
        [IgnoreDataMember]
        public double ConvertOil => Oil / 10.0;
        public override byte AttachInfoId { get; set; } = 0x02;
        public override byte AttachInfoLength { get; set; } = 2;

        public JT808_0x0200_0x02 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x02 jT808LocationAttachImpl0X02 = new JT808_0x0200_0x02();
            jT808LocationAttachImpl0X02.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0X02.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0X02.Oil = reader.ReadUInt16();
            return jT808LocationAttachImpl0X02;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x02 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.Oil);
        }
    }
}
