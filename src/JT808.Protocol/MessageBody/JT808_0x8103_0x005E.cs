using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 侧翻报警参数设置：
    /// 侧翻角度，单位 1 度，默认为 30 度
    /// </summary>
    public class JT808_0x8103_0x005E : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x005E>
    {
        public override uint ParamId { get; set; } = 0x005E;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 侧翻报警参数设置：
        /// 侧翻角度，单位 1 度，默认为 30 度
        /// </summary>
        public ushort ParamValue { get; set; }
        public JT808_0x8103_0x005E Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x005E jT808_0x8103_0x005E = new JT808_0x8103_0x005E();
            jT808_0x8103_0x005E.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005E.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005E.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x005E;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x005E value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
