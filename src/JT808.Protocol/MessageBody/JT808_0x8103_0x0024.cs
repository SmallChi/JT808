using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 从服务器无线通信拨号用户名。该值为空时，终端应使用主服务器相同配置
    /// 2019版本
    /// </summary>
    public class JT808_0x8103_0x0024 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0024>, IJT808_2019_Version
    {
        public override uint ParamId { get; set; } = 0x0024;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParamValue { get; set; }
        public JT808_0x8103_0x0024 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0024 jT808_0x8103_0x0024 = new JT808_0x8103_0x0024();
            jT808_0x8103_0x0024.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0024.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0024.ParamValue = reader.ReadString(jT808_0x8103_0x0024.ParamLength);
            return jT808_0x8103_0x0024;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0024 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}
