using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Runtime.Serialization;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    public class JT808_0x0200_0x01 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x01>, IJT808Analyze
    {
        public override byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x01;
        public override byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 里程
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// 里程 1/10km，对应车上里程表读数
        /// </summary>
        [IgnoreDataMember]
        public double ConvertMileage => Mileage / 10.0;

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x01 value = new JT808_0x0200_0x01();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Mileage = reader.ReadInt32();
            writer.WriteNumber($"[{value.Mileage.ReadNumber()}]里程", value.Mileage);
        }

        public JT808_0x0200_0x01 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x01 value = new JT808_0x0200_0x01();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Mileage = reader.ReadInt32();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x01 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.Mileage);
        }
    }
}
