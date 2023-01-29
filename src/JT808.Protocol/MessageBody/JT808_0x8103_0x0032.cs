using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 违规行驶时段范围
    /// </summary>
    public class JT808_0x8103_0x0032 : JT808MessagePackFormatter<JT808_0x8103_0x0032>, JT808_0x8103_BodyBase,  IJT808_2019_Version, IJT808Analyze
    {
        /// <summary>
        /// 0x0032
        /// </summary>
        public  uint ParamId { get; set; } = 0x0032;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public  byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 违规行驶时段范围（非法位移阈值），单位为米
        /// </summary>
        public byte[] ParamValue { get; set; }
        /// <summary>
        /// 违规行驶时段范围
        /// </summary>
        public  string Description => "违规行驶时段范围";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0032 value = new JT808_0x8103_0x0032();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.ParamValue = reader.ReadArray(4).ToArray();
            writer.WriteNumber($"[{ value.ParamId.ReadNumber()}]参数ID", value.ParamId);
            writer.WriteNumber($"[{value.ParamLength.ReadNumber()}]参数长度", value.ParamLength);
            writer.WriteString($"[{ value.ParamValue.ToHexString()}]参数值[违规行驶时段范围]", $"开始时间{value.ParamValue[0]}时{value.ParamValue[1]}分，结束时间{value.ParamValue[2]}时{value.ParamValue[3]}分");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0032 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0032 value = new JT808_0x8103_0x0032();
            value.ParamId = reader.ReadUInt32();
            value.ParamLength = reader.ReadByte();
            value.ParamValue = reader.ReadArray(4).ToArray();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0032 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteArray(value.ParamValue);
        }
    }
}
