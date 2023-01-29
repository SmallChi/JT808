using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Runtime.Serialization;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 里程
    /// </summary>
    public class JT808_0x0200_0x01 : JT808MessagePackFormatter<JT808_0x0200_0x01>, JT808_0x0200_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// JT808_0x0200_0x01
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x01;
        /// <summary>
        ///  4 byte
        /// </summary>
        public byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 里程
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// 里程 1/10km，对应车上里程表读数
        /// </summary>
        [IgnoreDataMember]
        public double ConvertMileage => Mileage / 10.0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x01 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x01 value = new JT808_0x0200_0x01();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Mileage = reader.ReadInt32();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x01 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteInt32(value.Mileage);
        }
    }
}
