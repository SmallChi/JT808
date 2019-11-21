using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 道路运输证 IC 卡认证主服务器 IP 地址或域名
    /// </summary>
    public class JT808_0x8103_0x001A : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x001A>
    {
        public override uint ParamId { get; set; } = 0x001A;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 道路运输证 IC 卡认证主服务器 IP 地址或域名
        /// </summary>
        public string ParamValue { get; set; }
        public JT808_0x8103_0x001A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x001A jT808_0x8103_0x001A = new JT808_0x8103_0x001A();
            jT808_0x8103_0x001A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x001A.ParamLength = reader.ReadByte();
            jT808_0x8103_0x001A.ParamValue = reader.ReadString(jT808_0x8103_0x001A.ParamLength);
            return jT808_0x8103_0x001A;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x001A value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}
