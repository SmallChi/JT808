using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x11 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x11>
    {
        /// <summary>
        /// 超速报警附加信息
        /// 0：无特定位置；
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// 若位置类型为 0，无该字段
        /// </summary>
        public uint AreaId { get; set; }
        public override byte AttachInfoId { get; set; } = 0x11;
        public override byte AttachInfoLength
        {
            get
            {
                if (JT808PositionType != JT808PositionType.无特定位置)
                {
                    return 5;
                }
                else
                {
                    return 1;
                }
            }
            set { }
        }

        public JT808_0x0200_0x11 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x11 jT808LocationAttachImpl0x11 = new JT808_0x0200_0x11();
            jT808LocationAttachImpl0x11.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x11.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x11.JT808PositionType = (JT808PositionType)reader.ReadByte();
            if (jT808LocationAttachImpl0x11.JT808PositionType != JT808PositionType.无特定位置)
            {
                jT808LocationAttachImpl0x11.AreaId = reader.ReadUInt32();
            }
            return jT808LocationAttachImpl0x11;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x11 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte((byte)value.JT808PositionType);
            if (value.JT808PositionType != JT808PositionType.无特定位置)
            {
                writer.WriteUInt32(value.AreaId);
            }
        }
    }
}
