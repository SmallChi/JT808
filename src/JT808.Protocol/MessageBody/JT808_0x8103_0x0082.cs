using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 车辆所在的市域 ID
    /// </summary>
    public class JT808_0x8103_0x0082 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0082>
    {
        public override uint ParamId { get; set; } = 0x0082;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 车辆所在的市域 ID
        /// </summary>
        public ushort ParamValue { get; set; }
        public JT808_0x8103_0x0082 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0082 jT808_0x8103_0x0082 = new JT808_0x8103_0x0082();
            jT808_0x8103_0x0082.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0082.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0082.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x0082;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0082 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
