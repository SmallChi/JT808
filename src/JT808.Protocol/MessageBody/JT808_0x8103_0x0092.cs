using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据输出频率，定义如下：
    /// 0x00：500ms；0x01：1000ms（默认值）；
    /// 0x02：2000ms；0x03：3000ms；
    /// 0x04：4000ms。
    /// </summary>
    public class JT808_0x8103_0x0092 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0092>
    {
        public override uint ParamId { get; set; } = 0x0092;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 模块详细定位数据输出频率，定义如下：
        /// 0x00：500ms；0x01：1000ms（默认值）；
        /// 0x02：2000ms；0x03：3000ms；
        /// 0x04：4000ms。
        /// </summary>
        public byte ParamValue { get; set; }
        public JT808_0x8103_0x0092 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0092 jT808_0x8103_0x0092 = new JT808_0x8103_0x0092();
            jT808_0x8103_0x0092.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0092.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0092.ParamValue = reader.ReadByte();
            return jT808_0x8103_0x0092;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0092 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
