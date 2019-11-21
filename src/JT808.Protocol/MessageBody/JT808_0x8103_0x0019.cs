using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 服务器 UDP 端口
    /// </summary>
    public class JT808_0x8103_0x0019 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0019>
    {
        public override uint ParamId { get; set; } = 0x0019;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        ///服务器 TCP 端口
        /// </summary>
        public uint ParamValue { get; set; }
        public JT808_0x8103_0x0019 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0019 jT808_0x8103_0x0019 = new JT808_0x8103_0x0019();
            jT808_0x8103_0x0019.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0019.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0019.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0019;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0019 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
