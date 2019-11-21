using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 接收终端 SMS 文本报警号码
    /// </summary>
    public class JT808_0x8103_0x0044 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0044>
    {
        public override uint ParamId { get; set; } = 0x0044;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 接收终端 SMS 文本报警号码
        /// </summary>
        public string ParamValue { get; set; }
        public JT808_0x8103_0x0044 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0044 jT808_0x8103_0x0044 = new JT808_0x8103_0x0044();
            jT808_0x8103_0x0044.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0044.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0044.ParamValue = reader.ReadString(jT808_0x8103_0x0044.ParamLength);
            return jT808_0x8103_0x0044;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0044 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}
