using System.Text.Json;

using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 超速报警预警差值，单位为 1/10Km/h
    /// </summary>
    public class JT808_0x8103_0x005B : JT808MessagePackFormatter<JT808_0x8103_0x005B>, JT808_0x8103_BodyBase, IJT808Analyze
    {
        /// <summary>
        /// 0x005B
        /// </summary>
        public  uint ParamId { get; set; } = 0x005B;
        /// <summary>
        /// 数据长度
        /// 2 byte
        /// </summary>
        public  byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 超速报警预警差值，单位为 1/10Km/h
        /// </summary>
        public ushort ParamValue { get; set; }
        /// <summary>
        /// 超速报警预警差值
        /// </summary>
        public  string Description => "超速报警预警差值";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x8103_0x005B jT808_0x8103_0x005B = new JT808_0x8103_0x005B();
            jT808_0x8103_0x005B.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005B.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005B.ParamValue = reader.ReadUInt16();
            writer.WriteNumber($"[{ jT808_0x8103_0x005B.ParamId.ReadNumber()}]参数ID", jT808_0x8103_0x005B.ParamId);
            writer.WriteNumber($"[{jT808_0x8103_0x005B.ParamLength.ReadNumber()}]参数长度", jT808_0x8103_0x005B.ParamLength);
            writer.WriteNumber($"[{ jT808_0x8103_0x005B.ParamValue.ReadNumber()}]参数值[超速报警预警差值1/10Km/h]", jT808_0x8103_0x005B.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public override JT808_0x8103_0x005B Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x005B jT808_0x8103_0x005B = new JT808_0x8103_0x005B();
            jT808_0x8103_0x005B.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x005B.ParamLength = reader.ReadByte();
            jT808_0x8103_0x005B.ParamValue = reader.ReadUInt16();
            return jT808_0x8103_0x005B;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public override void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x005B value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
