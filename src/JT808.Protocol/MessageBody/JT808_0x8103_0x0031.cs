using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 电子围栏半径（非法位移阈值），单位为米
    /// </summary>
    public class JT808_0x8103_0x0031 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0031>
    {
        public override uint ParamId { get; set; } = 0x0031;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 电子围栏半径（非法位移阈值），单位为米
        /// </summary>
        public ushort ParamValue { get; set; }
        public JT808_0x8103_0x0031 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0031 jT808_0x8103_0x0031 = new JT808_0x8103_0x0031();
            jT808_0x8103_0x0031.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0031.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0031.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x0031;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0031 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
