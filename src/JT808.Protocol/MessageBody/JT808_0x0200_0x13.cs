using JT808.Protocol.Enums;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x13 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x13>
    {
        public override byte AttachInfoId { get; set; } = 0x13;
        public override byte AttachInfoLength { get; set; } = 7;

        /// <summary>
        /// 路段 ID
        /// </summary>
        public int DrivenRouteId { get; set; }

        /// <summary>
        /// 路段行驶时间
        /// 单位为秒（s)
        /// </summary>
        public ushort Time { get; set; }

        /// <summary>
        ///  结果 0：不足；1：过长
        /// </summary>
        public JT808DrivenRouteType DrivenRoute { get; set; }

        public JT808_0x0200_0x13 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x13 jT808LocationAttachImpl0x13 = new JT808_0x0200_0x13();
            jT808LocationAttachImpl0x13.AttachInfoId = reader.ReadByte();
            jT808LocationAttachImpl0x13.AttachInfoLength = reader.ReadByte();
            jT808LocationAttachImpl0x13.DrivenRouteId = reader.ReadInt32();
            jT808LocationAttachImpl0x13.Time = reader.ReadUInt16();
            jT808LocationAttachImpl0x13.DrivenRoute = (JT808DrivenRouteType)reader.ReadByte();
            return jT808LocationAttachImpl0x13;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x13 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.DrivenRouteId);
            writer.WriteUInt16(value.Time);
            writer.WriteByte((byte)value.DrivenRoute);
        }
    }
}
