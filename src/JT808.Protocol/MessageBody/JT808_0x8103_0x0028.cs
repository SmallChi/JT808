using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 紧急报警时汇报时间间隔，单位为秒（s），>0
    /// </summary>
    public class JT808_0x8103_0x0028 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0028>
    {
        public override uint ParamId { get; set; } = 0x0028;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 紧急报警时汇报时间间隔，单位为秒（s），>0
        /// </summary>
        public uint ParamValue { get; set; }
        public JT808_0x8103_0x0028 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0028 jT808_0x8103_0x0028 = new JT808_0x8103_0x0028();
            jT808_0x8103_0x0028.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0028.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0028.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0028;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0028 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
