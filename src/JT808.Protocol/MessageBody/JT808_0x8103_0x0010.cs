using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 主服务器 APN，无线通信拨号访问点。若网络制式为 CDMA，则该处为PPP 拨号号码
    /// </summary>
    public class JT808_0x8103_0x0010 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0010>
    {
        public override uint ParamId { get; set; } = 0x0010;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 主服务器 APN，无线通信拨号访问点。若网络制式为 CDMA，则该处为PPP 拨号号码
        /// </summary>
        public string ParamValue { get; set; }
        public JT808_0x8103_0x0010 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0010 jT808_0x8103_0x0010 = new JT808_0x8103_0x0010();
            jT808_0x8103_0x0010.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0010.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0010.ParamValue = reader.ReadString(jT808_0x8103_0x0010.ParamLength);
            return jT808_0x8103_0x0010;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0010 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}
