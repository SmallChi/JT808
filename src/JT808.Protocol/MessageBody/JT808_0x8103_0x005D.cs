using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 碰撞报警参数设置
    /// b7-b0： 碰撞时间，单位 4ms；
    /// b15-b8：碰撞加速度，单位 0.1g，设置范围在：0-79 之间，默认为10。
    /// </summary>
    public class JT808_0x8103_0x005D : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x005D>
    {
        public override uint ParamId { get; set; } = 0x005D;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 碰撞报警参数设置
        /// b7-b0： 碰撞时间，单位 4ms；
        /// b15-b8：碰撞加速度，单位 0.1g，设置范围在：0-79 之间，默认为10。
        /// </summary>
        public ushort ParamValue { get; set; }
        public JT808_0x8103_0x005D Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x005D jT808_0x8103_0x005D = new JT808_0x8103_0x005D();
            jT808_0x8103_0x005D.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005D.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005D.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x005D;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x005D value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
