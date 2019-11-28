using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 违规行驶时段范围
    /// </summary>
    public class JT808_0x8103_0x0032 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0032>, IJT808_2019_Version
    {
        public override uint ParamId { get; set; } = 0x0032;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 电子围栏半径（非法位移阈值），单位为米
        /// </summary>
        public byte[] ParamValue { get; set; }
        public JT808_0x8103_0x0032 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0032 value = new JT808_0x8103_0x0032();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.ParamValue = reader.ReadArray(4).ToArray();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0032 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteArray(value.ParamValue);
        }
    }
}
