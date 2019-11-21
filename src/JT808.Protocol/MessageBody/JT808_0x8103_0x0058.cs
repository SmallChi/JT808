using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 当天累计驾驶时间门限，单位为秒（s）
    /// </summary>
    public class JT808_0x8103_0x0058 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0058>
    {
        public override uint ParamId { get; set; } = 0x0058;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 当天累计驾驶时间门限，单位为秒（s）
        /// </summary>
        public uint ParamValue { get; set; }
        public JT808_0x8103_0x0058 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0058 jT808_0x8103_0x0058 = new JT808_0x8103_0x0058();
            jT808_0x8103_0x0058.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0058.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0058.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0058;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0058 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
