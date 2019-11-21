using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 恢复出厂设置电话号码，可采用此电话号码拨打终端电话让终端恢复出厂设置
    /// </summary>
    public class JT808_0x8103_0x0042 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0042>
    {
        public override uint ParamId { get; set; } = 0x0042;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 恢复出厂设置电话号码，可采用此电话号码拨打终端电话让终端恢复出厂设置
        /// </summary>
        public string ParamValue { get; set; }
        public JT808_0x8103_0x0042 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0042 jT808_0x8103_0x0042 = new JT808_0x8103_0x0042();
            jT808_0x8103_0x0042.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0042.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0042.ParamValue = reader.ReadString(jT808_0x8103_0x0042.ParamLength);
            return jT808_0x8103_0x0042;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0042 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}
