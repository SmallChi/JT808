using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 报警屏蔽字，与位置信息汇报消息中的报警标志相对应，相应位为 1则相应报警被屏蔽
    /// </summary>
    public class JT808_0x8103_0x0050 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0050>
    {
        public override uint ParamId { get; set; } = 0x0050;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 报警屏蔽字，与位置信息汇报消息中的报警标志相对应，相应位为 1则相应报警被屏蔽
        /// </summary>
        public uint ParamValue { get; set; }
        public JT808_0x8103_0x0050 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0050 jT808_0x8103_0x0050 = new JT808_0x8103_0x0050();
            jT808_0x8103_0x0050.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0050.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0050.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0050;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0050 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
