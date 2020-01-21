using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Runtime.Serialization;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{

    public class JT808_0x0200_0x03 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x03>, IJT808Analyze
    {
        /// <summary>
        /// 行驶记录功能获取的速度
        /// </summary>
        public ushort Speed { get; set; }
        /// <summary>
        /// 行驶记录功能获取的速度 1/10km/h
        /// </summary>
        [IgnoreDataMember]
        public double ConvertSpeed => Speed / 10.0;
        public override byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x03;
        public override byte AttachInfoLength { get; set; } = 2;

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x03 value = new JT808_0x0200_0x03();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Speed = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Speed.ReadNumber()}]速度", value.Speed);
        }

        public JT808_0x0200_0x03 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x03 value = new JT808_0x0200_0x03();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Speed = reader.ReadUInt16();
            return value;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x03 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.Speed);
        }
    }
}
