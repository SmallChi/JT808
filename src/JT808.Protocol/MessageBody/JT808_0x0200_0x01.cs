using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System.Runtime.Serialization;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x01 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x01>
    {
        public override byte AttachInfoId { get; set; } = 0x01;
        public override byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 里程
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// 里程 1/10km，对应车上里程表读数
        /// </summary>
        [IgnoreDataMember]
        public double ConvertMileage => Mileage / 10.0;
        public JT808_0x0200_0x01 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x01 jT808LocationAttachImpl0X01 = new JT808_0x0200_0x01();
            jT808LocationAttachImpl0X01.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0X01.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0X01.Mileage = reader.ReadInt32();
            return jT808LocationAttachImpl0X01;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x01 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.Mileage);
        }
    }
}
