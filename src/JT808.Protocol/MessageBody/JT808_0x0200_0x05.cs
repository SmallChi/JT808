
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System.Text.Json;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 胎压
    ///  2019版本
    /// </summary>
    public class JT808_0x0200_0x05 : JT808MessagePackFormatter<JT808_0x0200_0x05>, JT808_0x0200_BodyBase, IJT808Analyze, IJT808_2019_Version
    {
        /// <summary>
        /// 胎压
        /// </summary>
        public byte[] TirePressure { get; set; }
        /// <summary>
        /// JT808_0x0200_0x05
        /// </summary>
        public byte AttachInfoId { get; set; } = JT808Constants.JT808_0x0200_0x05;
        /// <summary>
        /// 30 byte
        /// </summary>
        public byte AttachInfoLength { get; set; } = 30;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x0200_0x05 value = new JT808_0x0200_0x05();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.TirePressure = reader.ReadArray(value.AttachInfoLength).ToArray();
            writer.WriteString($"胎压", value.TirePressure.ToHexString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x0200_0x05 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x05 value = new JT808_0x0200_0x05();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.TirePressure = reader.ReadArray(value.AttachInfoLength).ToArray();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x05 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteArray(value.TirePressure);
        }
    }
}
