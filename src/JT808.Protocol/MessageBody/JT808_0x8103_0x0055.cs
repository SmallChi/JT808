using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 最高速度，单位为公里每小时（km/h）
    /// </summary>
    public class JT808_0x8103_0x0055 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0055>
    {
        public override uint ParamId { get; set; } = 0x0055;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 最高速度，单位为公里每小时（km/h）
        /// </summary>
        public uint ParamValue { get; set; }
        public JT808_0x8103_0x0055 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0055 jT808_0x8103_0x0055 = new JT808_0x8103_0x0055();
            jT808_0x8103_0x0055.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0055.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0055.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0055;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0055 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
