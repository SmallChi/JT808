using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 道路运输证 IC 卡认证主服务器 UDP 端口
    /// </summary>
    public class JT808_0x8103_0x001C : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x001C>
    {
        public override uint ParamId { get; set; } = 0x001C;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        ///道路运输证 IC 卡认证主服务器 UDP 端口
        /// </summary>
        public uint ParamValue { get; set; }
        public JT808_0x8103_0x001C Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x001C jT808_0x8103_0x001C = new JT808_0x8103_0x001C();
            jT808_0x8103_0x001C.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x001C.ParamLength = reader.ReadByte();
            jT808_0x8103_0x001C.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x001C;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x001C value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
