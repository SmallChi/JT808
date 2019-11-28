using JT808.Protocol.Attributes;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 胎压
    ///  2019版本
    /// </summary>
    public class JT808_0x0200_0x06 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x06>, IJT808_2019_Version
    {
        /// <summary>
        /// 车厢温度
        /// </summary>
        public short CarTemperature { get; set; }
        public override byte AttachInfoId { get; set; } = 0x05;
        public override byte AttachInfoLength { get; set; } = 2;

        public JT808_0x0200_0x06 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x06 value = new JT808_0x0200_0x06();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.CarTemperature =reader.ReadInt16();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x06 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt16(value.CarTemperature);
        }
    }
}
