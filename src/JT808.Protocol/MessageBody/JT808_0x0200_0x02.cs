using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Runtime.Serialization;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 油量
    /// </summary>
    public class JT808_0x0200_0x02 : JT808MessagePackFormatter<JT808_0x0200_0x02>, JT808_0x0200_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 油量
        /// </summary>
        public ushort Oil { get; set; }
        /// <summary>
        /// 油量 1/10L，对应车上油量表读数
        /// </summary>
        [IgnoreDataMember]
        public double ConvertOil => Oil / 10.0;
        /// <summary>
        /// JT808_0x0200_0x02
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x02;
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
            JT808_0x0200_0x02 value = new JT808_0x0200_0x02();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Oil = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Oil.ReadNumber()}]油量", value.Oil);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x02 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x02 value = new JT808_0x0200_0x02();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Oil = reader.ReadUInt16();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x02 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.Oil);
        }
    }
}
