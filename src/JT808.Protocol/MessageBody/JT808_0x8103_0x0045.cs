using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 终端电话接听策略，0：自动接听；1：ACC ON 时自动接听，OFF 时手动接听
    /// </summary>
    public class JT808_0x8103_0x0045 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0045>
    {
        public override uint ParamId { get; set; } = 0x0045;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 终端电话接听策略，0：自动接听；1：ACC ON 时自动接听，OFF 时手动接听
        /// </summary>
        public uint ParamValue { get; set; }
        public JT808_0x8103_0x0045 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0045 jT808_0x8103_0x0045 = new JT808_0x8103_0x0045();
            jT808_0x8103_0x0045.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0045.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0045.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0045;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0045 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
