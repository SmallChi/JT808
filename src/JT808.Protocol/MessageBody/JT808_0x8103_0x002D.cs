using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 驾驶员未登录汇报距离间隔，单位为米（m），>0
    /// </summary>
    public class JT808_0x8103_0x002D : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x002D>
    {
        public override uint ParamId { get; set; } = 0x002D;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 驾驶员未登录汇报距离间隔，单位为米（m），>0
        /// </summary>
        public uint ParamValue { get; set; }
        public JT808_0x8103_0x002D Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x002D jT808_0x8103_0x002D = new JT808_0x8103_0x002D();
            jT808_0x8103_0x002D.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x002D.ParamLength = reader.ReadByte();
            jT808_0x8103_0x002D.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x002D;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x002D value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
