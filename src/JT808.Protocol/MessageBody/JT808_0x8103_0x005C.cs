using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 疲劳驾驶预警差值，单位为秒（s），>0
    /// </summary>
    public class JT808_0x8103_0x005C : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x005C>
    {
        public override uint ParamId { get; set; } = 0x005C;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 疲劳驾驶预警差值，单位为秒（s），>0
        /// </summary>
        public ushort ParamValue { get; set; }
        public JT808_0x8103_0x005C Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x005C jT808_0x8103_0x005C = new JT808_0x8103_0x005C();
            jT808_0x8103_0x005C.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005C.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005C.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x005C;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x005C value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
