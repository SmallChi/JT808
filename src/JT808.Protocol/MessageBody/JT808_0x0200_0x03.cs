using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Runtime.Serialization;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 行驶记录功能获取的速度
    /// </summary>
    public class JT808_0x0200_0x03 : JT808MessagePackFormatter<JT808_0x0200_0x03>, JT808_0x0200_BodyBase,  IJT808Analyze
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
        /// <summary>
        /// JT808_0x0200_0x03
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x03;
        /// <summary>
        /// 2 byte
        /// </summary>
        public byte AttachInfoLength { get; set; } = 2;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x03 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x03 value = new JT808_0x0200_0x03();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Speed = reader.ReadUInt16();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x03 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.Speed);
        }
    }
}
