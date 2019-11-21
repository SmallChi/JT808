using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x12 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x12>
    {
        /// <summary>
        /// 位置类型
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 方向 
        /// 0：进
        /// 1：出
        /// </summary>
        public JT808DirectionType Direction { get; set; }
        public override byte AttachInfoId { get; set; } = 0x12;
        public override byte AttachInfoLength { get; set; } = 6;

        public JT808_0x0200_0x12 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x12 jT808LocationAttachImpl0x12 = new JT808_0x0200_0x12();
            jT808LocationAttachImpl0x12.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x12.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x12.JT808PositionType = (JT808PositionType)reader.ReadByte();
            jT808LocationAttachImpl0x12.AreaId = reader.ReadInt32();
            jT808LocationAttachImpl0x12.Direction = (JT808DirectionType)reader.ReadByte();
            return jT808LocationAttachImpl0x12;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x12 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte((byte)value.JT808PositionType);
            writer.WriteInt32(value.AreaId);
            writer.WriteByte((byte)value.Direction);
        }
    }
}
