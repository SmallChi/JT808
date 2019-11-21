using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 定位模式，定义如下：
    /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
    /// bit1，0：禁用北斗定位， 1：启用北斗定位；
    /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
    /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
    /// </summary>
    public class JT808_0x8103_0x0090 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0090>
    {
        public override uint ParamId { get; set; } = 0x0090;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// GNSS 定位模式，定义如下：
        /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
        /// bit1，0：禁用北斗定位， 1：启用北斗定位；
        /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
        /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
        /// </summary>
        public byte ParamValue { get; set; }
        public JT808_0x8103_0x0090 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0090 jT808_0x8103_0x0090 = new JT808_0x8103_0x0090();
            jT808_0x8103_0x0090.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0090.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0090.ParamValue = reader.ReadByte();
            return jT808_0x8103_0x0090;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0090 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
