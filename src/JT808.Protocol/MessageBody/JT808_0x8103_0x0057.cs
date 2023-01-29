using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 连续驾驶时间门限，单位为秒（s）
    /// </summary>
    public class JT808_0x8103_0x0057 : JT808MessagePackFormatter<JT808_0x8103_0x0057>, JT808_0x8103_BodyBase,  IJT808Analyze
    {
        /// <summary>
        /// 0x0057
        /// </summary>
        public  uint ParamId { get; set; } = 0x0057;
        /// <summary>
        /// 数据长度
        /// </summary>
        public  byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 连续驾驶时间门限，单位为秒（s）
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 连续驾驶时间门限
        /// </summary>
        public  string Description => "连续驾驶时间门限";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x0057 jT808_0x8103_0x0057 = new JT808_0x8103_0x0057();
            jT808_0x8103_0x0057.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0057.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0057.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ jT808_0x8103_0x0057.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x0057.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x0057.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x0057.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x0057.ParamValue.ReadNumber()}]参数值[连续驾驶时间门限s]", jT808_0x8103_0x0057.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x0057 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0057 jT808_0x8103_0x0057 = new JT808_0x8103_0x0057();
            jT808_0x8103_0x0057.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0057.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0057.ParamValue = reader.ReadUInt32();
            return jT808_0x8103_0x0057;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0057 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
